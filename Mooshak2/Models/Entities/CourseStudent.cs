using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.Entities
{
    /// <summary>
    /// Setja nemendur í ákveðnum námskeiðum í database'ið
    /// </summary>
    public class CourseStudent
    {
        /// <summary>
        /// ID'ið fyrir notandann
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Notandanafn notanda
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ID'ið fyrir námskeiðið sem nemandinn er skráður í.
        /// </summary>
        public int CourseID { get; set; }
    }
}