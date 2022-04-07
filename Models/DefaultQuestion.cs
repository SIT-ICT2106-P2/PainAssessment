using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PainAssessment.Models
{
    public class DefaultQuestion
    {

        [Required]
        public int DQID { get; set; }
        public int TCID { get; set; }
        public string QString { get; set; }
        public string QDescription { get; set; }
        /// <summary>
        /// Which pain section this question belongs to
        /// 0 - Central Modulation
        /// 1 - Regional Influence
        /// 2 - Local Stimulation
        /// </summary>
        public int PainSection { get; set; }
        public double Weightage { get; set; }


    }
}
