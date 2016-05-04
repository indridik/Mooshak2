using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.Entities
{
    /// <summary>
    /// Setja nemendur í ákveðnum námskeiðum í database'ið
    /// </summary>
    public class CourseTeacher
    {
        /// <summary>
        /// ID'ið fyrir kennarann
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Username kennara
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ID'ið fyrir námskeiðið sem kennarinn er skráður á.
        /// </summary>
        public int CourseID { get; set; }

    }
}