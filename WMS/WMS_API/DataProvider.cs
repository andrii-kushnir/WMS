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

        public static Guid thing = new Guid("ccaa55cf-5ab6-4c67-b4f5-93dccf4e1136");
        public static Guid m2 = new Guid("ee237fa7-98af-425d-a610-d1bbcd7ba04c");
        public static Guid kg = new Guid("db6c6a21-fbfe-4c52-bcd0-c7e589ca6df4");
        public static Guid sh100 = new Guid("db6c6a21-fbfe-4c52-bcd0-c7e589ca6df4");
        public static Guid box = new Guid("a7966abf-ffe9-4f7a-aa9c-f83d835f40f2");

        public static void SaveErrorToSQL(string error)
        {
            if (!String.IsNullOrWhiteSpace(error))
            {
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
                Guid packingForBarcode = Guid.Empty;
                var query = $"EXECUTE [us_GetGood] {codetvun}";
                var command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var pakingGUID = Guid.NewGuid();
                        packingForBarcode = pakingGUID;

                        result.Products.Add(new Product()
                        {
                            GUIDProduct = Convert.ToInt32(reader["codetvun"]).ToString(),
                            CodeMS = Convert.ToInt32(reader["codetv"]).ToString(),
                            Description = Convert.ToString(reader["nametv"]),
                            FullDescription = "",
                            AdditionalDescription = "",
                            ParentGUID = Convert.ToInt32(reader["nParent"]).ToString(),
                            Article = Convert.ToInt32(reader["codetvun"]).ToString(),
                            GUIDPackaging = pakingGUID,
                            Type = ProductType._1,
                            MinShipGUIDPackaging = pakingGUID,
                            QuantityOnPallet = 0
                        });

                        int codepl = Convert.ToInt32(reader["codepl"]);
                        switch (codepl)
                        {
                            case 0:
                                result.Products[0].Kind = "";
                                break;
                            case 2:
                                result.Products[0].Kind = "3";
                                break;
                            case 6:
                                result.Products[0].Kind = "13";
                                break;
                            default:
                                result.Products[0].Kind = codepl.ToString();
                                break;
                        }

                        packing = new Packing()
                        {
                            GUIDPackaging = pakingGUID,
                            GUIDProduct = result.Products[0].GUIDProduct,
                            Coef = 1,
                            Height = Convert.ToSingle(reader["height"]),
                            Width = Convert.ToSingle(reader["width"]),
                            Depth = Convert.ToSingle(reader["tovlength"]),
                            Weight = Convert.ToSingle(reader["vaga"]),
                            Capacity = Convert.ToSingle(reader["volume"]),
                            Basic = true
                        };
                        if (packing.Capacity == 0)
                            packing.Capacity = packing.Height * packing.Width * packing.Depth;
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
                        packing.Description = Convert.ToString(reader["ovname"]);
                        packing.GUIDClassifierPackage = new Guid(Convert.ToString(reader["guid"]));
                        //result.Products[0].Type = Convert.ToInt32(reader["type"]); - тут можна вказати тип товару, якщо треба буде!!!

                    }
                    else
                    {
                        SaveErrorToSQL("Одиниці виміру(ov) для товару не знайдені");
                        return result;
                    }

                    // ----------------- Фасовка -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        var pvu = Convert.ToSingle(reader["pvu"]);
                        var kvu = Convert.ToInt32(reader["kvu"]);

                        if (packing.GUIDClassifierPackage == m2 && pvu != kvu) // це тип плитка
                        {
                            float coef = ((float)kvu) / pvu;
                            var packingBase = new Packing()
                            {
                                GUIDPackaging = Guid.NewGuid(),
                                Description = "шт",
                                GUIDProduct = result.Products[0].GUIDProduct,
                                GUIDClassifierPackage = thing,
                                Coef = 1,
                                Height = packing.Height / coef,
                                Width = packing.Weight,
                                Depth = packing.Depth,
                                Weight = packing.Weight / coef,
                                Capacity = packing.Capacity / coef,
                                Basic = true
                            };
                            var packingDod = new Packing()
                            {
                                GUIDPackaging = Guid.NewGuid(),
                                Description = "ящик",
                                GUIDProduct = result.Products[0].GUIDProduct,
                                GUIDClassifierPackage = box,
                                Coef = kvu,
                                Height = 0,
                                Width = 0,
                                Depth = 0,
                                Weight = 0,
                                Capacity = 0,
                                Basic = false
                            };
                            result.Products[0].GUIDPackaging = packingBase.GUIDPackaging;
                            result.Products[0].MinShipGUIDPackaging = packingBase.GUIDPackaging;
                            packingForBarcode = packingDod.GUIDPackaging;
                            packing.Basic = false;
                            packing.Coef = coef;
                            result.Packing.Add(packingBase);
                            result.Packing.Add(packingDod);
                        }

                        if (packing.GUIDClassifierPackage == kg || packing.GUIDClassifierPackage == sh100)  // тип ящик (наприклад цвяхів) 
                        {
                            var packingDod = new Packing()
                            {
                                GUIDPackaging = Guid.NewGuid(),
                                Description = "ящик",
                                GUIDProduct = result.Products[0].GUIDProduct,
                                GUIDClassifierPackage = box,
                                Coef = pvu,
                                Height = 0,
                                Width = 0,
                                Depth = 0,
                                Weight = 0,
                                Capacity = 0,
                                Basic = false
                            };
                            packingForBarcode = packingDod.GUIDPackaging;
                            result.Packing.Add(packingDod);
                        }

                        if (packing.GUIDClassifierPackage != m2 && packing.GUIDClassifierPackage != kg && packing.GUIDClassifierPackage != sh100 && pvu == kvu)
                        {
                            result.Products[0].QuantityOnPallet = pvu;

                            //додовання палети(якщо треба):
                            //var packingDod = new Packing()
                            //{
                            //    GUIDPackaging = Guid.NewGuid(),
                            //    Description = "палета",
                            //    GUIDProduct = result.Products[0].GUIDProduct,
                            //    GUIDClassifierPackage = Guid(палета),
                            //    Coef = pvu,
                            //    Height = 0,
                            //    Width = 0,
                            //    Depth = 0,
                            //    Weight = 0,
                            //    Capacity = 0,
                            //    Basic = false
                            //};
                            //result.Packing.Add(packingDod);
                        }
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
                                GUIDProduct = result.Products[0].GUIDProduct,
                                BarcodeType = BarcodeType.B0
                            };
                            result.BarcodeTable.Add(barcode);
                        }
                    }

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
                            result.Products[0].ABCClassifier = ProductABCClassifier.A;
                            break;
                        case "B":
                            result.Products[0].ABCClassifier = ProductABCClassifier.B;
                            break;
                        default:
                            result.Products[0].ABCClassifier = ProductABCClassifier.C;
                            break;
                    }

                    // ----------------- Термін придатності -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        var minDay = Convert.ToInt32(reader["TminDay"]);
                        var maxDay = Convert.ToInt32(reader["TMaxDay"]);

                        if (minDay >1 && maxDay > 1)
                        {
                            result.Products[0].ShelfLifeMode = true;
                            result.Products[0].StoragePeriodInDays = maxDay.ToString();
                            result.Products[0].AllowableReceiptPercentageShelfLife = 100 * (float)minDay / maxDay;
                        }
                    }

                    // ----------------- Партія -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        result.Products[0].Part = true;
                    }

                    // ----------------- Калібр -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        result.Products[0].Calibre = true;
                    }

                    // ----------------- Тон -------------------
                    reader.NextResult();
                    if (reader.Read())
                    {
                        result.Products[0].Tone = true;
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
                    SaveErrorToSQL(ex.Message);
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

        public static List<int> GetSubgroups(int group)
        {
            var result = new List<int>();
            using (var connection = new SqlConnection(connectionSql101))
            {
                var query = $"SELECT * FROM [192.168.4.4].[Sk1].[dbo].[TGrups] WHERE nparent = {group}";
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
                    SaveErrorToSQL(ex.Message);
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
                            GUIDClassifierPackage = new Guid(Convert.ToString(reader["guid"])),
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
                var query = $"SELECT * FROM [192.168.4.4].[Sk1].[dbo].[TGrups] WHERE levelsort = {level}";
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
                    SaveErrorToSQL(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
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
