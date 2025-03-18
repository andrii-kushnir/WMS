using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class InfoOnOrderInvoice
    {
        /// <summary>
        /// Код склада WMS
        /// </summary>
        public string WareHouseCode { get; set; }
        public Invoice Invoice { get; set; }
    }

    public class Invoice
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public List<ProductRow> TableProduct { get; set; }
        public string NumberDoc { get; set; }
    }
}
