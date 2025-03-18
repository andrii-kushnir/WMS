//using System;
//using System.Collections.Generic;
//using System.Runtime.Serialization;
//using System.Xml.Serialization;

//namespace WMS_API_Nakl.ModelNakl
//{
//    /// <summary>
//    /// Тип накладної:
//    /// <br/>* OK – добре
//    /// <br/>*EXCESS – Накладна надлишок
//    /// <br/>*LACK – Накладна недостача
//    /// </summary>
//    public enum TypeNakl
//    {
//        //Накладна співпадає
//        // [EnumMember(Value = @"OK")]
//        OK = 0,
//        //Накладна надлишок
//        //[EnumMember(Value = @"EXCESS")]
//        Надлишок = 1,
//        //Накладна недостача
//        //[EnumMember(Value = @"LACK")]
//        Недостача = 2
//    }
//    public class NPrihOUT
//    {
//        [XmlAttribute(AttributeName = "T")]
//        public TypeNakl TypeNakl;

//        [XmlAttribute]
//        public string Coden { get; set; }
//        [XmlAttribute]
//        public string Codetvun { get; set; }
//        [XmlAttribute(AttributeName = "Kol")]
//        public double Qty { get; set; }
//        [XmlAttribute(AttributeName = "KolN")]
//        public double OriginalQty { get; set; }
//        [XmlAttribute]
//        public string Prompt { get; set; }
//        [XmlAttribute]
//        public string TypeOp { get; set; }
//        [XmlAttribute]
//        public string Quality { get; set; }
//        [XmlAttribute(AttributeName = "IsProb")]
//        public int flag;
//        [XmlAttribute(AttributeName = "GN")]
//        public string GUID;
//        [XmlAttribute(AttributeName = "G")]
//        public string GUIDDoc;
//        [XmlAttribute(AttributeName = "V")]
//        public string Weight;
//        [XmlAttribute(AttributeName = "N")]
//        public string NameDoc;
//        [XmlAttribute(AttributeName = "P")]
//        public string NumberDocIn;
//        [XmlIgnore]
//        public string GUIDentry;
//        [XmlIgnore]
//        public string GUIDorder;
//        [XmlAttribute(AttributeName = "ST")]
//        public string State;

//    }

//    public class TovDodProp
//    {
//        [XmlAttribute]
//        public int codetvun { get; set; }
//        [XmlAttribute]
//        public string coden { get; set; }
//        [XmlAttribute(AttributeName = "Kol")]
//        public double Qty { get; set; }
//        [XmlAttribute(AttributeName = "Qua")]
//        public string Quality { get; set; }
//        [XmlAttribute(AttributeName = "DoM")]
//        public string DateofMan { get; set; }
//        [XmlAttribute(AttributeName = "SL")]
//        public string ShelfLife { get; set; }
//        [XmlAttribute(AttributeName = "P")]
//        public string Part { get; set; }
//        [XmlAttribute(AttributeName = "C")]
//        public string Calibre { get; set; }
//        [XmlAttribute(AttributeName = "T")]
//        public string Tone { get; set; }
//    }
//    public class NPrihOUTList
//    {
//        public List<NPrihOUT> nPrihs;
//        public List<TovDodProp> TovDodProps;
//        public NPrihOUTList()
//        {
//            this.nPrihs = new List<NPrihOUT>();
//            TovDodProps = new List<TovDodProp>();
//        }
//        public NPrihOUTList(PostInvoice postInvoice, string prompt = "", POSTInfoOnOrderIn pOSTInfoOnOrderIn = null) : this()
//        {

//            string coden = "";
//            string nameDoc = "";
//            string numberDocIn = "";
//            string typeOp = "";
//            int flag1 = 0;
//            string gUIDentry = pOSTInfoOnOrderIn.GUIDentry;
//            string gUIDDoc = pOSTInfoOnOrderIn.GUIDorder;
//            string gUIDorder = pOSTInfoOnOrderIn.GUIDorder;
//            string state = "";


//            Tableproduct[] tableProduct = null;
//            if (postInvoice.Invoice != null)
//            {
//                if (postInvoice.Invoice.NameDoc.Contains("Повернення через ТЗД"))
//                {
//                    coden = postInvoice.Invoice.GUIDOrderSource;
//                    prompt = "ТЗД.ПОВ." + prompt;
//                }
//                else
//                { coden = postInvoice.Invoice.NumberDoc; }
//                tableProduct = postInvoice.Invoice.TableProduct;
//                typeOp = postInvoice.Invoice.TypeOperation;
//                flag1 = 0;
//                nameDoc = postInvoice.Invoice.NameDoc;
//                numberDocIn = postInvoice.Invoice.NumberDocIn;
//                state = postInvoice.Invoice.State;
//            }
//            if (postInvoice.Problemsituations != null)
//            {
//                coden = postInvoice.Problemsituations.NumberDoc;
//                tableProduct = postInvoice.Problemsituations.TableProduct;
//                typeOp = postInvoice.Problemsituations.NameProblemSituation;
//                flag1 = 1;
//                prompt = postInvoice.Problemsituations.Comment;
//                nameDoc = postInvoice.Problemsituations.NameDoc;
//                numberDocIn = "";
//                state = postInvoice.Problemsituations.State;
//            }

