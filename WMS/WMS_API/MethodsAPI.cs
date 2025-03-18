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
        private const string baseUrl106 = "http://192.168.4.101/wms_arc_ceramics_106/hs/WMSExchange/";
        private const string usernameAPI = "Exchange";
        private const string passwordAPI = "Exchange";

        public const string wareHouseCode = "000000001";

        public static string baseUrl = baseUrlMain;

        public static event EventHandler<SendGoodEvEventArgs> SendGoodEv;

        public class SendGoodEvEventArgs : EventArgs
        {
            public int Count { get; set; }
        }

        public static void ChangeServer(int Main)
        {
            switch (Main)
            {
                case 0:
                    baseUrl = baseUrlMain;
                    break;
                case 1:
                    baseUrl = baseUrl106;
                    break;
                case 2:
                    baseUrl = baseUrlTest;
                    break;
            }
            //if (Main)
            //{
            //    baseUrl = baseUrlMain;
            //}
            //else
            //{
            //    baseUrl = baseUrlTest;
            //}
        }

        public static void TestRequest()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(null, error);
            DataProvider.SaveErrorToSQL(null, result.CheckOnError());
        }

        public static void SendClassifierPackage()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetClassifierPackage();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(null, error);
            DataProvider.SaveErrorToSQL(null, result.CheckOnError());
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
                DataProvider.SaveErrorToSQL(null, error, $"level = {level}");
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
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
                DataProvider.SaveErrorToSQL(null, error);
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }

        public static void SendChangeGoods()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetChangeGoods();
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0)
            {
                if (data.Info.Products.Count <= 10000)
                {
                    var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                    DataProvider.SaveErrorToSQL(null, error);
                    DataProvider.SaveErrorToSQL(null, result.CheckOnError());
                }
                else
                {
                    var i = 0;
                    while (i < data.Info.Products.Count)
                    {
                        var dataPart = new InfoRestOfGoods() { WareHouseCode = wareHouseCode};
                        dataPart.Info = new InfoInfoRestOfGoods()
                        {
                            GroupsProducts = new List<ProductGroup>(),
                            ClassifierPackage = new List<ClassifierPackage>(),
                            Products = new List<Product>(),
                            Packing = new List<Packing>(),
                            BarcodeTable = new List<BarcodeRow>(),
                            TableProduct = new List<ProductRow>()
                        };
                        var count = Math.Min(10000, data.Info.Products.Count - i);
                        dataPart.Info.Products = data.Info.Products.GetRange(i, count);
                        var json = JsonConvert.SerializeObject(dataPart, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                        DataProvider.SaveErrorToSQL(null, error);
                        DataProvider.SaveErrorToSQL(null, result.CheckOnError());
                        i += 10000;
                    }
                    i = 0;
                    while (i < data.Info.Packing.Count)
                    {
                        var dataPart = new InfoRestOfGoods() { WareHouseCode = wareHouseCode };
                        dataPart.Info = new InfoInfoRestOfGoods()
                        {
                            GroupsProducts = new List<ProductGroup>(),
                            ClassifierPackage = new List<ClassifierPackage>(),
                            Products = new List<Product>(),
                            Packing = new List<Packing>(),
                            BarcodeTable = new List<BarcodeRow>(),
                            TableProduct = new List<ProductRow>()
                        };
                        var count = Math.Min(10000, data.Info.Packing.Count - i);
                        dataPart.Info.Packing = data.Info.Packing.GetRange(i, count);
                        var json = JsonConvert.SerializeObject(dataPart, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                        DataProvider.SaveErrorToSQL(null, error);
                        DataProvider.SaveErrorToSQL(null, result.CheckOnError());
                        i += 10000;
                    }
                }
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
                DataProvider.SaveErrorToSQL(null, error);
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
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
                DataProvider.SaveErrorToSQL(null, error);
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }

        public static void SendChangeSets()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetChangeSets();
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0 || data.Info.TableSetProducts.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(null, error);
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }

        public static void SendGood(int codetvun)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetOneGood(codetvun);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(null, error, $"codetvun = {codetvun}");
            DataProvider.SaveErrorToSQL(null, result.CheckOnError());
        }

        public static void SendNewGood(int codetvun)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetNewGood(codetvun);
            if (data.Info.GroupsProducts.Count != 0 || data.Info.ClassifierPackage.Count != 0 || data.Info.Products.Count != 0 || data.Info.Packing.Count != 0 || data.Info.BarcodeTable.Count != 0 || data.Info.TableProduct.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(null, error, $"codetvun = {codetvun}");
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }

        public static void SendGoodGroups(int groгps)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetGroupGoodGroup(groгps);
            if (data.Info.Products.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                SendGoodEv?.Invoke(null, new SendGoodEvEventArgs() { Count = data.Info.Products.Count });
                DataProvider.SaveErrorToSQL(null, error, $"groups = {groгps}");
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }

            //посилання товарів з підгруп рекурсивно:
            var subgroups = DataProvider.GetSubgroups(groгps);
            foreach(var group in subgroups)
            {
                SendGoodGroups(group);
            }
        }

        public static void SendBarcodeGroups(int groгps)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetGroupBarcode(groгps);
            if (data.Info.BarcodeTable.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(null, error, $"barcode_groups = {groгps}");
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }

            var subgroups = DataProvider.GetSubgroups(groгps);
            foreach (var group in subgroups)
            {
                SendBarcodeGroups(group);
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
                DataProvider.SaveErrorToSQL(null, error, $"place = {place}");
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }

        public static void SendGoodsFromSelect(string select)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
