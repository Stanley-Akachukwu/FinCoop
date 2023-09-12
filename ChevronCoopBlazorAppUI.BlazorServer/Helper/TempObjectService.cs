namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
    public class TempObjectService
    {
        private object tempObject;
        private object tempObject2;

        public void SetTempObject(object obj)
        {
            tempObject = obj;
        }

        public void SetLoanAccountMasterViewTempObject(object obj)
        {
            tempObject2 = obj;
        }

        public object GetTempObject()
        {
            var obj = tempObject;
            return obj;
        }
        public object GetLoanAccountMasterViewTempObject()
        {
            var obj = tempObject2;
            return obj;
        }
    }
}
