using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class InfoOnOrder
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderModificationsTypeOperation TypeOperation { get; set; }

        public ProductOnOrder Product { get; set; }
    }


    public class ProductOnOrder
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GUIDProduct { get; set; }

        /// <summary>
        /// Идентификатор учетной системы
        /// </summary>
        //[StringLength(100)]
        public string CodeMS { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        //[StringLength(150)]
        public string Description { get; set; }

        /// <summary>
        /// Полное наименование товара (если используется)
        /// </summary>
        //[StringLength(250)]
        public string FullDescription { get; set; }

        /// <summary>
        /// Уникальный идентификатор группы товара
        /// </summary>
        public string ParentGUID { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        //[StringLength(40)]
        public string Article { get; set; }

        /// <summary>
        /// Уникальный идентификатор единицы измерения товара, который используется для минимальной единицы отгрузки
        /// </summary>
        public string MinShipGUIDPackaging { get; set; }

        /// <summary>
        /// Бренд производителя товара
        /// </summary>
        //[StringLength(150)]
        public string Brand { get; set; }

        /// <summary>
        /// Код категории ERP - используется для сопоставления категорий ERP-WMS
        /// </summary>
        //[StringLength(20)]
        public string Kind { get; set; }

        /// <summary>
        /// Код подкатегории ERP - используется для сопоставления подкатегорий ERP-WMS (код не должен пересекаться с кодами категории)
        /// </summary>
        //[StringLength(20)]
        public string Subkind { get; set; }

        /// <summary>
        /// Тип товара:
        /// <br/>  * 1 – штучный
        /// <br/>  * 2 – весовой
        /// <br/>  * 3 – штучный средневесовой
        /// <br/>  * 4 – отрезной
        /// <br/>
        /// </summary>
        //[StringLength(1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType Type { get; set; }

        /// <summary>
        /// Признак ведения учета по сериям
        /// </summary>
        public bool SeriesMode { get; set; }

        /// <summary>
        /// Признак ведения учета по срокам годности
        /// </summary>
        public bool ShelfLifeMode { get; set; }

        /// <summary>
        /// Признак ведения учета в разрезе серийных номеров
        /// </summary>
        public bool SeriesNumberMode { get; set; }

        /// <summary>
        /// Срок хранения в днях
        /// </summary>
        //[StringLength(15)]
        public int StoragePeriodInDays { get; set; }

        /// <summary>
        /// Класс оборачиваемости товара (ABC):
        /// <br/>  * A
        /// <br/>  * B
        /// <br/>  * C
        /// <br/>
        /// </summary>
        //[StringLength(1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductABCClassifier ABCClassifier { get; set; }

        /// <summary>
        /// Количество товара на паллете
        /// </summary>
        public float QuantityOnPallet { get; set; }

        public List<Packing> Packing { get; set; }

        public List<BarcodeRow> BarcodeTable { get; set; }
    }
}
