using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v12.0.0.0))")]
    public class InfoRestOfGoods
    {
        /// <summary>
        /// Код склада WMS
        /// </summary>
        public string WareHouseCode { get; set; }

        public InfoInfoRestOfGoods Info { get; set; }
    }


    public class InfoInfoRestOfGoods
    {
        public List<ProductGroup> GroupsProducts { get; set; }

        public List<ClassifierPackage> ClassifierPackage { get; set; }

        public List<Product> Products { get; set; }

        public List<Packing> Packing { get; set; }

        public List<BarcodeRow> BarcodeTable { get; set; }

        public List<ProductRow> TableProduct { get; set; }
    }
}
