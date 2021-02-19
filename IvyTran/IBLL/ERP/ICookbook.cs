using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface ICookbook
    {
        DataTable GetCookbooks(string formulaName, string itemName, string type,string formulaClsNo);
        DataTable GetCookbookDetail(string formulaNo);
        DataTable SearchCookbook(string keyword);
        void GetCookbookDetails(string formulaNo, out bi_t_formula_main formulaMain, out DataTable formulaDetails);
        void SaveCookbook(bi_t_formula_main formulaMain, List<bi_t_formula_detail> formulaDetails);
        void ImportCookbook(DataTable tb);
        void DelCookbook(string formulaNos);

        DataTable GetCookbookClss(string formulaClsNo, int length);
        List<bi_t_formula_cls> GetCookbookClsLis(string formulaClsNo, int length);
        bi_t_formula_cls GetCookbookCls(string formulaClsNo);
        string MaxCookbookClsCode(string par_code);
        bi_t_formula_cls Add(string parCode,Model.bi_t_formula_cls item);
        bi_t_formula_cls Change(Model.bi_t_formula_cls item);
        void Delete(string formula_clsno);
    }
}