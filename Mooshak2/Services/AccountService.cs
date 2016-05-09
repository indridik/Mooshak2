using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
    public class AccountService
    {
        private MooshakDataContext context;
        public AccountService()
        {
            this.context = new MooshakDataContext();
        }

        public void CreateUser(string username, string firstname)
        {
            string userId = context.AspNetUsers.FirstOrDefault(a => a.UserName == username).Id;


            MooshakUser user = new MooshakUser()
            {
                Username = username,
                Firstname = firstname,
                Id = new Guid(userId)
            };

            context.MooshakUsers.InsertOnSubmit(user);
            context.SubmitChanges();
        }
    }
}