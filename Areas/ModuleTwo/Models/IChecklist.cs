﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainAssessment.Areas.ModuleTwo.Models
{
    public interface IChecklist
    {
        public int RetrieveIntAttribute(String attribute);
        public String RetrieveStringAttribute(String attribute);
    }
}
