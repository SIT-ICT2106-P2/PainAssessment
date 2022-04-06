﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using PainAssessment.Areas.Admin.Models.Builder;
using PainAssessment.Areas.Admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainAssessment.Areas.Admin.Models.ModelBinder
{
    public class AdminModelBinder : IModelBinder
    {
        private readonly IClinicalAreaService clinicalAreaService;
        private readonly IPracticeTypeService practiceTypeService;
        private readonly IPainEducationService painEducationService;
        private readonly IAdministratorService adminService;
        public AdminModelBinder(IClinicalAreaService clinicalAreaService, IPracticeTypeService practiceTypeService, IPainEducationService painEducationService, IAdministratorService adminService)
        {
            this.clinicalAreaService = clinicalAreaService;
            this.practiceTypeService = practiceTypeService;
            this.painEducationService = painEducationService;
            this.adminService = adminService;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Administrator admin;

            Microsoft.AspNetCore.Http.IFormCollection data = bindingContext.HttpContext.Request.Form;
            bool fullNameResult = data.TryGetValue("FullName", out Microsoft.Extensions.Primitives.StringValues fullName);
            bool nameResult = data.TryGetValue("Name", out Microsoft.Extensions.Primitives.StringValues name);
            bool roleResult = data.TryGetValue("Role", out Microsoft.Extensions.Primitives.StringValues role);
            bool idResult = data.TryGetValue("Id", out Microsoft.Extensions.Primitives.StringValues id);
            bool dobResult = data.TryGetValue("DOB", out Microsoft.Extensions.Primitives.StringValues dob);
            bool experienceResult = data.TryGetValue("Experience", out Microsoft.Extensions.Primitives.StringValues experience);
            bool clinicResult = data.TryGetValue("ClinicalAreaID", out Microsoft.Extensions.Primitives.StringValues clinicalAreaID);

            bool parseClinicSuccess = int.TryParse(clinicalAreaID, out int parsedClinicID);
            bool parseDOBSuccess = DateTime.TryParse(dob, out DateTime parsedDOB);


            if (!fullNameResult || !nameResult || !roleResult|| !experienceResult || !dobResult || !clinicResult )
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            else
            {
                int createdClinicID = default;
                DateTime date = default;
                List<int> priorEducationList = new();
                if (!parseClinicSuccess)
                {
                    ClinicalArea newClinicalArea = new(clinicalAreaID);
                    clinicalAreaService.CreateClinicalArea(newClinicalArea);
                    clinicalAreaService.SaveClinicalArea();
                    createdClinicID = newClinicalArea.Id;
                }
                IAdminBuilder adminBuilder = new AdminBuilder().WithName(name.ToString())
                                                                                   .WithFullName(fullName)
                                                                                   .WithRole(role)
                                                                                   .WithExperience(experience)
                                                                                   .WithClinic(parseClinicSuccess ? parsedClinicID : createdClinicID)
                                                                                   .WithDOB(DateTime.Parse(dob));


                if (idResult)
                {
                    adminBuilder.WithId(Guid.Parse(id.ToString()));
                }
                admin = adminBuilder.Build();
                bindingContext.Result = ModelBindingResult.Success(admin);

            }
            return Task.CompletedTask;
        }
    }
}