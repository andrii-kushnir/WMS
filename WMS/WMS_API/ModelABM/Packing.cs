using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class Packing
    {
        /// <summary>
        /// Уникальный идентификатор единицы измерения
        /// </summary>
        public Guid GUIDPackaging { get; set; }

        /// <summary>
        /// Наименование единицы измерения
        /// </summary>
        //[StringLength(25)]
        public string Description { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public string GUIDProduct { get; set; }

        /// <summary>
        /// Уникальный идентификатор классификатора
        /// </summary>
        public Guid GUIDClassifierPackage { get; set; }

        /// <summary>
        /// Коэффициент единицы измерения относительно базовой единицы
        /// </summary>
        public float Coef { get; set; }

        /// <summary>
        /// Высота (метры)
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Ширина (метры)
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Глубина (метры)
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// Вес (килограмм)
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Объем (м³)
        /// </summary>
        public float Capacity { get; set; }

        /// <summary>
        /// Признак базовой единицы измерения
        /// </summary>
        public bool Basic { get; set; }

        /// <summary>
        /// Пометка удаления
        /// </summary>
        public bool Deprecated { get; set; } = false;
    }
}
