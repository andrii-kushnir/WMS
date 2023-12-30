using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_API.ModelABM
{
    public class ClassifierPackage
    {
        /// <summary>
        /// Уникальный идентификатор классификатора
        /// </summary>
        public Guid GUIDClassifierPackage { get; set; }

        /// <summary>
        /// Идентификатор учетной системы
        /// </summary>
        [StringLength(3)]
        public string CodeClassifierPackage { get; set; }

        /// <summary>
        /// Наименование классификатора
        /// </summary>
        [StringLength(25)]
        public string Description { get; set; }

        /// <summary>
        /// Полное наименование классификатора
        /// </summary>
        [StringLength(100)]
        public string FullDescription { get; set; }
    }

}
