using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class Remain
    {
        /// <summary>
        /// Название ячейки
        /// </summary>
        public string Cell { get; set; }

        /// <summary>
        /// Название зони
        /// </summary>
        public string ZoneCell { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GUIDProduct { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Код учетной системы
        /// </summary>
        public string CodeMS { get; set; }

        /// <summary>
        /// Серия товара
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// Срок годности товара
        /// </summary>
        public DateTime ShelfLife { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime DateOfManufacture { get; set; }

        /// <summary>
        /// Качество
        /// </summary>
        public string Quality { get; set; }

        /// <summary>
        /// Уникальный идентификатор упаковки
        /// </summary>
        public string GUIDPackingList { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public float Qty { get; set; }

        /// <summary>
        /// Идентификатор склада учетной системы
        /// </summary>
        public string MSWarehouseCode { get; set; }

        /// <summary>
        /// Наименование склада учетной системы
        /// </summary>
        public string MSWarehouse { get; set; }

        /// <summary>
        /// Партія
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// Калібр
        /// </summary>
        public string Calibre { get; set; }

        /// <summary>
        /// Тон
        /// </summary>
        public string Tone { get; set; }
    }

    public class MyBooleanConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value;

            if (value == null)
            {
                return false;
            }

            if (value.GetType() == typeof(Boolean))
            {
                return value;
            }

            if (value.GetType() == typeof(String))
            {
                switch (value)
                {
                    case "true":
                    case "yes":
                    case "1":
                        return true;
                    case "":
                    case "false":
                    case "no":
                    case "0":
                        return false;
                }
            }

            return false;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(String) || objectType == typeof(Boolean))
            {
                return true;
            }
            return false;
        }
    }
}
