using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models.Entities
{

    /// <summary>
    /// Milestone reppar hversu mikið vægi hver liður í verkefni er
    /// </summary>
    public class AssignmentMilestone
    {
        /// <summary>
        /// Unique ID fyrir milestone-ið í database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// nr. á verkefnið sjálft
        /// </summary>
        public int AssignmentID { get; set; }

        /// <summary>
        /// nafnið á liðinn
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// reppar vægi þessa liðs. T.d. 15% væri 15. (Passa að það stemmi þá uppí 100)
        /// </summary>
        public int Weight { get; set; }
    }
}