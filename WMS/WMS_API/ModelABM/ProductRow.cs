﻿using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class ProductRow
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GUIDProduct { get; set; }

        /// <summary>
        /// Уникальный идентификатор единицы измерения
        /// </summary>
        public string GUIDPackaging { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public float Qty { get; set; }

        /// <summary>
        /// Серия товара, если у товара есть признак SeriesMode
        /// </summary>
        //[StringLength(150)]
        public string Series { get; set; }

        /// <summary>
        /// Срок годности товара, если у товара есть признак ShelfLifeMode
        /// </summary>
        public DateTime ShelfLife { get; set; }

        /// <summary>
        /// Качество
        /// </summary>
        //[StringLength(100)]
        public string Quality { get; set; }

        /// <summary>
        /// Дата производства
        /// </summary>
        public DateTime DateOfManufacture { get; set; }

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
}
