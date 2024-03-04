using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_API.ModelABM;

namespace WMS_API
{
    public static class DataProvider
    {
        //private const string connectionSql101 = "Context Connection = true;";
        private const string connectionSql101 = @"Server=192.168.4.101; Database=erp; uid=КушнірА; pwd=зщшфтв;";
        private const string connectionSql111 = @"Server=192.168.4.111; Database=WMS; uid=sa; pwd=Kwkx15Rp37;";
        private const string connectionSql4 = @"Server=192.168.4.4; Database=Sk1; uid=WMS; pwd=Akrt1689;";

        //private const string connectionSql101 = @"Server=192.168.4.101; Database=erp; uid=sa; pwd=Yi*7tg8tc=t?PjM;";

        //public static Guid thing = new Guid("ccaa55cf-5ab6-4c67-b4f5-93dccf4e1136");
        //public static Guid m2 = new Guid("ee237fa7-98af-425d-a610-d1bbcd7ba04c");
        //public static Guid kg = new Guid("db6c6a21-fbfe-4c52-bcd0-c7e589ca6df4");
        //public static Guid sh100 = new Guid("db6c6a21-fbfe-4c52-bcd0-c7e589ca6df4");
        //public static Guid box = new Guid("a7966abf-ffe9-4f7a-aa9c-f83d835f40f2");

        public static int thing = 18;
        public static int m2 = 3;
        public static int kg = 7;
        public static int sh100 = 25;
        public static int box = 26;

