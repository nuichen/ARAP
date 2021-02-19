using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    public interface ISysBLL
    {
        List<Model.bt_par_setting> GetParSettingList();
        void UpdateParSetting(List<Model.bt_par_setting> lst);
        void DeleteOldData();
        void WritePrintLog(string sheet_no);
        void WriteClickLog(string click_num);

        void clear_db();
    }
}
