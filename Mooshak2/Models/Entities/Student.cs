using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.Entities
{
    public class Student
    {
        /// <summary>
        /// ID'ið fyrir notandann
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Notandanafn notanda
        /// </summary>
        public string UserName { get; set; }
    }
}