        public static void SaveErrorToSQL(string error, string param = null)
        {
            if (!String.IsNullOrWhiteSpace(error))
            {
                if (!String.IsNullOrWhiteSpace(param))
                    error = param + "; " + error;
                var methodName = new StackTrace(1).GetFrame(0).GetMethod().Name;
                using (SqlConnection connection = new SqlConnection(connectionSql101))
                {
                    connection.Open();
                    var sql = $@"INSERT INTO [erp].[dbo].[APIErrorLog] (method, error, date) VALUES ('{methodName}', '{error.Ekran()}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    using (var query = new SqlCommand(sql, connection))
                        query.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static InfoInfoRestOfGoods GetOneGood(int codetvun)
        { 
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                Packing packing = null;
                Product product = null;
                int ovId = 0;
                int codetv = 0;
                string packingForBarcode = null;
                var query = $"EXECUTE [us_GetGood] {codetvun}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        codetv = Convert.ToInt32(reader["codetv"]);
                        var description = new string(Convert.ToString(reader["nametv"]).Where(c => !char.IsControl(c)).ToArray());
                        var isToneCalibrPart = reader["isToneCalibrPart"] == System.DBNull.Value ? false : true;
                        product = new Product()
                        {
                            GUIDProduct = Convert.ToInt32(reader["codetvun"]).ToString(),
                            CodeMS = Convert.ToInt32(reader["codetvun"]).ToString(),
                            Description = description,
                            FullDescription = "",
                            AdditionalDescription = "",
                            ParentGUID = Convert.ToInt32(reader["nParent"]).ToString(),
                            Article = codetv.ToString(),
                            Type = ProductType._1,
                            QuantityOnPallet = 0,
                            ShelfLifeMode = reader["isShelfLifeMode"] == System.DBNull.Value ? false : true,
                            Part = isToneCalibrPart,
                            Calibre = isToneCalibrPart,
                            Tone = isToneCalibrPart
                        };
                        result.Products.Add(product);

                        int codepl = Convert.ToInt32(reader["codepl"]);
                        switch (codepl)
                        {
                            case 0:
                            case 17:
                            case 18:
                            case 19:
                            case 2:
                            case 15:
                            case 16:
                                product.Kind = "";
                                break;
                            default:
                                product.Kind = codepl.ToString();
                                break;
                        }

                        packing = new Packing()
                        {
                            GUIDProduct = product.GUIDProduct,
                            Coef = 1,
                            Height = Convert.ToSingle(reader["height"]),
                            Width = Convert.ToSingle(reader["width"]),
                            Depth = Convert.ToSingle(reader["tovlength"]),
                            Weight = Convert.ToSingle(reader["vaga"]),
                            Capacity = Convert.ToSingle(reader["volume"]),
                            Basic = true
                        };
                        result.Packing.Add(packing);
                    }
                    else
                    {
                        SaveErrorToSQL("Товар не знайдений");
                        return result;
                    }

                    // ----------------- Класифікатор одиниць вимірювання -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        ovId = Convert.ToInt32(reader["id"]);
                        var pakingGuid = ExtensionSQL.PackingCode(codetvun, ovId);
                        product.GUIDPackaging = pakingGuid;
                        product.MinShipGUIDPackaging = pakingGuid;
                        packingForBarcode = pakingGuid;
                        packing.GUIDPackaging = pakingGuid;
                        packing.GUIDClassifierPackage = ovId.ToString("000");
                        packing.Description = Convert.ToString(reader["ovname"]);
                        product.Type = (ProductType)Convert.ToInt32(reader["type"]);
                    }
                    else
                    {
                        SaveErrorToSQL("Одиниці виміру(ov) для товару не знайдені");
                        return result;
                    }

                    // ----------------- Термін придатності + ВГХ -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        var minDay = reader["TminDay"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["TminDay"]);
                        var maxDay = reader["TMaxDay"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["TMaxDay"]);

                        if (minDay > 1 && maxDay > 1)
                        {
                            product.ShelfLifeMode = true;
                            product.StoragePeriodInDays = maxDay.ToString();
                            product.AllowableReceiptPercentageShelfLife = 100 * (float)minDay / maxDay;
                        }

                        var Cbrutto = reader["Cbrutto"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["Cbrutto"]);
                        var Cvol = reader["Cvol"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["Cvol"]);
                        var Cwidt = reader["Cwidt"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["Cwidt"]);
                        var Cleng = reader["Cleng"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["Cleng"]);
                        var CHeig = reader["CHeig"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["CHeig"]);

                        if (Cvol == 1 && Cwidt == 1 && Cleng == 1 && CHeig == 1)
                        {
                            Cvol = 0; Cwidt = 0; Cleng = 0; CHeig = 0;
                        }
                        if (packing.Height == 0 && Cleng != 0) packing.Height = Cleng;
                        if (packing.Width == 0 && Cwidt != 0) packing.Width = Cwidt;
                        if (packing.Depth == 0 && CHeig != 0) packing.Depth = CHeig;
                        if (packing.Capacity == 0 && Cvol != 0) packing.Capacity = Cvol;
                        if (packing.Weight == 0 && Cbrutto != 0) packing.Weight = Cbrutto;
                    }

                    // ----------------- Фасовка -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        var pvu = Convert.ToSingle(reader["pvu"]);
                        var kvu = Convert.ToInt32(reader["kvu"]);

                        var packings = FasovkaExpansion(packing, ovId, pvu, kvu, ref packingForBarcode, product);
                        result.Packing.AddRange(packings);

                        product.QuantityOnPallet = reader["npallet"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["npallet"]);
                    }

                    // ----------------- Штрих-коди -------------------
                    reader.NextResult();
                    while (reader.Read())
                    {
                        var scancode = Convert.ToString(reader["scancode"]);
                        if (!String.IsNullOrEmpty(scancode))
                        {
                            var barcode = new BarcodeRow()
                            {
                                Barcode = scancode,
                                GUIDPackaging = packingForBarcode,
                                GUIDProduct = product.GUIDProduct,
                                BarcodeType = BarcodeType.B0
                            };
                            result.BarcodeTable.Add(barcode);
                        }
                    }
                    var barcodeARC = new BarcodeRow()
                    {
                        Barcode = "ARC" + codetv.ToString(),
                        GUIDPackaging = packingForBarcode,
                        GUIDProduct = product.GUIDProduct,
                        BarcodeType = BarcodeType.B0
                    };
                    result.BarcodeTable.Add(barcodeARC);

