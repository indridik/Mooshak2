using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// ID notanda
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Notandanafn notanda
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Fullt nafn notanda
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Segir til um hvaða tag af notanda
        /// </summary>
        public int UserType { get; set; }
    }
}