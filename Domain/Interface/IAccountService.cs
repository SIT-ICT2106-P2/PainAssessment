using PainAssessment.Models;
using System;
using System.Collections.Generic;

namespace PainAssessment.Interfaces
{
    public interface IAccountService
    {
        Account GetAccount(Guid accountId);
        Account GetAccountByUsername(string username); 
        void UpdatePassword(Account account);
        void CreateAcc(Account account);
        bool CheckUsername(string username);
        void UpdateAccountStatus(Account account);
    }
}
