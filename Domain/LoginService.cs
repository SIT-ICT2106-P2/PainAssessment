using PainAssessment.Interfaces;
using PainAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BC = BCrypt.Net.BCrypt;
using Org.BouncyCastle.Crypto.Generators;

namespace PainAssessment.Domain
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly User myUser = User.GetInstance;

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /**
         * Find account and set instance to User Object
         */
        public User Login(string username, string password)
        {

            IEnumerable<Account> account = _unitOfWork.LoginRepository.Find(Acc => Acc.Username.Equals(username));
            // Check if account exists and password matches to the one in db
            Account userAcc = account.FirstOrDefault();
            if (userAcc != null && BC.Verify(password, userAcc.Password))
            {
                Account user = _unitOfWork.LoginRepository.GetById<Account, Guid>(userAcc.AccountId);

                myUser.SetProperty(user.AccountId, user.Role);

                return myUser;
            }
            else
            {
                return null;
            }
        }

        /**
         * Get current logged in user's role from instance
         */
        public string GetRole()
        {
            return myUser.GetRole;

        }

        /**
         * Get current logged in user's account Id from instance
         */
        public Guid GetAccountId()
        {
            return myUser.GetGuid;
        }

        /**
         * Check if instance is initiated, return a boolean
         */
        public bool CheckInstance()
        {
            if (User.GetInstance != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Check if user is first sign in. 
         */
        public string IsFirstSignIn(Guid accountId)
        {
            Account user = _unitOfWork.LoginRepository.GetById<Account, Guid>(accountId);
            if (user != null)
            {
                if (user.FirstSignIn.Equals(true))
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return null;
            }

        }

        /**
         * Update firstSignIn field
         */
        public void SetFirstSignInFalse(Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.Save();
        }

    }
}
