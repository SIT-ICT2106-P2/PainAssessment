using PainAssessment.Interfaces;
using PainAssessment.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PainAssessment.Domain
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /**
         * Get User Account details
         */
        public Account GetAccount(Guid accountId)
        {
            Account user = _unitOfWork.AccountRepository.GetById<Account, Guid>(accountId);
            if (user != null)
            {
                return user;
            }
            else return null;
        }

        /**
         * Get User Account details by Username
         */
        public Account GetAccountByUsername(string username)
        {
            IEnumerable<Account> account = _unitOfWork.AccountRepository.Find(Acc => Acc.Username.Equals(username));
            Account userAcc = account.First();
            if (userAcc != null)
            {
                return userAcc;
            }
            else return null;
        }

        /**
         * Update User Password
         */
        public void UpdatePassword(Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.Save();

        }

        /**
         * Update AccountStatus
         */
        public void UpdateAccountStatus(Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.Save();
        }

        /**
         * Create Account
         */
        public void CreateAcc(Account account)
        {
            _unitOfWork.AccountRepository.Add(account);
            _unitOfWork.Save();
        }

        /**
         * Function to check duplicate username in repository
         */
        public bool CheckUsername(string username)
        {
            IEnumerable<Account> account = _unitOfWork.AccountRepository.Find(Acc => Acc.Username.Equals(username));

            if (account.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}