using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class RestRouteSheet
    {
        /// <summary>
        /// Тип операции — всегда передавать ROUTELIST
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RouteSheetTypeOperation TypeOperation { get; set; } = RouteSheetTypeOperation.ROUTELIST;

        /// <summary>
        /// Код склада WMS
        /// </summary>
        public string WareHouseCode { get; set; }

        public InfoRouteSheet RouteSheet { get; set; }
    }

    public class InfoRouteSheet
    {
        /// <summary>
        /// Уникальный идентификатор маршрутного листа
        /// </summary>
        public string GUIDRouteSheet { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DateDoc { get; set; }

        /// <summary>
        /// Номер документа маршрутного листа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Coment { get; set; } = "";

        /// <summary>
        /// Уникальный идентификатор маршрута
        /// </summary>
        public Guid RouteGUID { get; set; }

        /// <summary>
        /// Название маршрута
        /// </summary>
        public string RouteMethod { get; set; } = "";

        /// <summary>
        /// Уникальный идентификатор способа доставки
        /// </summary>
        public Guid DMGUID { get; set; }

        /// <summary>
        /// Название способа доставки
        /// </summary>
        public string DeliveryMethod { get; set; } = "";

        /// <summary>
        /// Уникальный идентификатор автомобиля
        /// </summary>
        public string VehicleGUID { get; set; }

        /// <summary>
        /// Название автомобиля
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// Номер автомобиля
        /// </summary>
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Уникальный идентификатор водителя
        /// </summary>
        public Guid DriverGUID { get; set; }

        /// <summary>
        /// Водитель
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// ИНН водителя
        /// </summary>
        public string DriverINN { get; set; }

        /// <summary>
        /// Номер водительского удостоверения
        /// </summary>
        public string DriverLicenseNumber { get; set; }

        /// <summary>
        /// Уникальный идентификатор экспедитора
        /// </summary>
        public Guid ForwarderGUID { get; set; }

        /// <summary>
        /// Экспедитор
        /// </summary>
        public string Forwarder { get; set; }

        /// <summary>
        /// Уникальный идентификатор перевозчика
        /// </summary>
        public Guid CarrierGUID { get; set; }

        /// <summary>
        /// Перевозчик
        /// </summary>
        public string Carrier { get; set; } = "";

        /// <summary>
        /// Ворота отгрузки
        /// </summary>
        public string CellOut { get; set; }

        /// <summary>
        /// Штрихкод документа
        /// </summary>
        public string Barcode { get; set; } = "";

        /// <summary>
        /// Дата отгрузки плановая
        /// </summary>
        public DateTime DateOut { get; set; }

        /// <summary>
        /// Уникальный идентификатор организации
        /// </summary>
        public Guid GUIDOrganization { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Приоритет документа: 0 — самый высокий, 999 — самый низкий
        /// </summary>
        public int Priority { get; set; }

        public List<OrderRow> TableOrder { get; set; }

        //public List<CrossTransitOrderRow> CrossTransit { get; set; }
    }

    public partial class OrderRow
    {
        /// <summary>
        /// Уникальный идентификатор документа отгрузки
        /// </summary>
        public String GUIDOrder { get; set; }

        /// <summary>
        /// Приоритет погрузки
        /// </summary>
        public int ShipmentPriority { get; set; }
    }

    public partial class CrossTransitOrderRow
    {
        /// <summary>
        /// Уникальный идентификатор документа отгрузки
        /// </summary>
        public System.Guid GUIDDoc { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public int NumberDoc { get; set; }

        /// <summary>
        /// Уникальный идентификатор контрагента-получателя
        /// </summary>
        public System.Guid GUIDContractor { get; set; }
    }

    public enum RouteSheetTypeOperation
    {
        [EnumMember(Value = @"ROUTELIST")]
        ROUTELIST = 0
    }
}
