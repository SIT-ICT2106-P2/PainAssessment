using PainAssessment.Models;
using System;
using System.Collections.Generic;

namespace PainAssessment.Interfaces
{
    public interface ILoginService
    {
        User Login(string username, string password);
        string GetRole();
        Guid GetAccountId();
        bool CheckInstance();
        void SetFirstSignInFalse(Account account);
        string IsFirstSignIn(Guid accountId);
    }
}
