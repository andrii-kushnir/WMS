using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class GetRemains
    {
        /// <summary>
        /// Код склада WMS
        /// </summary>
        public string WareHouseCode { get; set; }

        /// <summary>
        /// * true - получение свободных остатков
        /// * false - получение всех остатков
        /// </summary>
        public bool Free { get; set; }

        /// <summary>
        /// * true - получение остатков с учетом служебных ячеек (ячейки недостачи, брака, буферная зона, зона приема товара, зона контроля, зона коррекции остатков)
        /// * false - получение остатков без учета служебных ячеек
        /// </summary>
        public bool ShowServiceCells { get; set; }

        public List<MSWarehouseCodeArray> MSWarehouseCodeArray { get; set; }

        /// <summary>
        /// Дата остатков
        /// </summary>
        public DateTime Date { get; set; }
    }

    public partial class MSWarehouseCodeArray
    {
        /// <summary>
        /// Идентификатор склада учетной системы
        /// </summary>
        public string MSWarehouseCode { get; set; }
    }
}
