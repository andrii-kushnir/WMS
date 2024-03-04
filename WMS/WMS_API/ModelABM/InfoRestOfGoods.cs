using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{

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

        public List<ProductSet> TableSetProducts { get; set; }
    }
}
