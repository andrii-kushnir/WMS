using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WMS_API.ModelABM
{
    public class Product
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GUIDProduct { get; set; }

        /// <summary>
        /// Идентификатор учетной системы
        /// </summary>
        [StringLength(100)]
        public string CodeMS { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        [StringLength(150)]
        public string Description { get; set; }

        /// <summary>
        /// Полное наименование товара (если используется)
        /// </summary>
        [StringLength(250)]
        public string FullDescription { get; set; }

        /// <summary>
        /// Дополнительное описание
        /// </summary>
        [StringLength(250)]
        public string AdditionalDescription { get; set; }

        /// <summary>
        /// Уникальный идентификатор группы товара
        /// </summary>
        public string ParentGUID { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        [StringLength(40)]
        public string Article { get; set; }

        /// <summary>
        /// Уникальный идентификатор единицы товара по умолчанию
        /// </summary>
        public Guid GUIDPackaging { get; set; }

        /// <summary>
        /// Бренд производителя товара
        /// </summary>
        [StringLength(150)]
        public string Brand { get; set; }

        /// <summary>
        /// Код категории ERP - используется для сопоставления категорий ERP-WMS
        /// </summary>
        [StringLength(20)]
        public string Kind { get; set; }

        /// <summary>
        /// Код подкатегории ERP - используется для сопоставления подкатегорий ERP-WMS (код не должен пересекаться с кодами категории)
        /// </summary>
        [StringLength(20)]
        public string Subkind { get; set; }

        /// <summary>
        /// Тип товара:
        /// <br/>  * 1 – штучный
        /// <br/>  * 2 – весовой
        /// <br/>  * 3 – штучный средневесовой
        /// <br/>  * 4 – отрезной
        /// <br/>
        /// </summary>
        [StringLength(1)]
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
        /// Признак ведения учета в разрезе уникальных серийных номеров
        /// </summary>
        public bool UniqueSNMode { get; set; }

        /// <summary>
        /// Срок хранения в днях
        /// </summary>
        [StringLength(15)]
        public string StoragePeriodInDays { get; set; }

        /// <summary>
        /// Уникальный идентификатор единицы измерения товара, который используется для минимальной единицы отгрузки
        /// </summary>
        public Guid MinShipGUIDPackaging { get; set; }

        /// <summary>
        /// Отключает верификацию товара сканированием штрихкода на ТСД
        /// </summary>
        public bool NoBarcode { get; set; } = false;

        /// <summary>
        /// Количество товара на паллете
        /// </summary>
        public float QuantityOnPallet { get; set; }

        /// <summary>
        /// Признак набора
        /// </summary>
        public bool IsSet { get; set; }

        /// <summary>
        /// Класс оборачиваемости товара (ABC):
        /// <br/>  * A
        /// <br/>  * B
        /// <br/>  * C
        /// <br/>
        /// </summary>
        [StringLength(1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductABCClassifier ABCClassifier { get; set; }

        /// <summary>
        /// Допустимый процент остатка срока годности (для прихода)
        /// </summary>
        public float AllowableReceiptPercentageShelfLife { get; set; }

        /// <summary>
        /// Тип серийного номера :
        /// <br/>  * U - уникальный (для новых марок)
        /// <br/>  * C - обычный (не используется)
        /// <br/>
        /// </summary>
        [StringLength(1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductSerialNumberType SerialNumberType { get; set; }

        /// <summary>
        /// Признак наличия фото
        /// </summary>
        public bool HasPhoto { get; set; }

        /// <summary>
        /// Материал упаковки
        /// </summary>
        [StringLength(150)]
        public string PackingMaterial { get; set; }

        /// <summary>
        /// Температурный режим (от)
        /// </summary>
        public float TemperatureModeFrom { get; set; }

        /// <summary>
        /// Температурный режим (до)
        /// </summary>
        public float TemperatureModeTo { get; set; }
    }

    public enum ProductType
    {
        [EnumMember(Value = @"1")]
        _1 = 0,

        [EnumMember(Value = @"2")]
        _2 = 1,

        [EnumMember(Value = @"3")]
        _3 = 2,

        [EnumMember(Value = @"4")]
        _4 = 3,
    }

    public enum ProductABCClassifier
    {
        [EnumMember(Value = @"A")]
        A = 0,

        [EnumMember(Value = @"B")]
        B = 1,

        [EnumMember(Value = @"C")]
        C = 2,
    }

    public enum ProductSerialNumberType
    {
        [EnumMember(Value = @"U")]
        U = 0,

        [EnumMember(Value = @"C")]
        C = 1,
    }
}
