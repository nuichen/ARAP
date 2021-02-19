namespace IvyTran.IBLL.ERP
{
    public interface IUpdate
    {
        bool IsOk();
        void AutoUpdate();
        void Update();
        string GetServerVer( );
    }
}
