using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                // ----------------- ТОВАР -------------------
                Packing packing = null;
                Guid packingForBarcode = Guid.Empty;
                var query = $"SELECT TOP 1 T.*, C.guid AS ovguid, C.ovname AS ovname, IIF(P.codepl = 0, '', P.namepl) AS namepl FROM [192.168.4.4].[Sk1].[dbo].[Tovar] T, [erp].[dbo].[ClassifierPackage] C, [192.168.4.4].[Sk1].[dbo].[Place] P WHERE codetvun = {codetvun} AND T.ov = C.ov AND T.codepl = P.codepl";
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
                            Brand = "",
                            Kind = Convert.ToString(reader["namepl"]),
                            Subkind = "",
                            Type = ProductType._1,
                            ShelfLifeMode = false,
                            SeriesMode = false,
                            SeriesNumberMode = false,
                            UniqueSNMode = false,
                            MinShipGUIDPackaging = pakingGUID,
                            NoBarcode = false,
                            QuantityOnPallet = 0,
                            IsSet = false,
                            SerialNumberType = ProductSerialNumberType.C,
                            HasPhoto = false,
                            PackingMaterial = "",
                            TemperatureModeFrom = 0,
                            TemperatureModeTo = 0
                        });

                        packing = new Packing()
                        {
                            GUIDPackaging = pakingGUID,
                            Description = Convert.ToString(reader["ovname"]),
                            GUIDProduct = result.Products[0].GUIDProduct,
                            GUIDClassifierPackage = new Guid(Convert.ToString(reader["ovguid"])),
                            Coef = 1,
                            Height = Convert.ToSingle(reader["height"]),
                            Width = Convert.ToSingle(reader["width"]),
                            Depth = Convert.ToSingle(reader["tovlength"]),
                            Weight = Convert.ToSingle(reader["vaga"]),
                            Capacity = Convert.ToSingle(reader["volume"]),
                            Basic = true,
                            Deprecated = false
                        };
                        if (packing.Capacity == 0)
                            packing.Capacity = packing.Height * packing.Width * packing.Depth;


                        //dateCreate = reader["dateCreate"] == System.DBNull.Value ? new DateTime(2000, 1, 1) : Convert.ToDateTime(reader["dateCreate"]),
                        //inviteType = (InviteType)Convert.ToInt32(reader["inviteType"]),
                        //subscribed = Convert.ToBoolean(reader["subscribed"]),
                        //buhnetName = reader["namep"] == System.DBNull.Value ? null : Convert.ToString(reader["namep"]),
                    }
                    else
                    {
                        //Logger.Error($"DataProvider.GetOneGood(): товару не знайдено!!");
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Error($"DataProvider.GetOneGood(): {ex.Message}");
                }
                finally
                {
                    reader?.Close();
                }

                // ----------------- ФАСОВКА -------------------
                query = $"SELECT TOP 1 * FROM [192.168.4.4].[Tovar].[dbo].[TFasovka] WHERE codetvun = {codetvun}";
                command = new SqlCommand(query, connection);
                try
                {
                    reader = command.ExecuteReader();
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
                                Basic = true,
                                Deprecated = false
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
                                Basic = false,
                                Deprecated = false
                            };
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
                                Basic = false,
                                Deprecated = false
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
                            //    Basic = false,
                            //    Deprecated = false
                            //};
                            //result.Packing.Add(packingDod);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Error($"DataProvider.GetOneGood(): {ex.Message}");
                }
                finally
                {
                    result.Packing.Add(packing);
                    reader?.Close();
                }

                // -------------------- ABC ----------------------
                query = $"SELECT TOP 1 * FROM [192.168.4.4].[Sk1].[dbo].[TovarABC_old] WHERE codetvun = {codetvun}";
                command = new SqlCommand(query, connection);
                try
                {
                    string ABC = "";
                    reader = command.ExecuteReader();
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
                }
                catch (Exception ex)
                {
                    //Logger.Error($"DataProvider.GetOneGood(): {ex.Message}");
                }
                finally
                {
                    reader?.Close();
                }

                // ----------------- Термін придатності -------------------
                query = $"SELECT TOP 1 * FROM [192.168.4.111].[WMS].[dbo].[Article] WHERE codetvun = {codetvun}";
                command = new SqlCommand(query, connection);
                try
                {
                    reader = command.ExecuteReader();
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
                }
                catch (Exception ex)
                {
                    //Logger.Error($"DataProvider.GetOneGood(): {ex.Message}");
                }
                finally
                {
                    reader?.Close();
                }

                // ----------------- Штрих-коди -------------------
                query = $"SELECT * FROM [192.168.4.4].[Tovar].[dbo].[Tovar_Shufr] WHERE codetvun = {codetvun}";
                command = new SqlCommand(query, connection);
                try
                {
                    reader = command.ExecuteReader();
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
                }
                catch (Exception ex)
                {
                    //Logger.Error($"DataProvider.GetOneGood(): {ex.Message}");
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
                    //Logger.Error($"DataProvider.GetGroupGood(): {ex.Message}");
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
                    //Logger.Error($"DataProvider.SetClassifierPackage(): {ex.Message}");
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
                    //Logger.Error($"DataProvider.SetClassifierPackage(): {ex.Message}");
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
                    //Logger.Error($"DataProvider.SetClassifierPackage(): {ex.Message}");
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
        //    var name = Regex.Replace(user.Name, @"'", @"''");
        //    var query = $"INSERT INTO [dbo].[ArseniumViberClients] ([guid], [phone], [dateCreate], [viberId], [viberName], [inviteType], [subscribed], [avatar], [language], [country], [os], [device]) VALUES ('{user.Id}', '{user.phone}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{user.idViber}', '{name}', {(int)user.inviteType}, {(user.subscribed ? 1 : 0)}, '{user.Avatar}', '{user.language ?? ""}', '{user.country ?? ""}', '{user.primary_device_os ?? ""}', '{user.device_type ?? ""}')";
        //    Enqueue100(query);
        //}



    }
}
