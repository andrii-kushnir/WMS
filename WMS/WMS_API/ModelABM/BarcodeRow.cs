using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class BarcodeRow
    {
        /// <summary>
        /// Значение штрихкода
        /// </summary>
        //[StringLength(50)]
        public string Barcode { get; set; }

        /// <summary>
        /// Уникальный идентификатор единицы измерения
        /// </summary>
        public Guid GUIDPackaging { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GUIDProduct { get; set; }

        /// <summary>
        /// Тип штрихкода. Применяется для вариантов разбора:
        /// <br/>  * B0 – основной – сравнение производится по точному совпадению
        /// <br/>  * B1 – часть серийного номера с начала строки – для определения товара из серийного номера
        /// <br/>
        /// </summary>
        //[StringLength(2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public BarcodeType BarcodeType { get; set; }
    }

    public enum BarcodeType
    {
        [EnumMember(Value = @"B0")]
        B0 = 0,

        [EnumMember(Value = @"B1")]
        B1 = 1,
    }
}
