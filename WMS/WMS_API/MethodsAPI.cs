using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_API.ModelABM;

namespace WMS_API
{
    public static class MethodsAPI
    {
        private const string baseUrl = "http://192.168.4.101/wms_arc_ceramics/hs/WMSExchange/";
        private const string usernameAPI = "Будзяк Тарас";
        private const string passwordAPI = "123";

        public const string wareHouseCode = "000000001";

        public static event EventHandler<SendGoodEvEventArgs> SendGoodEv;

        public class SendGoodEvEventArgs : EventArgs
        {
            public int Count { get; set; }
        }

        public static void TestRequest()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(error);
        }

        public static void SendClassifierPackage()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetClassifierPackage();
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error);
        }

        public static void SendGroups()
        {
            for (var level = 1; level <= 10;  level++)
            {
                var data = new InfoRestOfGoods();
                data.WareHouseCode = wareHouseCode;
                data.Info = DataProvider.GetGroupsProducts(level);
                var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error);
            }
        }

        public static void SendGood(int codetvun)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetOneGood(codetvun);
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error);
        }

        public static void SendGroups(int grops)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetGroupGood(grops);
            SendGoodEv?.Invoke(null, new SendGoodEvEventArgs() { Count = data.Info.Products.Count});
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error);

            //посилання товарів з підгруп рекурсивно:
            var subgroups = DataProvider.GetSubgroups(grops);
            foreach(var group in subgroups)
            {
                SendGroups(group);
            }
        }
    }
}
