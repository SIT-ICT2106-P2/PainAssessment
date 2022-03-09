﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PainAssessment.Areas.Admin.Models
{
    public class PainEducation
    {
        public int Id { get; private set; }
        [Required]
        [DisplayName("Prior Pain Education")]
        public string Name { get; private set; }
        public PainEducation(string name)
        {
            Name = name;
        }

        public PainEducation(string name, int clinicalAreaID)
        {
            Id = clinicalAreaID;
            Name = name;
        }
    }
}