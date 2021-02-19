using System;
using Model;

namespace IvyTran.BLL.ERP
{
    public class ComputeCost
    {
        public void Compute(DB.IDB db, string sheet_property, string db_no, string branch_no, string item_no,
        decimal sheet_qty, decimal sheet_price,
        out decimal price, out decimal end_price, out decimal adjust_amt)
        {
            string sql = "select * from bi_t_item_info where item_no='" + item_no + "'";
            bi_t_item_info item = db.ExecuteToModel<bi_t_item_info>(sql, null);
            if (item == null)
            {
                throw new Exception("不存在商品内码" + item.item_no);
            }

            sql = "select * from ic_t_branch_stock where branch_no='" + branch_no + "'" +
            " and item_no='" + item_no + "'";
            Model.ic_t_branch_stock stock = db.ExecuteToModel<Model.ic_t_branch_stock>(sql, null);
            if (stock == null)
            {
                stock = new Model.ic_t_branch_stock();
                stock.branch_no = branch_no;
                stock.item_no = item_no;
                stock.stock_qty = 0;
                stock.cost_price = item.price;
                stock.display_flag = "1";
                stock.last_price = 0;
                stock.fifo_price = 0;
                stock.update_time = System.DateTime.Now;
                db.Insert(stock);
            }


            if (sheet_property == "1")
            {
                //入库类
                if (db_no.Equals("-"))
                {
                    InPrice(stock.stock_qty, stock.cost_price, -sheet_qty, sheet_price, out price, out end_price, out adjust_amt);
                }
                else
                {
                    InPrice(stock.stock_qty, stock.cost_price, sheet_qty, sheet_price, out price, out end_price, out adjust_amt);
                }
            }
            else
            {
                //出库类
                if (db_no.Equals("-"))
                {
                    OutPrice(stock.stock_qty, stock.cost_price, sheet_qty, sheet_price, out price, out end_price, out adjust_amt);
                }
                else
                {
                    OutPrice(stock.stock_qty, stock.cost_price, -sheet_qty, sheet_price, out price, out end_price, out adjust_amt);
                }
            }


        }

        /// <summary>
        /// 入库类
        /// </summary>
        /// <param name="stock_qty">库存数量</param>
        /// <param name="stock_price">库存成本</param>
        /// <param name="sheet_qty">入库数量</param>
        /// <param name="sheet_price">入库成本</param>
        /// <param name="price">成本</param>
        /// <param name="end_price">最后价格</param>
        /// <param name="adjust_amt">调整金额</param>
        private void InPrice(decimal stock_qty, decimal stock_price, decimal sheet_qty, decimal sheet_price,
          out decimal price, out decimal end_price, out decimal adjust_amt)
        {
            price = 0.00M;
            end_price = 0.00M;
            adjust_amt = 0.00M;


            if (stock_qty < 0)
            {
                //负库存入库
                if (stock_qty + sheet_qty < 0)
                {
                    //入库后小于0 取库存成本
                    price = sheet_price;
                    end_price = stock_price;
                    adjust_amt = (stock_qty + sheet_qty) * end_price - (stock_qty * stock_price + sheet_qty * sheet_price);
                }
                else
                {
                    //入库后大于0 取入库成本
                    price = sheet_price;
                    end_price = sheet_price;
                    adjust_amt = (stock_qty + sheet_qty) * end_price - (stock_qty * stock_price + sheet_qty * sheet_price);
                }
            }
            else
            {
                //正库存入库
                if (stock_qty + sheet_qty < 0)
                {
                    //入库后成负库存 取入库成本
                    price = sheet_price;
                    end_price = sheet_price;
                    adjust_amt = (stock_qty + sheet_qty) * end_price - (stock_qty * stock_price + sheet_qty * sheet_price);
                }
                else if (stock_qty + sheet_qty == 0)
                {
                    //入库后成0 取入库成本
                    price = sheet_price;
                    end_price = sheet_price;
                    adjust_amt = (stock_qty + sheet_qty) * end_price - (stock_qty * stock_price + sheet_qty * sheet_price);
                }
                else
                {
                    //入库后正库存 判断金额
                    if ((stock_qty * stock_price + sheet_qty * sheet_price) > 0)
                    {
                        //入库后 正库存 正金额 采用加权
                        price = sheet_price;
                        end_price = (stock_qty * stock_price + sheet_qty * sheet_price) / (stock_qty + sheet_qty);
                        adjust_amt = (stock_qty + sheet_qty) * end_price - (stock_qty * stock_price + sheet_qty * sheet_price);
                    }
                    else
                    {
                        //入库后 正库存  负金额 取库存成本
                        price = sheet_price;
                        end_price = stock_price;
                        adjust_amt = (stock_qty + sheet_qty) * end_price - (stock_qty * stock_price + sheet_qty * sheet_price);
                    }
                }
            }

        }

        /// <summary>
        /// 出库类
        /// </summary>
        private void OutPrice(decimal stock_qty, decimal stock_price, decimal sheet_qty, decimal sheet_price,
          out decimal price, out decimal end_price, out decimal adjust_amt)
        {
            price = stock_price;
            end_price = stock_price;
            adjust_amt = 0.00M;


            #region 
            /*
            if (stock_qty < 0)
            {
                //负库存出库
                if (stock_qty - sheet_qty < 0)
                {
                    //出库后 库存还是小于0 加权
                    price = sheet_price;
                    end_price = (stock_qty * stock_price - sheet_qty * sheet_price) / (stock_qty - sheet_qty);
                    adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                }
                else if (stock_qty - sheet_qty == 0)
                {
                    //出库后 库存等于0 取出库成本
                    price = sheet_price;
                    end_price = sheet_price;
                    adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                }
                else
                {
                    //出库后 库存大于0 
                    if ((stock_qty * stock_price - sheet_price * sheet_qty) > 0)
                    {
                        //出库后 金额大于0 加权
                        price = sheet_price;
                        end_price = (stock_qty * stock_price - sheet_qty * sheet_price) / (stock_qty - sheet_qty);
                        adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                    }
                    else
                    {
                        //出库后 金额小于等于0 取出库成本
                        price = sheet_price;
                        end_price = stock_price;
                        adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                    }

                }
            }
            else
            {
                //正库存出库
                if (stock_qty - sheet_qty < 0)
                {
                    //出库后 结存库存小于0 取出库成本
                    price = sheet_price;
                    end_price = stock_price;
                    adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                }
                else if (stock_qty - sheet_qty == 0)
                {
                    //出库后 结存库存等于0 取出库成本
                    price = sheet_price;
                    end_price = stock_price;
                    adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                }
                else
                {
                    //出库后 结存库存大于0
                    if ((stock_qty * stock_price - sheet_qty * sheet_price) > 0)
                    {
                        //出库后 金额大于0 加权
                        price = sheet_price;
                        end_price = (stock_qty * stock_price - sheet_qty * sheet_price) / (stock_qty - sheet_qty);
                        adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                    }
                    else
                    {
                        //出库后 金额小于等于0 取出库成本
                        price = sheet_price;
                        end_price = stock_price;
                        adjust_amt = (stock_qty - sheet_qty) * end_price - (stock_qty * stock_price - sheet_qty * sheet_price);
                    }
                }
            }*/
            #endregion

        }

    }
}
