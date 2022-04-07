using Microsoft.AspNetCore.Mvc;
using PainAssessment.Areas.Admin.Models.ModelBinder;
using PainAssessment.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PainAssessment.Areas.Admin.Models
{
    [ModelBinder(typeof(AdminModelBinder))]
    public class Administrator
    {
        [Key]
        public Guid AdminId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        //public DateTime DOB { get; set; }
        public int ClinicalAreaID { get; set; }
        public string ClinicalArea { get; set; }
        public string Experience { get; set; }

        public Administrator(string name, string fullName,  string experience, int clinicalAreaID, Guid id)
        {
            Name = name;
            FullName = fullName;
            Experience = experience;
            ClinicalAreaID = clinicalAreaID;
            AdminId = id;
        }
        
        public Administrator(string name, string fullName, string experience, int clinicalAreaID)
        {
            FullName = fullName;
            Name = name;
            Experience = experience;
            ClinicalAreaID = clinicalAreaID;
        }
        public Administrator()
        {
        }
    }
}
