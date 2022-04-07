using PainAssessment.Areas.Admin.Models;
using PainAssessment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PainAssessment.Areas.Admin.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdministratorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /**
         * Create Admin
         */
        public void CreateAdmin(Administrator admin)
        {
            _unitOfWork.AdministratorRepository.Add(admin);
            _unitOfWork.Save();
        }

        /**
         * Get one Admin
         */
        public Administrator GetOneAdmin(Guid id)
        {
            /*IEnumerable<Administrator> admin = _unitOfWork.AdministratorRepository.Find(Acc => Acc.Account.AccountId.Equals(id));
            Administrator adminAcc = admin.First();*/
            Administrator adminDetails = _unitOfWork.AdministratorRepository.GetById<Administrator,Guid>(id);
            return adminDetails;
        }

        /**
         * Get all Admin
         */
        public Administrator GetAllAdmin()
        {
            return (Administrator)_unitOfWork.AdministratorRepository.GetAll();
        }


        /**
         * Update Admin
         */
        public void UpdateAdmin(Administrator admin)
        {
            _unitOfWork.AdministratorRepository.Update(admin);
            _unitOfWork.Save();
        }

        /**
         * Delete Admin
         */
        public void DeleteAdmin(int id)
        {
            _unitOfWork.AdministratorRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
