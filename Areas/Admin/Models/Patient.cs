﻿using PainAssessment.Areas.Admin.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace PainAssessment.Areas.Admin.Models
{
    public class Patient
    {
        public Guid PatientID { get; private set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; private set; }
        [Required]
        public string Gender { get; private set; }
        [Required]
        public DateTime BirthDate { get; private set; }
        public string Condition { get; private set; }
        public string Notes { get; private set; }

        private readonly List<Practitioner> _practitioners = new List<Practitioner>();
        public virtual IReadOnlyList<Practitioner> Practitioners => _practitioners.ToList();

        private readonly List<PractitionerPatient> _practitionerPatients = new List<PractitionerPatient>();
        public virtual IReadOnlyList<PractitionerPatient> PractitionerPatients => _practitionerPatients.ToList();

        public Patient( string name, string gender, DateTime birthDate, string condition, string notes, Guid patientID)
        {
            PatientID = patientID;
            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Condition = condition;
            Notes = notes;
        }

        public Patient(string name, string gender, DateTime birthDate, string condition, string notes)
        {
            Name = Regex.Replace(name, @"\b\w{3,}\b", match => Utility.MaskName(match.Value));
            Gender = gender;
            BirthDate = birthDate;
            Condition = condition;
            Notes = notes;
        }

        public void AddPractitionerRelation(Practitioner practitioner)
        {
            _practitioners.Add(practitioner);
        }

    }
}