namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
    public static class SortSerialNumber
    {
        public static int serialNumber = 1;
        public static Dictionary<object, int> serialNumberMap = new Dictionary<object, int>();

        public static int GetSerialNumber(object dataItem)
        {
            if (!serialNumberMap.ContainsKey(dataItem))
            {
                serialNumberMap[dataItem] = serialNumber++;
            }
            else
            {
                serialNumber = 1;
            }

            return serialNumberMap[dataItem];
        }
    }
}
