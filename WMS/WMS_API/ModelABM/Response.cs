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
    public class Response
    {
        /// <summary>
        /// Успешность обработки пакета
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Код обработки запроса на стороне WMS
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        public int Code { get; set; }

        /// <summary>
        /// Описание кода обработки
        /// </summary>
        public string Description { get; set; }
    }

    //public enum ResponseCode
    //{

    //    [EnumMember(Value = @"1")]
    //    _1 = 1,

    //    [EnumMember(Value = @"2")]
    //    _2 = 2,

    //    [EnumMember(Value = @"3")]
    //    _3 = 3,

    //    [EnumMember(Value = @"4")]
    //    _4 = 4,

    //    [EnumMember(Value = @"5")]
    //    _5 = 5,

    //    [EnumMember(Value = @"6")]
    //    _6 = 6,

    //    [EnumMember(Value = @"101")]
    //    _101 = 101,

    //    [EnumMember(Value = @"102")]
    //    _102 = 102,

    //    [EnumMember(Value = @"103")]
    //    _103 = 103,

    //    [EnumMember(Value = @"104")]
    //    _104 = 104,

    //    [EnumMember(Value = @"105")]
    //    _105 = 105,

    //    [EnumMember(Value = @"106")]
    //    _106 = 106,

    //    [EnumMember(Value = @"107")]
    //    _107 = 107,

    //    [EnumMember(Value = @"108")]
    //    _108 = 108,

    //}
}
