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
    public class OrdersModifications
    {
        /// <summary>
        /// Код склада WMS
        /// </summary>
        public List<OrderModifications> TableDocInfo { get; set; }
    }

    public class OrderModifications
    {
        /// <summary>
        /// Уникальный идентификатор записи изменений
        /// </summary>
        public Guid GUID { get; set; }

        public Guid GUIDentry { get; set; }

        /// <summary>
        /// Уникальный идентификатор документа
        /// </summary>
        public string GUIDDoc { get; set; }

        public string GUIDorder { get; set; }

        /// <summary>
        /// Этап документа, на котором зарегистрированы изменения
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        //public OrderModificationsStageDoc StageDoc { get; set; }
        public string StageDoc { get; set; }

        /// <summary>
        /// Признак стартового этапа
        /// </summary>
        public string StartStage { get; set; }

        /// <summary>
        /// Дата начала сбора документа(фактически когда была отобрана первая строка документа)
        /// </summary>
        public DateTime DateStartWorkStage { get; set; }

        /// <summary>
        /// Дата регистрации документа в обмен
        /// </summary>
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Тип операции:
        /// <br/>* IN – прием товаров
        /// <br/>* OUT – отгрузка товаров
        /// <br/>* MOVINGWMS – пермещение сделаный в WMS
        /// <br/>* INVENTORY – инвентаризация
        /// <br/>* ROUTELIST – маршрутный лист
        /// <br/>* PROBLEMSITUATION – проблемная ситуация
        /// <br/>* NOMENCLATURE – товар
        /// <br/>* CROSSTRANSIT – кросс-транзит
        /// <br/>* PRODUCTION – комплектация
        /// <br/>* RECLAMATION – рекламация
        /// <br/>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderModificationsTypeOperation TypeOperation { get; set; }

        /// <summary>
        /// Название документа для документов прихода, расхода как передавалось из учетной системы
        /// </summary>
        public string NameDoc { get; set; }

        /// <summary>
        /// Уникальный идентификатор организации
        /// </summary>
        public string GUIDOrganization { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Organization { get; set; }
    }

    public enum OrderModificationsStageDoc
    {
        [EnumMember(Value = @"Приймання розміщення")]
        Прием_контроль = 0,

        [EnumMember(Value = @"Відвантаження завершення")]
        ОтгрузкаОтбор = 1,
    }

    public enum OrderModificationsTypeOperation
    {
        [EnumMember(Value = @"IN")]
        IN = 0,

        [EnumMember(Value = @"OUT")]
        OUT = 1,

        [EnumMember(Value = @"MOVINGWMS")]
        MOVINGWMS = 2,

        [EnumMember(Value = @"INVENTORY")]
        INVENTORY = 3,

        [EnumMember(Value = @"ROUTELIST")]
        ROUTELIST = 4,

        [EnumMember(Value = @"PROBLEMSITUATION")]
        PROBLEMSITUATION = 5,

        [EnumMember(Value = @"NOMENCLATURE")]
        NOMENCLATURE = 6,

        [EnumMember(Value = @"CROSSTRANSIT")]
        CROSSTRANSIT = 7,

        [EnumMember(Value = @"PRODUCTION")]
        PRODUCTION = 8,

        [EnumMember(Value = @"RECLAMATION")]
        RECLAMATION = 9,
    }
}
