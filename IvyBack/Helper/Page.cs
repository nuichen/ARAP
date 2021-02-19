using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyBack.Helper
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class Page<T>
    {
        //当前页数
        private int _pageIndex;
        public int PageIndex
        {
            get
            {
                if (_pageIndex < 1)
                    _pageIndex = 1;
                return _pageIndex;
            }
            set { _pageIndex = value; }
        }

        //总数量
        private int _pageCount;
        public int PageCount
        {
            get
            {
                if (_list != null && _pageCount != _list.Count)
                    _pageCount = _list.Count;
                return _pageCount;
            }
            set
            {
                if (PageSize > 0)
                    _pageMax = _pageCount % _pageSize == 0 ? _pageCount / _pageSize : (_pageCount / _pageSize) + 1;
                if (_pageMax < _pageIndex)
                {
                    _pageIndex = _pageMax;
                }
                _pageCount = value;
            }
        }

        //最大页数
        private int _pageMax;
        public int PageMax
        {
            get
            {
                if (PageSize > 0)
                    _pageMax = _pageCount % _pageSize == 0 ? _pageCount / _pageSize : (_pageCount / _pageSize) + 1;

                if (_pageMax == 0)
                    _pageMax = 1;

                return _pageMax;
            }
            set { _pageMax = value; }
        }

        //分页数量
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        //集合
        private List<T> _list;
        public List<T> List
        {
            get
            {
                if (_list == null)
                    _list = new List<T>();
                return _list;
            }
            set
            {
                _pageCount = value.Count;
                _list = value;
            }
        }

        private DataTable _tb;
        public DataTable Tb
        {
            get
            {
                if (_tb == null)
                    _tb = Assign<T>();
                return _tb;
            }
            set
            {
                _tb = value;
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        public void HomPage()
        {
            _pageIndex = 1;
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void PrePage()
        {
            if (_pageIndex > 1)
                _pageIndex--;
            else
                HomPage();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (_pageIndex < _pageMax)
                _pageIndex++;
            else
                TraPage();
        }

        /// <summary>
        /// 尾页
        /// </summary>
        public void TraPage()
        {
            _pageIndex = _pageMax;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>返回当前页数数据</returns>
        public List<T> GetList()
        {
            return _list.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize).ToList();
        }

        /// <summary>
        /// 获取某个对象
        /// </summary>
        /// <param name="index">对象下表</param>
        /// <returns>对象</returns>
        public T GetObj(int index)
        {
            try
            {
                return _list[index];
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }

        /// <summary>
        /// 获取当前页的某个对象
        /// </summary>
        /// <param name="index">当前页的对象下表</param>
        /// <returns>对象</returns>
        public T GetObjPage(int index)
        {
            try
            {
                return _list.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize).ToList()[index];
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }

        public DataTable Assign<T>()
        {
            DataTable tb = new DataTable();
            var t = typeof(T);
            var pars = t.GetProperties();

            foreach (var item in pars)
            {
                tb.Columns.Add(item.Name, item.PropertyType);
            }
            return tb;
        }



    }
}