#warning доробити!
            //data.Info = DataProvider.GetGoodsFromSelect(select);
            if (data.Info.Products.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(null, error, $"select = {select}");
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }

        public static void SendSets()
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetSets();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(null, error);
            DataProvider.SaveErrorToSQL(null, result.CheckOnError());
        }

        public static void GetModifications()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(null, error);
            var orsers = result.Deserialize<OrdersModifications>(out error);
            DataProvider.SaveErrorToSQL(null, error);
            var nomenclature = orsers.TableDocInfo.Where(or => or.TypeOperation == OrderModificationsTypeOperation.NOMENCLATURE).ToList();
            //doubleTrue - список істинних подвоєних елементів. Тобто якщо елемент подвоєний то сюди попав правильний(останній) елемент.
            var doubleTrue = nomenclature
                .OrderByDescending(or => or.RegDate)
                .GroupBy(v => v.GUIDorder)
                .Where(g => g.Count() > 1)
                .Select(g => g.First())
                .ToList();
            foreach(var order in nomenclature)
            {
                if (doubleTrue.Contains(order) || doubleTrue.All(d => d.GUIDorder != order.GUIDorder)) //якщо він правильний(останній) подвоєний або не подвоєний //тобто тут відлітають подвоєні старі зміни
                {
                    result = RequestData.SendPost(baseUrl + "POSTInfoOnOrder", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\", \"GUIDorder\": \"" + order.GUIDorder + "\"}", out error);
                    DataProvider.SaveErrorToSQL(null, error);
                    var product = result.Deserialize<InfoOnOrderNomenclature>(out error);
                    DataProvider.SaveErrorToSQL(null, error);
                    if (product.TypeOperation == OrderModificationsTypeOperation.NOMENCLATURE)
                    {
                        DataProvider.AddModificationsToBD(product.Product);
                    }
                }
                result = RequestData.SendPost(baseUrl + "POSTOrderResultAS", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\"}", out error);
                DataProvider.SaveErrorToSQL(null, error);
            }
        }

        public static void TestModifications()
        {
            var result = RequestData.SendPost(baseUrl + "POSTInfoOnOrder", usernameAPI, passwordAPI, "{\"GUIDentry\": \"bd1819d9-486c-4bef-89a3-9eb286c21c59\", \"GUIDorder\": \"1014880\"}", out string error);
            DataProvider.SaveErrorToSQL(null, error);
            var product = result.Deserialize<InfoOnOrderNomenclature>(out error);
            DataProvider.SaveErrorToSQL(null, error);
            if (product.TypeOperation == OrderModificationsTypeOperation.NOMENCLATURE)
            {
                DataProvider.AddModificationsToBD(product.Product);
            }
        }

        public static void GetInventory()
        {
            var result = RequestData.SendPost(baseUrl + "POSTOrdersModifications", usernameAPI, passwordAPI, "", out string error);
            DataProvider.SaveErrorToSQL(null, error);
            var orsers = result.Deserialize<OrdersModifications>(out error);
            DataProvider.SaveErrorToSQL(null, error);
            var nomenclature = orsers.TableDocInfo.Where(or => or.TypeOperation == OrderModificationsTypeOperation.INVENTORY).ToList();
            foreach (var order in nomenclature)
            {
                result = RequestData.SendPost(baseUrl + "POSTInfoOnOrder", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\", \"GUIDorder\": \"" + order.GUIDorder + "\"}", out error);
                DataProvider.SaveErrorToSQL(null, error);
                var product = result.Deserialize<InfoOnOrderNomenclature>(out error);
                DataProvider.SaveErrorToSQL(null, error);
                if (product.TypeOperation == OrderModificationsTypeOperation.INVENTORY)
                {
                    //DataProvider.AddModificationsToBD(product.Product);
                    result = RequestData.SendPost(baseUrl + "POSTOrderResultAS", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\"}", out error);
                    DataProvider.SaveErrorToSQL(null, error);
                }
            }
        }

        public static void GetTovarDodProp()
        {
            //для тесту додаткових властивостей товару:
            var order = new OrderModifications() { GUIDentry = new Guid("9dd012a4-64cc-49a3-b177-6db771ea9948"), GUIDorder = "1061965" };
            var result = RequestData.SendPost(baseUrl + "POSTInfoOnOrder", usernameAPI, passwordAPI, "{\"GUIDentry\": \"" + order.GUIDentry.ToString() + "\", \"GUIDorder\": \"" + order.GUIDorder + "\"}", out string error);
            DataProvider.SaveErrorToSQL(null, error);
            var product = result.Deserialize<InfoOnOrderInvoice>(out error);
            var tovDodProp = product.Invoice.TableProduct.Where(p => p.Part != null || p.Calibre != null || p.Tone != null || p.ShelfLife > new DateTime(2020, 01, 01) || p.DateOfManufacture > new DateTime(2020, 01, 01) || (p.Quality != "Кондиция" && p.Quality != "")).Select(t => new TovDodProp() {
                    codetvun = Convert.ToInt32(t.GUIDProduct),
                    coden = product.Invoice.NumberDoc,
                    Qty = t.Qty,
                    Quality = t.Quality,
                    DateofMan = t.DateOfManufacture.DateToSQL(),
                    ShelfLife = t.ShelfLife.DateToSQL(),
                    Part = t.Part,
                    Calibre = t.Calibre,
                    Tone = t.Tone
            }).ToList();
            DataProvider.SaveErrorToSQL(null, error);
        }

        public class TovDodProp
        {
            public int codetvun { get; set; }
            public string coden { get; set; }
            public double Qty { get; set; }
            public string Quality { get; set; }
            public string DateofMan { get; set; }
            public string ShelfLife { get; set; }
            public string Part { get; set; }
            public string Calibre { get; set; }
            public string Tone { get; set; }
        }

        public static void GetRemains()
        {
            var getRemains = new GetRemains()
            {
                WareHouseCode = wareHouseCode,
                Free = false,
                ShowServiceCells = false,
                MSWarehouseCodeArray = new List<MSWarehouseCodeArray>(),
                Date = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(getRemains, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTGetRemains", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(null, error, "SendPost");
            var remains = result.Deserialize<List<Remain>>(out error);
            DataProvider.SaveErrorToSQL(null, error, "Deserialize");
            DataProvider.AddRemainsToBD(remains);
        }

        public static void GetRemainsDodProp()
        {
            var getRemains = new GetRemains()
            {
                WareHouseCode = wareHouseCode,
                Free = true,
                ShowServiceCells = false,
                MSWarehouseCodeArray = new List<MSWarehouseCodeArray>(),
                Date = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(getRemains, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTGetRemains", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(null, error, "SendPost");
            var remains = result.Deserialize<List<Remain>>(out error);
            //var remainsDodProp = remains.Where(r => !(r.Tone == null && r.Part == null && r.Calibre == null)).ToList();
            DataProvider.SaveErrorToSQL(null, error, "Deserialize");
            DataProvider.AddRemainsDodPropToBD(remains);
        }

        public static void SendRoute(int route, string place)
        {
            var data = new RestRouteSheet();
            data.WareHouseCode = wareHouseCode;
            data.RouteSheet = DataProvider.GetRoute(route, place);
            if (data.RouteSheet == null) return;
            var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var result = RequestData.SendPost(baseUrl + "POSTRouteSheet", usernameAPI, passwordAPI, json, out string error);
            DataProvider.SaveErrorToSQL(null, error, $"route = {route}");
            DataProvider.SaveErrorToSQL(null, result.CheckOnError());
        }

        public static void SendGoodOnSelect(string query)
        {
            var data = new InfoRestOfGoods();
            data.WareHouseCode = wareHouseCode;
            data.Info = DataProvider.GetGoodOnSelect(query);
            if (data.Info.Products.Count != 0)
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var result = RequestData.SendPost(baseUrl + "POSTInfoRestOfGoods", usernameAPI, passwordAPI, json, out string error);
                DataProvider.SaveErrorToSQL(null, error);
                DataProvider.SaveErrorToSQL(null, result.CheckOnError());
            }
        }
    }
}
