using PainAssessment.Areas.Admin.Models;
using System;

namespace PainAssessment.Areas.Admin.Services
{
    public interface IAdministratorService
    {
        void CreateAdmin(Administrator admin);
        Administrator GetOneAdmin(Guid id);
        Administrator GetAllAdmin();
        void UpdateAdmin(Administrator admin);
        void DeleteAdmin(int id);

    }
}
