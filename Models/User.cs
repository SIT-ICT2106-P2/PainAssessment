﻿using System;
using System.Collections.Generic;

namespace PainAssessment.Models
{
    public class User
    {
        private static User _instance = null;
        private static Guid _AccountId = Guid.Empty;
        private static string _Role = null;
        public Guid GetGuid
        {
            get { return _AccountId; }
        }

        public string GetRole
        {
            get { return _Role; }
        }

        //public static Boolean hasEmptyProperty()
        //{
        //    return User._AccountId == Guid.Empty || User._Role != null;
        //}

        public void setProperty(Guid guid, string role)
        {
            _Role = role;
            _AccountId = guid;
        }

        private User() { }

        private static object syncLock = new object();

        public static User GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        _instance = new User();
                    }
                }
                return _instance;

            }
        }
    }
}
