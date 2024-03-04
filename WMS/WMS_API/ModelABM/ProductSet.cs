using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class ProductSet
    {
        /// <summary>
        /// Уникальный идентификатор варианта комплектации
        /// </summary>
        public string SetGUID { get; set; }

        /// <summary>
        /// Наименование варианта комплектации
        /// </summary>
        public string SetDescription { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара, к которому привязан вариант комплектации
        /// </summary>
        public string GoodsGUID { get; set; }

        /// <summary>
        /// Уникальный идентификатор основного товара комплекта (из таблицы TableComponents — GoodsGUID)
        /// </summary>
        public string MainProductGUID { get; set; }

        /// <summary>
        /// Выходное количество комплекта
        /// </summary>
        public float SetQty { get; set; } = 1;

        public List<SetComponent> TableComponents { get; set; }
    }

    public class SetComponent
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GoodsGUID { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string GoodsDescription { get; set; }

        /// <summary>
        /// Уникальный идентификатор единицы измерения
        /// </summary>
        public string GUIDPackaging { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public float Qty { get; set; }
    }


}
