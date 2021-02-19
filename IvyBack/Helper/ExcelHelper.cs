﻿using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace IvyBack.Helper
{
    public class ExcelHelper
    {
        public ExcelHelper()
        {
        }

        /// <summary> 
        /// 导出数据
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="path">保存路径</param> 
        public void WriteToExcel(DataTable dt, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 

            Cells cells = sheet.Cells;//单元格 

            //全局样式
            Style main_style = workbook.CreateStyle();
            main_style.Name = "main_style";
            main_style.Font.Name = "微软雅黑";
            main_style.Font.Size = 12;
            cells.ApplyStyle(main_style, new StyleFlag() { All = true });

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            //生成列名
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                cells[0, i].PutValue(dt.Columns[i]);
            }

            //生成数据行 
            for (int i = 1; i <= Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    Cell cell = cells[i, k];

                    string show_value = GetTableCellValue(dt, i - 1, k);
                    if (dt.Columns[k].DataType == typeof(string))
                    {
                        show_value = "'" + show_value;
                    }

                    SetColumnDateType(cell, dt.Columns[k].DataType);

                    cell.PutValue(show_value, true);

                }
            }

            SetColumnWithAuto(sheet);
            workbook.Save(path);
        }

        public void WriteToExcel(List<DataTable> dts, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            workbook.Worksheets.Clear();

            int index = 0;
            foreach (DataTable dt in dts)
            {
                Worksheet sheet = workbook.Worksheets.Add(dt.TableName);

                Cells cells = sheet.Cells;//单元格 

                //全局样式
                Style main_style = workbook.CreateStyle();
                main_style.Name = "main_style";
                main_style.Font.Name = "微软雅黑";
                main_style.Font.Size = 12;
                cells.ApplyStyle(main_style, new StyleFlag() { All = true });

                int Colnum = dt.Columns.Count;//表格列数 
                int Rownum = dt.Rows.Count;//表格行数 

                //生成列名
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    cells[0, i].PutValue(dt.Columns[i].ColumnName);
                }

                //生成数据行 
                for (int i = 1; i <= Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        Cell cell = cells[i, k];

                        string show_value = dt.Rows[i - 1][k].ToString();
                        if (dt.Columns[k].DataType == typeof(string))
                        {
                            show_value = "'" + show_value;
                        }

                        cell.PutValue(show_value, true);

                        SetColumnDateType(cell, dt.Columns[k].DataType);
                    }
                }

                SetColumnWithAuto(sheet);
                index++;
            }

            workbook.Save(path);
        }

        /// <summary>
        /// 导出数据 自定义样式
        /// </summary>
        /// <param name="tb_main"></param>
        /// <param name="tb_detail"></param>
        /// <param name="path"></param>
        public void WriteToExcelStyle(DataTable tb_main, DataTable tb_detail, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            sheet.Name = tb_main.TableName;

            Cells cells = sheet.Cells;//单元格 
            cells.SetRowHeight(0, 24);
            cells.SetRowHeight(1, 22);


            int Colnum = tb_detail.Columns.Count;//表格列数 
            int Rownum = tb_detail.Rows.Count;//表格行数 

            //全局样式
            Style main_style = workbook.CreateStyle();
            main_style.Name = "main_style";
            main_style.Font.Name = "微软雅黑";
            main_style.Font.Size = 11;
            cells.ApplyStyle(main_style, new StyleFlag() { All = true });

            //标题
            cells.Merge(0, 0, 1, tb_detail.Columns.Count);
            cells[0, 0].PutValue(tb_main.Rows[0]["title"]);

            Style title_style = cells[0, 0].GetStyle();
            title_style.HorizontalAlignment = TextAlignmentType.Center;
            title_style.Font.Size = 16;
            title_style.Font.IsBold = true;
            title_style.Pattern = BackgroundType.Solid;
            title_style.IsTextWrapped = true;
            title_style.ForegroundColor = System.Drawing.Color.FromArgb(215, 215, 215);
            title_style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            title_style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            title_style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            title_style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            cells[0, 0].GetMergedRange().SetStyle(title_style);
            //生成列名
            for (int i = 0; i < tb_detail.Columns.Count; i++)
            {
                Cell cell = cells[1, i];

                Style cell_style = cell.GetStyle();
                cell_style.Font.Size = 11;
                cell_style.HorizontalAlignment = TextAlignmentType.Left;
                cell_style.TextDirection = TextDirectionType.LeftToRight;
                cell_style.VerticalAlignment = TextAlignmentType.Center;
                cell_style.Pattern = BackgroundType.Solid;
                cell_style.IsTextWrapped = true;
                cell_style.ForegroundColor = System.Drawing.Color.FromArgb(215, 215, 215);
                cell_style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                cell_style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                cell_style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                cell_style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                cell.SetStyle(cell_style, true);

                cell.PutValue(tb_detail.Columns[i]);
                cells.SetColumnWidth(i, 20);
            }

            int first_row = -1;
            string item_name = "";
            //生成数据行 
            for (int i = 1; i <= Rownum; i++)
            {
                cells.SetRowHeight(i + 1, 15);
                for (int k = 0; k < Colnum; k++)
                {
                    Cell cell = cells[i + 1, k];

                    Style cell_style = cell.GetStyle();
                    cell_style.Font.Name = "微软雅黑";
                    cell_style.Font.Size = 11;
                    cell_style.HorizontalAlignment = TextAlignmentType.Left;
                    cell_style.TextDirection = TextDirectionType.LeftToRight;
                    cell_style.VerticalAlignment = TextAlignmentType.Center;
                    cell_style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    cell_style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    cell_style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    cell_style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    cell.SetStyle(cell_style);

                    cell.PutValue(tb_detail.Rows[i - 1][k].ToString());
                }

                if (first_row < 0)
                {
                    first_row = i + 1;
                    item_name = tb_detail.Rows[i - 1][0].ToString();
                }
                else
                {
                    if (!item_name.Equals(tb_detail.Rows[i - 1][0].ToString()))
                    {
                        cells.Merge(first_row, 7, i + 1 - first_row, 1);
                        cells.Merge(first_row, 6, i + 1 - first_row, 1);

                        first_row = i + 1;
                        item_name = tb_detail.Rows[i - 1][0].ToString();
                    }
                    else
                    {
                        if (i == Rownum)
                        {
                            cells.Merge(first_row, 7, i + 2 - first_row, 1);
                            cells.Merge(first_row, 6, i + 2 - first_row, 1);

                            first_row = -1;
                            item_name = "";
                        }
                    }
                }
            }

            workbook.Save(path);
        }
        public void WriteToExcelStyles(List<DataTable> tb_mains, List<DataTable> tb_details, string path)
        {
            Workbook workbook = new Workbook(); //工作簿 
            for (int m = 0; m < tb_mains.Count; m++)
            {
                if (workbook.Worksheets.Count - 1 < m)
                {
                    workbook.Worksheets.Add();
                }
                DataTable tb_main = tb_mains[m];
                DataTable tb_detail = tb_details[m];

                Worksheet sheet = workbook.Worksheets[m]; //工作表 
                sheet.Name = tb_main.TableName;

                Cells cells = sheet.Cells;//单元格 
                cells.SetRowHeight(0, 24);
                cells.SetRowHeight(1, 22);

                int Colnum = tb_detail.Columns.Count;//表格列数 
                int Rownum = tb_detail.Rows.Count;//表格行数 

                //全局样式
                Style main_style = workbook.CreateStyle();
                main_style.Name = "main_style";
                main_style.Font.Name = "微软雅黑";
                main_style.Font.Size = 11;
                cells.ApplyStyle(main_style, new StyleFlag() { All = true });

                //标题
                cells.Merge(0, 0, 1, tb_detail.Columns.Count);
                cells[0, 0].PutValue(tb_main.Rows[0]["title"]);

                Style title_style = cells[0, 0].GetStyle();
                title_style.HorizontalAlignment = TextAlignmentType.Center;
                title_style.Font.Size = 16;
                title_style.Font.IsBold = true;
                title_style.Pattern = BackgroundType.Solid;
                title_style.IsTextWrapped = true;
                title_style.ForegroundColor = System.Drawing.Color.FromArgb(215, 215, 215);
                title_style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                title_style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                title_style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                title_style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                cells[0, 0].GetMergedRange().SetStyle(title_style);
                //生成列名
                for (int i = 0; i < tb_detail.Columns.Count; i++)
                {
                    Cell cell = cells[1, i];

                    Style cell_style = cell.GetStyle();
                    cell_style.Font.Size = 11;
                    cell_style.HorizontalAlignment = TextAlignmentType.Left;
                    cell_style.TextDirection = TextDirectionType.LeftToRight;
                    cell_style.VerticalAlignment = TextAlignmentType.Center;
                    cell_style.Pattern = BackgroundType.Solid;
                    cell_style.IsTextWrapped = true;
                    cell_style.ForegroundColor = System.Drawing.Color.FromArgb(215, 215, 215);
                    cell_style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    cell_style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    cell_style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    cell_style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                    cell.SetStyle(cell_style, true);

                    cell.PutValue(tb_detail.Columns[i]);
                    cells.SetColumnWidth(i, 20);
                }

                int first_row = -1;
                string item_name = "";
                //生成数据行 
                for (int i = 1; i <= Rownum; i++)
                {
                    cells.SetRowHeight(i + 1, 15);
                    for (int k = 0; k < Colnum; k++)
                    {
                        Cell cell = cells[i + 1, k];

                        Style cell_style = cell.GetStyle();
                        cell_style.Font.Name = "微软雅黑";
                        cell_style.Font.Size = 11;
                        cell_style.HorizontalAlignment = TextAlignmentType.Left;
                        cell_style.TextDirection = TextDirectionType.LeftToRight;
                        cell_style.VerticalAlignment = TextAlignmentType.Center;
                        cell_style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                        cell_style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                        cell_style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                        cell_style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                        cell.SetStyle(cell_style);

                        cell.PutValue(tb_detail.Rows[i - 1][k].ToString());
                    }

                    if (first_row < 0)
                    {
                        first_row = i + 1;
                        item_name = tb_detail.Rows[i - 1][0].ToString();
                    }
                    else
                    {
                        if (!item_name.Equals(tb_detail.Rows[i - 1][0].ToString()))    //下一行第一列与上一行相同
                        {
                            cells.Merge(first_row, 8, i + 1 - first_row, 1);
                            //   cells.Merge(first_row, 6, i + 1 - first_row, 1);

                            first_row = i + 1;
                            item_name = tb_detail.Rows[i - 1][0].ToString();
                        }
                        else                                                            //下一行第一列不与上一行相同
                        {
                            if (i == Rownum)
                            {
                                cells.Merge(first_row, 8, i + 2 - first_row, 1);
                                //   cells.Merge(first_row, 6, i + 2 - first_row, 1);

                                first_row = -1;
                                item_name = "";
                            }
                        }
                    }

                }
            }
            workbook.Save(path);
        }


        /// <summary> 
        /// 导出数据到模版 
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="path">保存路径</param> 
        public void WriteToExcelOfModel(DataTable tb_main, DataTable tb_detail, string path, string model_path)
        {
            Workbook workbook = new Workbook(model_path);
            WorkbookDesigner designer = new WorkbookDesigner(workbook);
            designer.SetDataSource(tb_main);
            designer.SetDataSource(tb_detail);
            designer.Process();
            designer.Workbook.Save(path);
        }
        public void WriteToExcelOfModel(List<DataTable> tb_main, List<DataTable> tb_detail, string path, string model_path)
        {
            Workbook workbook = new Workbook(model_path);
            WorkbookDesigner designer = new WorkbookDesigner(workbook);
            foreach (DataTable tb in tb_main)
            {
                designer.SetDataSource(tb);
            }
            foreach (DataTable tb in tb_detail)
            {
                designer.SetDataSource(tb);
            }
            designer.Process();
            designer.Workbook.Save(path);
        }

        public void WriteToExcelOfModels(List<DataTable> tb_main, List<DataTable> tb_detail, string path, string model_path)
        {
            {
                Workbook workbook = new Workbook(model_path);

                int index = 0;
                foreach (DataTable dt in tb_main)
                {
                    Worksheet sheet = workbook.Worksheets.Add(dt.TableName);
                    sheet.Copy(workbook.Worksheets[0]);

                    Cells cells = sheet.Cells;


                    for (int k = 0; k < cells.MaxRow + 1; k++)
                    {
                        for (int i = 0; i < cells.MaxColumn + 1; i++)
                        {
                            string value = cells[k, i].StringValue.Trim();
                            if (value.Contains("[main]"))
                            {
                                value = value.Replace("[main]", $"[{dt.TableName}]");
                            }
                            if (value.Contains("[detail]"))
                            {
                                value = value.Replace("[detail]", $"[{tb_detail[index].TableName}]");
                            }
                            cells[k, i].PutValue(value);
                        }
                    }

                    index++;
                }

                workbook.Worksheets.RemoveAt(0);
                workbook.Save(Program.path + "\\Excel\\临时模板.xls");
            }

            {
                Workbook workbook = new Workbook(Program.path + "\\Excel\\临时模板.xls");
                WorkbookDesigner designer = new WorkbookDesigner(workbook);
                foreach (DataTable tb in tb_main)
                {
                    designer.SetDataSource(tb);
                }
                foreach (DataTable tb in tb_detail)
                {
                    designer.SetDataSource(tb);
                }
                designer.Process();
                designer.Workbook.Save(path);
            }

        }


        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataTable ExportDataTableAsString(string filePath)
        {
            Workbook workbook = new Workbook(filePath);
            Cells cells = workbook.Worksheets[0].Cells;

            int index = 0;

            for (int i = 0; i < cells.MaxDataRow + 1; i++)
            {
                int ishave = 0;
                for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                {
                    string str = cells[i, j].StringValue.Trim();
                    if (!string.IsNullOrEmpty(str))
                    {
                        ishave++;
                    }
                }
                if (ishave > (cells.MaxDataColumn + 1) / 2)
                {
                    index = i;
                    break;
                }
            }

            DataTable dataTable2 = cells.ExportDataTableAsString(index, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);//showTitle

            return dataTable2;
        }
        /// <summary>
        /// 导入（带合并单元格）
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Dictionary<string, DataTable> ImportExcelFileAspose(string filePath)
        {
            Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            Workbook workbook = new Workbook(filePath);


            foreach (Worksheet ws in workbook.Worksheets)
            {
                Cells cells = ws.Cells;

                DataTable tb = new DataTable();
                int index = 0;

                for (int i = 0; i < cells.MaxDataRow + 1; i++)
                {
                    int ishave = 0;
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {
                        string str = cells[i, j].StringValue.Trim();
                        if (!string.IsNullOrEmpty(str))
                        {
                            ishave++;
                        }
                    }
                    if (ishave > (cells.MaxDataColumn + 1) / 2)
                    {
                        index = i;
                        break;
                    }
                }
                for (int i = 0; i < cells.MaxDataColumn + 1; i++)
                {
                    string str = cells[index, i].StringValue.Trim();
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (tb.Columns.Contains(str))
                        {
                            tb.Columns.Add(str + i);
                        }
                        else
                        {
                            tb.Columns.Add(str);
                        }
                    }
                    else
                    {
                        tb.Columns.Add("Column" + i);
                    }
                }
                for (int i = index + 1; i < cells.MaxDataRow + 1; i++)
                {
                    DataRow dr = tb.NewRow();
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {
                        string str = cells[i, j].StringValue.Trim();
                        if (cells[i, j].IsMerged)
                        {
                            //是合并单元格
                            Range range = cells[i, j].GetMergedRange();
                            str = cells[range.FirstRow, range.FirstColumn].StringValue.Trim();
                            dr[j] = str;
                        }
                        else
                        {
                            dr[j] = str;
                        }

                    }
                    tb.Rows.Add(dr);
                }

                dic.Add(ws.Name, tb);
            }

            return dic;
        }


        public List<T> ImportExcel<T>(string filePath, string mustFileds, Dictionary<string, string> fileCorrDic, Action<T, DataRow> createAction) where T : new()
        {
            List<T> modelList = new List<T>();
            DataTable tb = ExportDataTableAsString(filePath);

            if (tb.Rows.Count < 1 || tb.Columns.Count < 1)
            {
                throw new ArgumentException("Excel不能为空");
            }

            if (!tb.ExistFileds(mustFileds, out string message))
            {
                throw new ArgumentException(message);
            }

            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (DataRow row in tb.Rows)
            {
                T model = System.Activator.CreateInstance<T>();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string dr_coloumn = "";
                    if (fileCorrDic.TryGetValue(propertyInfo.Name, out dr_coloumn))
                    {
                        if (tb.Columns.Contains(dr_coloumn))
                        {
                            object value;
                            switch (Type.GetTypeCode(propertyInfo.PropertyType))
                            {
                                case TypeCode.String:
                                    value = row[dr_coloumn].ToString();
                                    break;
                                case TypeCode.Double:
                                    value = row[dr_coloumn].ToDouble();
                                    break;
                                case TypeCode.Decimal:
                                    value = row[dr_coloumn].ToDecimal();
                                    break;
                                case TypeCode.Int16:
                                    value = row[dr_coloumn].ToInt16();
                                    break;
                                case TypeCode.Int32:
                                    value = row[dr_coloumn].ToInt32();
                                    break;
                                case TypeCode.Int64:
                                    value = row[dr_coloumn].ToInt64();
                                    break;
                                case TypeCode.DateTime:
                                    value = row[dr_coloumn].ToDateTime();
                                    break;
                                default:
                                    value = row[dr_coloumn];
                                    break;
                            }

                            propertyInfo.SetValue(model, value, null);
                        }

                    }
                }

                createAction(model, row);
                modelList.Add(model);
            }

            return modelList;
        }

        Style GetStyle(string path)
        {
            Workbook workbook = new Workbook(path);
            Worksheet ws = workbook.Worksheets[0];
            Cells cells = ws.Cells;
            Style style = cells.Style;
            for (int i = 0; i <= cells.MaxColumn; i++)
            {
                Console.WriteLine("w:" + cells.GetColumnWidth(i));
            }
            for (int i = 0; i <= cells.MaxRow; i++)
            {
                Console.WriteLine("h:" + cells.GetColumnWidth(i));
            }

            return style;
        }

        /// <summary>
        /// 设置表页的列宽度自适应
        /// </summary>
        /// <param name="sheet">worksheet对象</param>
        void SetColumnWithAuto(Worksheet sheet)
        {
            Cells cells = sheet.Cells;
            int columnCount = cells.MaxColumn;  //获取表页的最大列数
            int rowCount = cells.MaxRow;        //获取表页的最大行数

            for (int col = 0; col < columnCount; col++)
            {
                sheet.AutoFitColumn(col, 0, rowCount);
            }
            for (int col = 0; col < columnCount; col++)
            {
                cells.SetColumnWidthPixel(col, cells.GetColumnWidthPixel(col) + 30);
            }
        }

        void SetColumnDateType(Cell cell, Type type)
        {
            Style style = cell.GetStyle();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    style.Number = 49;
                    break;
                case TypeCode.Double:
                case TypeCode.Decimal:
                    style.Number = 2;
                    break;
                case TypeCode.Int16:
                case TypeCode.Int32:
                    style.Number = 1;
                    break;
                case TypeCode.DateTime:
                    style.Custom = "yyyy-MM-dd HH:mm:ss";
                    break;
                default:
                    style.Number = 49;
                    return;
            }

            cell.SetStyle(style, true);
        }

        dynamic GetTableCellValue(DataTable tb, int row, int column)
        {
            dynamic obj;
            switch (Type.GetTypeCode(tb.Columns[column].DataType))
            {
                case TypeCode.DateTime:
                    DateTime time = tb.Rows[row][column].ToDateTime();
                    obj = time.Toyyyy_MM_dd_HH_mm_ss();
                    break;
                default:
                    obj = tb.Rows[row][column].ToString();
                    break;
            }
            return obj;
        }
    }
}
