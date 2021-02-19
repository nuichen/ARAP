namespace IvyTran.IBLL.ERP
{
  public   interface IImageClient
    {
      void Upload(System.Drawing.Image img,string img_type,out string img_path);
      void Delete(string img_path);
    }
}