//            if ((postInvoice.Invoice != null) && (postInvoice.Invoice.NumberDoc == "0000044221253"))
//            {
//                Console.WriteLine(postInvoice.Invoice.NumberDoc);
//            }

//            // nPrihs = new List<NPrihOUT>();
//            foreach (var item in tableProduct)
//            {
//                {
//                    int tovar = nPrihs.FindIndex(x => x.Codetvun == item.GUIDProduct && x.Coden == coden
//                                                && x.TypeOp == typeOp && x.Quality == item.Quality);
//                    if (tovar >= 0)
//                    {
//                        nPrihs[tovar].Qty += item.Qty;
//                        nPrihs[tovar].TypeNakl = SetTypeNakl(nPrihs[tovar]);
//                        nPrihs[tovar].Weight = GetWidth(postInvoice.Invoice, item.GUIDProduct);
//                        //    Console.WriteLine($"{item.GUIDProduct}, {coden}, {typeOp}");
//                    }
//                    else
//                    {
//                        // Це якщо складений номер 12345.102
//                        if (gUIDDoc.Contains(".")) { gUIDDoc = gUIDDoc.Substring(0, gUIDDoc.IndexOf(".")); }
//                        // Це якщо зробили на ТЗД Guid  робимо coden =0
//                        if (gUIDDoc.Length == 36) { gUIDDoc = "0"; }

//                        nPrihs.Add(new NPrihOUT()
//                        {
//                            TypeNakl = SetTypeNakl(item),
//                            Coden = coden,
//                            Codetvun = item.GUIDProduct,
//                            Qty = item.Qty,
//                            OriginalQty = item.OriginalQty,
//                            Prompt = prompt,
//                            TypeOp = typeOp,
//                            Quality = item.Quality,
//                            flag = flag1,
//                            GUID = gUIDorder,
//                            GUIDDoc = gUIDDoc,
//                            Weight = GetWidth(postInvoice.Invoice, item.GUIDProduct),
//                            NameDoc = nameDoc,
//                            NumberDocIn = numberDocIn,
//                            GUIDentry = gUIDentry,
//                            State = state,
//                            GUIDorder = gUIDorder

//                        });
//                    }
//                }

//            }
//            //Додаван6ня додаткових властивостей товару:
//            if (postInvoice?.Invoice?.TypeOperation != null && postInvoice.Invoice.TypeOperation == "OUT")
//                TovDodProps = postInvoice.Invoice.TableProduct.Where(p => p.Part != null || p.Calibre != null || p.Tone != null || p.ShelfLife > new DateTime(2020, 01, 01) || p.DateOfManufacture > new DateTime(2020, 01, 01) || (p.Quality != "Кондиция" && p.Quality != "")).Select(t => new TovDodProp()
//                {
//                    codetvun = Convert.ToInt32(t.GUIDProduct),
//                    coden = coden,
//                    Qty = t.Qty,
//                    Quality = t.Quality,
//                    DateofMan = t.DateOfManufacture.DateToSQL(),
//                    ShelfLife = t.ShelfLife.DateToSQL(),
//                    Part = t.Part,
//                    Calibre = t.Calibre,
//                    Tone = t.Tone
//                }).ToList();

//        }
//        private TypeNakl SetTypeNakl(Tableproduct tableproduct)
//        {
//            if (tableproduct.Qty - tableproduct.OriginalQty == 0)
//            { return TypeNakl.OK; }
//            if (tableproduct.Qty - tableproduct.OriginalQty > 0)
//            { return TypeNakl.Надлишок; }
//            else
//            { return TypeNakl.Недостача; }

//        }
//        private TypeNakl SetTypeNakl(NPrihOUT tableproduct)
//        {
//            if (tableproduct.Qty - tableproduct.OriginalQty == 0)
//            { return TypeNakl.OK; }
//            if (tableproduct.Qty - tableproduct.OriginalQty > 0)
//            { return TypeNakl.Надлишок; }
//            else
//            { return TypeNakl.Недостача; }

//        }
//        private string GetWidth(Invoice invoice, string gUIDProduct)
//        {
//            if (invoice == null || invoice.Packing == null)
//                return "";
//            int tovarIndex = Array.FindIndex(invoice.Packing, x => x.GUIDProduct == gUIDProduct);
//            if (tovarIndex >= 0)
//            { return invoice.Packing[tovarIndex].Weight.ToString().Replace(",", "."); }

//            return "";
//        }
//    }
//}

