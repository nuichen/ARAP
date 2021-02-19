using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IDesign
    {
        void Add(IPrintObject ins);
        void RemoveAll();
        void Remove(IPrintObject ins);
        int Count { get; }
        IPrintObject FirstSelectObject { get; set; }
        void OffSetX(int inte);
        void OffSetY(int inte);
        void OffSetWidth(int inte);
        void OffSetHeight(int inte);
        void ShowMenu(System.Drawing.Point ScrPoint);
        string xml { get; set; }
        void Record();
        List<IPrintObject> GetSelectObjects();

    }
}
