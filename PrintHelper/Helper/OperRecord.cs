using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.Helper
{
    class OperRecord
    {
        private string xml = "";
        public OperRecord(string xml)
        {
            this.xml = xml;
        }

        private OperRecord _Pre = null;
        public OperRecord Pre
        {
            get
            {
                return _Pre;
            }
            set
            {
                _Pre = value;
            }
        }

        private OperRecord _Next = null;
        public OperRecord Next
        {
            get
            {
                return _Next;
            }
            set
            {
                _Next = value;
            }
        }

        public void Undo(cons.IDesign des)
        {
            des.xml = xml;
           
        }

        

    }
}
