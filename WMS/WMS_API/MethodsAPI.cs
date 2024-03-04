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
        private const string baseUrlMain = "http://192.168.4.101/wms_arc_ceramics/hs/WMSExchange/";
        private const string baseUrlTest = "http://192.168.4.101/wms_arc_ceramics_test/hs/WMSExchange/";
        private const string usernameAPI = "Будзяк Тарас";
        private const string passwordAPI = "123";

        public const string wareHouseCode = "000000001";

        public static string baseUrl = baseUrlMain;

        public static event EventHandler<SendGoodEvEventArgs> SendGoodEv;

        public class SendGoodEvEventArgs : EventArgs
        {
            public int Count { get; set; }
        }

        public static void ChangeServer(bool Main)
        {
            if (Main)
            {
                baseUrl = baseUrlMain;
            }
            else
            {
                baseUrl = baseUrlTest;
            }
        }

        public static void TestRequest()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(error);
            DataProvider.SaveErrorToSQL(result.CheckOnError());
        }

        public static void SendClassifierPackage()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetClassifierPackage();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error);
            DataProvider.SaveErrorToSQL(result.CheckOnError());
        }

        public static void SendGroups()
        {
            for (var level = 1; level <= 10;  level++)
            {
                var data = new InfoRestOfGoods();
                data.WareHouseCode = wareHouseCode;
                data.Info = DataProvider.GetGroupsProducts(level);
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error, $"level = {level}");
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }
        }

        public static void SendChangeGroups()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetChangeGroups();
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error);
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }
        }

        public static void SendChangeGoods()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetChangeGoods();
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error);
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }
        }

        public static void SendChangeFasovka()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetChangeFasovka();
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error);
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }
        }

        public static void SendChangeShufr()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetChangeShufr();
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error);
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }
        }

        public static void SendGood(int codetvun)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetOneGood(codetvun);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error, $"codetvun = {codetvun}");
            DataProvider.SaveErrorToSQL(result.CheckOnError());
        }

        public static void SendGoodGroups(int groгps)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetGroupGood(groгps);
            if (data.Info.Products.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                SendGoodEv?.Invoke(null, new SendGoodEvEventArgs() { Count = data.Info.Products.Count });
                DataProvider.SaveErrorToSQL(error, $"groups = {groгps}");
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }

            //посилання товарів з підгруп рекурсивно:
            var subgroups = DataProvider.GetSubgroups(groгps);
            foreach(var group in subgroups)
            {
                SendGoodGroups(group);
            }
        }

        public static void SendGoodPlace(int place)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetGoodsPlace(place);
            if (data.Info.Products.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(error, $"place = {place}");
                DataProvider.SaveErrorToSQL(result.CheckOnError());
            }
        }

        public static void SendSets()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetSets();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error);
            DataProvider.SaveErrorToSQL(result.CheckOnError());
        }

        public static void GetModifications()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(error);
            var orsers = result.Deserialize<OrdersModifications>(out error);
            DataProvider.SaveErrorToSQL(error);
            var nomenclature = orsers.TableDocInfo.Where(or => or.TypeOperation == OrderModificationsTypeOperation.NOMENCLATURE).ToList();
            foreach(var order in nomenclature)
            {
                result = RequestData.SendPost(baseUrl + "POSTInfoOnOrder", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\", \"GUIDorder\": \"" + order.GUIDorder + "\"}", out error);
                DataProvider.SaveErrorToSQL(error);
                var product = result.Deserialize<InfoOnOrder>(out error);
                DataProvider.SaveErrorToSQL(error);
                if (product.TypeOperation == OrderModificationsTypeOperation.NOMENCLATURE)
                {
                    DataProvider.AddModificationsToBD(product.Product);
                    result = RequestData.SendPost(baseUrl + "POSTOrderResultAS", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\"}", out error);
                    DataProvider.SaveErrorToSQL(error);
                }
            }
        }

        public static void GetInventory()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(error);
            var orsers = result.Deserialize<OrdersModifications>(out error);
            DataProvider.SaveErrorToSQL(error);
            var nomenclature = orsers.TableDocInfo.Where(or => or.TypeOperation == OrderModificationsTypeOperation.INVENTORY).ToList();
            foreach (var order in nomenclature)
            {
                result = RequestData.SendPost(baseUrl + "POSTInfoOnOrder", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\", \"GUIDorder\": \"" + order.GUIDorder + "\"}", out error);
                DataProvider.SaveErrorToSQL(error);
                var product = result.Deserialize<InfoOnOrder>(out error);
                DataProvider.SaveErrorToSQL(error);
                if (product.TypeOperation == OrderModificationsTypeOperation.NOMENCLATURE)
                {
                    DataProvider.AddModificationsToBD(product.Product);
                    result = RequestData.SendPost(baseUrl + "POSTOrderResultAS", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\"}", out error);
                    DataProvider.SaveErrorToSQL(error);
                }
            }
        }

        public static void SendRoute(int route)
        {
            var data = new RestRouteSheet();
            data.WareHouseCode = wareHouseCode;
            data.RouteSheet = DataProvider.GetRoute(route);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTRouteSheet", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(error, $"route = {route}");
            DataProvider.SaveErrorToSQL(result.CheckOnError());
        }
    }
}
