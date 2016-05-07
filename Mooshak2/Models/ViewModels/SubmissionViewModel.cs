using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.ViewModels
{
    public class SubmissionViewModel
    {
        /// <summary>
        /// Notandanafn nemanda sem skilaði
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Hluti verkefnis sem var skilað
        /// </summary>
        public char Milestone { get; set; }
        /// <summary>
        /// Niðurstaða skila
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Forritunartungumál skila
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Tímasetning skila
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// Hvaða kúrs er verið að skila verkefni í
        /// </summary>
        public string Course { get; set; }

    }
}