                    // -------------------- ABC ----------------------
                    string ABC = "";
                    reader.NextResult();
                    if (reader.Read())
                    {
                        ABC = Convert.ToString(reader["kolABC"]);
                    }
                    switch (ABC)
                    {
                        case "A":
                            product.ABCClassifier = ProductABCClassifier.A;
                            break;
                        case "B":
                            product.ABCClassifier = ProductABCClassifier.B;
                            break;
                        default:
                            product.ABCClassifier = ProductABCClassifier.C;
                            break;
                    }

                    // ----------------- Партія -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        product.Part = true;
                    }

                    // ----------------- Калібр -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        product.Calibre = true;
                    }

                    // ----------------- Тон -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        product.Tone = true;
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message, $"codetvun = {codetvun}");
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetGroupGood(int group)
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            var listTovar = new List<int>();
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"SELECT codetvun FROM [192.168.4.4].[Sk1].[dbo].[Tovar] WHERE nParent = {group}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var code = Convert.ToInt32(reader["codetvun"]);
                        listTovar.Add(code);
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message, $"group = {group}");
                }
                finally
                {
                    reader?.Close();
                }
            }
            foreach(var codetvun in listTovar)
            {
                var tovar = GetOneGood(codetvun);
                result.GroupsProducts.AddRange(tovar.GroupsProducts);
                result.ClassifierPackage.AddRange(tovar.ClassifierPackage);
                result.Products.AddRange(tovar.Products);
                result.Packing.AddRange(tovar.Packing);
                result.BarcodeTable.AddRange(tovar.BarcodeTable);
                result.TableProduct.AddRange(tovar.TableProduct);
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetGoodsPlace(int place)
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            var listTovar = new List<int>();
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"SELECT codetvun FROM [192.168.4.4].[Sk1].[dbo].[Tovar] WHERE codepl = {place}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var code = Convert.ToInt32(reader["codetvun"]);
                        listTovar.Add(code);
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message, $"place = {place}");
                }
                finally
                {
                    reader?.Close();
                }
            }
            foreach (var codetvun in listTovar)
            {
                var tovar = GetOneGood(codetvun);
                result.GroupsProducts.AddRange(tovar.GroupsProducts);
                result.ClassifierPackage.AddRange(tovar.ClassifierPackage);
                result.Products.AddRange(tovar.Products);
                result.Packing.AddRange(tovar.Packing);
                result.BarcodeTable.AddRange(tovar.BarcodeTable);
                result.TableProduct.AddRange(tovar.TableProduct);
            }
            return result;
        }

        public static List<int> GetSubgroups(int group)
        {
            var result = new List<int>();
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"SELECT * FROM [192.168.4.4].[Sk1].[dbo].[TGrups] WHERE ttype = 2 AND nparent = {group}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nkey = Convert.ToInt32(reader["nkey"]);
                        result.Add(nkey);
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message, $"group = {group}");
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetClassifierPackage()
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"SELECT * FROM [erp].[dbo].[ClassifierPackage]";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.ClassifierPackage.Add(new ClassifierPackage()
                        {
                            GUIDClassifierPackage = Convert.ToInt32(reader["id"]).ToString("000"),
                            Description = Convert.ToString(reader["ovname"]),
                            FullDescription = Convert.ToString(reader["ovfullname"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetGroupsProducts(int level)
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"SELECT * FROM [192.168.4.4].[Sk1].[dbo].[TGrups] WHERE ttype = 2 AND levelsort = {level}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nkey = Convert.ToString(reader["nkey"]);
                        var group = new ProductGroup()
                        {
                            Description = Convert.ToString(reader["namegr"]),
                            GUID = nkey,
                            CodeMS = nkey,
                            ParentGUID = Convert.ToString(reader["nparent"])
                        };
                        if (group.ParentGUID == "0")
                            group.ParentGUID = "1";
                        result.GroupsProducts.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message, $"level = {level}");
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }
        
        public static InfoInfoRestOfGoods GetChangeGroups()
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"EXECUTE [us_GetChangeGroups]";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nameop = Convert.ToString(reader["nameop"]);
                        if (nameop == "WMS")
                            continue;

                        var nkey = Convert.ToString(reader["nKey"]);
                        var group = new ProductGroup()
                        {
                            Description = Convert.ToString(reader["namegr"]),
                            GUID = nkey,
                            CodeMS = nkey,
                            ParentGUID = Convert.ToString(reader["nParent"])
                        };
                        if (group.ParentGUID == "0")
                            group.ParentGUID = "1";
                        result.GroupsProducts.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetChangeGoods()
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"EXECUTE [us_GetChangeGoods]";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string packingForBarcode = null;

                        var nameop = Convert.ToString(reader["nameop"]);
                        if (nameop == "WMS")
                            continue;

                        var codetv = Convert.ToInt32(reader["codetv"]);
                        var codetvun = Convert.ToInt32(reader["codetvun"]);
                        var description = new string(Convert.ToString(reader["nametv"]).Where(c => !char.IsControl(c)).ToArray());
                        var product = new Product()
                        {
                            GUIDProduct = codetvun.ToString(),
                            CodeMS = codetvun.ToString(),
                            Description = description,
                            FullDescription = "",
                            AdditionalDescription = "",
                            ParentGUID = Convert.ToInt32(reader["nParent"]).ToString(),
                            Article = codetv.ToString(),
                            Type = (ProductType)Convert.ToInt32(reader["typeOv"]),
                            QuantityOnPallet = reader["npallet"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["npallet"]),
                            ShelfLifeMode = reader["ShelfLifeMode"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["ShelfLifeMode"]) == 1 ? true : false),
                            StoragePeriodInDays = reader["StoragePeriodInDays"] == System.DBNull.Value ? "0" : Convert.ToInt32(reader["StoragePeriodInDays"]).ToString(),
                            AllowableReceiptPercentageShelfLife = reader["AllowableReceiptPercentageShelfLife"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["AllowableReceiptPercentageShelfLife"]),
                            Part = reader["Part"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["Part"]) == 1 ? true : false),
                            Calibre = reader["Calibre"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["Calibre"]) == 1 ? true : false),
                            Tone = reader["Tone"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["Tone"]) == 1 ? true : false)
                        };
                        result.Products.Add(product);

                        int codepl = Convert.ToInt32(reader["codepl"]);
                        switch (codepl)
                        {
                            case 0:
                            case 17:
                            case 18:
                            case 19:
                            case 2:
                            case 15:
                            case 16:
                                product.Kind = "";
                                break;
                            default:
                                product.Kind = codepl.ToString();
                                break;
                        }

                        var ovId = Convert.ToInt32(reader["ovid"]);
                        var pakingGuid = ExtensionSQL.PackingCode(codetvun, ovId);
                        product.GUIDPackaging = pakingGuid;
                        product.MinShipGUIDPackaging = pakingGuid;
                        packingForBarcode = pakingGuid;

                        var packing = new Packing()
                        {
                            GUIDPackaging = pakingGuid,
                            Description = Convert.ToString(reader["ovname"]),
                            GUIDProduct = product.GUIDProduct,
                            GUIDClassifierPackage = ovId.ToString("000"),
                            Coef = 1,
                            Height = Convert.ToSingle(reader["height"]),
                            Width = Convert.ToSingle(reader["width"]),
                            Depth = Convert.ToSingle(reader["tovlength"]),
                            Weight = Convert.ToSingle(reader["vaga"]),
                            Capacity = Convert.ToSingle(reader["volume"]),
                            Basic = true
                        };
                        result.Packing.Add(packing);

                        if (reader["pvu"] != System.DBNull.Value && reader["kvu"] != System.DBNull.Value)
                        {
                            var pvu = Convert.ToSingle(reader["pvu"]);
                            var kvu = Convert.ToInt32(reader["kvu"]);

                            var packings = FasovkaExpansion(packing, ovId, pvu, kvu, ref packingForBarcode, product);
                            result.Packing.AddRange(packings);
                        }

                        product.ABCClassifier = ProductABCClassifier.C;
                        if (reader["kolABC"] != System.DBNull.Value)
                        {
                            switch (Convert.ToString(reader["kolABC"]))
                            {
                                case "A":
                                    product.ABCClassifier = ProductABCClassifier.A;
                                    break;
                                case "B":
                                    product.ABCClassifier = ProductABCClassifier.B;
                                    break;
                                default:
                                    product.ABCClassifier = ProductABCClassifier.C;
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetChangeFasovka()
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"EXECUTE [us_GetChangeFasovka]";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string packingForBarcode = null;

                        var nameop = Convert.ToString(reader["nameop"]);
                        if (nameop == "WMS")
                            continue;

                        var codetv = Convert.ToInt32(reader["codetv"]);
                        var codetvun = Convert.ToInt32(reader["codetvun"]);
                        var description = new string(Convert.ToString(reader["nametv"]).Where(c => !char.IsControl(c)).ToArray());
                        var product = new Product()
                        {
                            GUIDProduct = codetvun.ToString(),
                            CodeMS = codetvun.ToString(),
                            Description = description,
                            FullDescription = "",
                            AdditionalDescription = "",
                            ParentGUID = Convert.ToInt32(reader["nParent"]).ToString(),
                            Article = codetv.ToString(),
                            Type = (ProductType)Convert.ToInt32(reader["typeOv"]),
                            QuantityOnPallet = reader["npallet"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["npallet"]),
                            ShelfLifeMode = reader["ShelfLifeMode"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["ShelfLifeMode"]) == 1 ? true : false),
                            StoragePeriodInDays = reader["StoragePeriodInDays"] == System.DBNull.Value ? "0" : Convert.ToInt32(reader["StoragePeriodInDays"]).ToString(),
                            AllowableReceiptPercentageShelfLife = reader["AllowableReceiptPercentageShelfLife"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["AllowableReceiptPercentageShelfLife"]),
                            Part = reader["Part"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["Part"]) == 1 ? true : false),
                            Calibre = reader["Calibre"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["Calibre"]) == 1 ? true : false),
                            Tone = reader["Tone"] == System.DBNull.Value ? false : (Convert.ToInt32(reader["Tone"]) == 1 ? true : false)
                        };
                        result.Products.Add(product);

                        int codepl = Convert.ToInt32(reader["codepl"]);
                        switch (codepl)
                        {
                            case 0:
                            case 17:
                            case 18:
                            case 19:
                            case 2:
                            case 15:
                            case 16:
                                product.Kind = "";
                                break;
                            default:
                                product.Kind = codepl.ToString();
                                break;
                        }

                        var ovId = Convert.ToInt32(reader["ovid"]);
                        var pakingGuid = ExtensionSQL.PackingCode(codetvun, ovId);
                        product.GUIDPackaging = pakingGuid;
                        product.MinShipGUIDPackaging = pakingGuid;
                        packingForBarcode = pakingGuid;

                        var packing = new Packing()
                        {
                            GUIDPackaging = pakingGuid,
                            Description = Convert.ToString(reader["ovname"]),
                            GUIDProduct = product.GUIDProduct,
                            GUIDClassifierPackage = ovId.ToString("000"),
                            Coef = 1,
                            Height = Convert.ToSingle(reader["height"]),
                            Width = Convert.ToSingle(reader["width"]),
                            Depth = Convert.ToSingle(reader["tovlength"]),
                            Weight = Convert.ToSingle(reader["vaga"]),
                            Capacity = Convert.ToSingle(reader["volume"]),
                            Basic = true
                        };
                        result.Packing.Add(packing);

                        if (reader["pvu"] != System.DBNull.Value && reader["kvu"] != System.DBNull.Value)
                        {
                            var pvu = Convert.ToSingle(reader["pvu"]);
                            var kvu = Convert.ToInt32(reader["kvu"]);

                            var packings = FasovkaExpansion(packing, ovId, pvu, kvu, ref packingForBarcode, product);
                            result.Packing.AddRange(packings);
                        }

                        product.ABCClassifier = ProductABCClassifier.C;
                        if (reader["kolABC"] != System.DBNull.Value)
                        {
                            switch (Convert.ToString(reader["kolABC"]))
                            {
                                case "A":
                                    product.ABCClassifier = ProductABCClassifier.A;
                                    break;
                                case "B":
                                    product.ABCClassifier = ProductABCClassifier.B;
                                    break;
                                default:
                                    product.ABCClassifier = ProductABCClassifier.C;
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static InfoInfoRestOfGoods GetChangeShufr()
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>()
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"EXECUTE [us_GetChangeShufr]";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var scancode = Convert.ToString(reader["scancode"]);
                        if (!String.IsNullOrEmpty(scancode))
                        {
                            var codetvun = Convert.ToInt32(reader["codetvun"]);
                            var ovId = Convert.ToInt32(reader["ovid"]);
                            var pvu = reader["pvu"] == System.DBNull.Value ? 0 : Convert.ToSingle(reader["pvu"]);
                            var kvu = reader["kvu"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["kvu"]);

                            string packingForBarcode;
                            if (ovId == m2 && pvu != kvu && pvu != 0 && kvu != 0) // це тип плитка
                                packingForBarcode = ExtensionSQL.PackingCode(codetvun, thing);
                            else
                                packingForBarcode = ExtensionSQL.PackingCode(codetvun, ovId);

                            var barcode = new BarcodeRow()
                            {
                                Barcode = scancode,
                                GUIDPackaging = packingForBarcode,
                                GUIDProduct = codetvun.ToString(),
                                BarcodeType = BarcodeType.B0
                            };
                            result.BarcodeTable.Add(barcode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static List<Packing> FasovkaExpansion(Packing packing, int ov, float pvu, int kvu, ref string packingForBarcode, Product product = null)
        {
            var result = new List<Packing>();

            var codetvun = packing.GUIDProduct;

            if (ov == m2 && pvu != kvu && pvu != 0 && kvu != 0) // це тип плитка
            {
                float coef = ((float)kvu) / pvu;
                var packingBase = new Packing()
                {
                    GUIDPackaging = ExtensionSQL.PackingCode(codetvun, thing),
                    Description = "шт",
                    GUIDProduct = codetvun,
                    GUIDClassifierPackage = thing.ToString("000"),
                    Coef = 1,
                    Height = packing.Height / coef,
                    Width = packing.Width,
                    Depth = packing.Depth,
                    Weight = packing.Weight / coef,
                    Capacity = packing.Capacity / coef,
                    Basic = true
                };
                result.Add(packingBase);
                var packingDod = new Packing()
                {
                    GUIDPackaging = ExtensionSQL.PackingCode(codetvun, box),
                    Description = "ящик",
                    GUIDProduct = codetvun,
                    GUIDClassifierPackage = box.ToString("000"),
                    Coef = kvu,
                    Height = 0,
                    Width = 0,
                    Depth = 0,
                    Weight = 0,
                    Capacity = 0,
                    Basic = false
                };
                result.Add(packingDod);
                if (product != null)
                {
                    product.Type = ProductType._1;
                    product.GUIDPackaging = packingBase.GUIDPackaging;
                    product.MinShipGUIDPackaging = packingBase.GUIDPackaging;
                }
                packingForBarcode = packingBase.GUIDPackaging;
                packing.Basic = false;
                packing.Coef = coef;
            }

            if (ov == kg || ov == sh100)  // тип ящик (наприклад цвяхів) 
            {
                var packingDod = new Packing()
                {
                    GUIDPackaging = ExtensionSQL.PackingCode(codetvun, box),
                    Description = "ящик",
                    GUIDProduct = codetvun,
                    GUIDClassifierPackage = box.ToString("000"),
                    Coef = pvu,
                    Height = 0,
                    Width = 0,
                    Depth = 0,
                    Weight = 0,
                    Capacity = 0,
                    Basic = false
                };
                result.Add(packingDod);
            }

            //if (ov != m2 && ov != kg && ov != sh100 && pvu == kvu)
            //{
            //    if (product != null)
            //    {
            //        product.QuantityOnPallet = pvu;
            //    }
            //}

            return result;
        }

        public static InfoInfoRestOfGoods GetSets()
        {
            var result = new InfoInfoRestOfGoods()
            {
                GroupsProducts = new List<ProductGroup>(),
                ClassifierPackage = new List<ClassifierPackage>(),
                Products = new List<Product>(),
                Packing = new List<Packing>(),
                BarcodeTable = new List<BarcodeRow>(),
                TableProduct = new List<ProductRow>(),
                TableSetProducts = new List<ProductSet>(),
            };
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"EXECUTE [us_GetSets]";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    ProductSet productSet;
                    InfoInfoRestOfGoods tovar;
                    var mainOld = 0;
                    var mainNameOld = "";
                    var set = new List<SetComponent>();
                    while (reader.Read())
                    {
                        var main = Convert.ToInt32(reader["main"]);
                        if (mainOld != 0 && mainOld != main && set.Count != 0)
                        {
                            tovar = GetOneGood(mainOld);
                            tovar.Products[0].IsSet = true;
                            result.Products.Add(tovar.Products[0]);

                            productSet = new ProductSet()
                            {
                                SetGUID = mainOld.ToString(),
                                SetDescription = mainNameOld,
                                GoodsGUID = mainOld.ToString(),
                                MainProductGUID = "",
                                SetQty = 1,
                                TableComponents = set
                            };
                            result.TableSetProducts.Add(productSet);
                            set = new List<SetComponent>();
                        }

                        mainOld = main;
                        mainNameOld = Convert.ToString(reader["mainName"]);

                        if (reader["kompl"] == System.DBNull.Value || reader["ov"] == System.DBNull.Value)
                            continue;

                        set.Add(new SetComponent()
                        {
                            GoodsGUID = Convert.ToInt32(reader["kompl"]).ToString(),
                            GoodsDescription = Convert.ToString(reader["komplName"]),
                            GUIDPackaging = ExtensionSQL.PackingCode(Convert.ToInt32(reader["kompl"]), Convert.ToInt32(reader["ov"])),
                            Qty = 1
                        });
                    }
                    tovar = GetOneGood(mainOld);
                    tovar.Products[0].IsSet = true;
                    result.Products.Add(tovar.Products[0]);

                    productSet = new ProductSet()
                    {
                        SetGUID = mainOld.ToString(),
                        SetDescription = mainNameOld,
                        GoodsGUID = mainOld.ToString(),
                        MainProductGUID = "",
                        SetQty = 1,
                        TableComponents = set
                    };
                    result.TableSetProducts.Add(productSet);
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static void AddModificationsToBD(ProductOnOrder product)
        {
            using (var connection = new SqlConnection(connectionSql101))
            {
                Packing basePacking;
                if (product.Packing.Count == 1)
                    basePacking = product.Packing[0];
                else
                {
                    //Цей варіант обробити!!! Потрібно витягнути одиницю виміру з ERP і взяти з WMS саме цю одиницю виміру:
                    basePacking = product.Packing.FirstOrDefault(p => p.Basic);
                }
                var query = $"INSERT INTO TmpModifications (codetvun, height, width, tovlength, vaga, volume, QuantityOnPallet) VALUES ({product.GUIDProduct}, {basePacking.Height}, {basePacking.Width}, {basePacking.Depth}, {basePacking.Weight}, {basePacking.Capacity}, {basePacking.Weight}, {product.QuantityOnPallet}, )";
#warning ДОРОБИТИ ТОНИ/КАЛІБРИ
                //DO query

                foreach (var barcode in product.BarcodeTable)
                {
                    if (barcode.Barcode.IndexOf("ARC") != 0)
                    {
                        query = $"INSERT INTO TmpModificationsShufr (codetvun, shufr) VALUES ({product.GUIDProduct}, '{barcode.Barcode}')";
                        //DO query
                    }
                }
            }
        }

        public static InfoRouteSheet GetRoute(int route)
        {
            var result = new InfoRouteSheet();

            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"EXECUTE [us_GetRouteSheet] {route}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result.GUIDRouteSheet = Convert.ToInt32(reader["codep"]).ToString();
                        result.DateDoc = Convert.ToDateTime(reader["dateNow"]);
                        result.Number = route.ToString();
                        result.VehicleGUID = Convert.ToInt32(reader["codet"]).ToString();
                        result.Vehicle = Convert.ToString(reader["namet"]);
                        result.CellOut = Convert.ToString(reader["place"]);
                        result.DateOut = Convert.ToDateTime(reader["daten"]);

                        result.TableOrder = new List<OrderRow>() {
                            new OrderRow(){ GUIDOrder = Convert.ToInt32(reader["codenNakl"]).ToString() }};

                        while (reader.Read())
                        {
                            var order = new OrderRow() 
                            {
                                GUIDOrder = Convert.ToInt32(reader["codenNakl"]).ToString()
                            };
                            result.TableOrder.Add(order);
                        }
                    }
                    else
                    {
                        SaveErrorToSQL("Маршрутний лист не знайдений");
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public static int CountTovarGroup(int group)
        {
            using (var connection = new SqlConnection(connectionSql4))
            {
                var query = $";WITH GroupLevel_n AS (SELECT nkey as parentmain, nkey, levelsort as level, namegr FROM TGrups WHERE nkey = " + group.ToString() + " UNION ALL SELECT Main.parentmain, T.nkey, T.levelsort, T.namegr FROM GroupLevel_n as Main, TGrups as T WHERE(Main.nkey = T.nparent) UNION ALL SELECT T.nparent, T.nkey, T.levelsort, T.namegr FROM GroupLevel_n as Main, TGrups as T WHERE(Main.nkey = T.nparent)	UNION ALL SELECT T.nkey, T.nkey, T.levelsort, T.namegr FROM GroupLevel_n as Main, TGrups as T WHERE(Main.nkey = T.nparent)) SELECT DISTINCT parentmain, nkey INTO #tmpGroup FROM GroupLevel_n SELECT COUNT(*) FROM #tmpGroup G, Tovar T WHERE T.nParent = G.nkey AND G.parentmain = " + group.ToString() + " DROP TABLE #tmpGroup";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteScalar();
                return (int)reader;
            }
        }

        public static int CountTovarPlace(int place)
        {
            using (var connection = new SqlConnection(connectionSql4))
            {
                var query = $"SELECT COUNT(*) FROM [Tovar].[dbo].[Tovar] where codepl = " + place.ToString() + "";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteScalar();
                return (int)reader;
            }
        }


        //public void SaveClientSQL(UserViber user)
        //{

        //    dateCreate = reader["dateCreate"] == System.DBNull.Value? new DateTime(2000, 1, 1) : Convert.ToDateTime(reader["dateCreate"]),
        //    inviteType = (InviteType) Convert.ToInt32(reader["inviteType"]),
        //    subscribed = Convert.ToBoolean(reader["subscribed"]),
        //    buhnetName = reader["namep"] == System.DBNull.Value? null : Convert.ToString(reader["namep"]),


        //    var name = Regex.Replace(user.Name, @"'", @"''");
        //    var query = $"INSERT INTO [dbo].[ArseniumViberClients] ([guid], [phone], [dateCreate], [viberId], [viberName], [inviteType], [subscribed], [avatar], [language], [country], [os], [device]) VALUES ('{user.Id}', '{user.phone}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{user.idViber}', '{name}', {(int)user.inviteType}, {(user.subscribed ? 1 : 0)}, '{user.Avatar}', '{user.language ?? ""}', '{user.country ?? ""}', '{user.primary_device_os ?? ""}', '{user.device_type ?? ""}')";
        //    Enqueue100(query);
        //}



    }
}
