using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class ProductGroup
    {
        /// <summary>
        /// Наименование группы товара
        /// </summary>
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Уникальный идентификатор группы товара
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Идентификатор учетной системы
        /// </summary>
        [StringLength(100)]
        public string CodeMS { get; set; }

        /// <summary>
        /// Уникальный идентификатор родителя группы товара
        /// </summary>
        public string ParentGUID { get; set; }
    }

}
