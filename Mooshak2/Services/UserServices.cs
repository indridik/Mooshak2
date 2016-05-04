﻿using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Mooshak2.Services
{
    public class UserServices
    {
        private ApplicationDbContext _db;

        public UserServices()
        {
            _db = new ApplicationDbContext();
        }
        public UserViewModel GetUserByID(int userID)
        {
            var user = _db.Users.SingleOrDefault(x => x.ID == userID);
            if (user == null)
            {
                //TODO: henda error
                return null;
            }
            UserViewModel viewModel = new UserViewModel
            {
                ID = user.ID,
                UserName = user.UserName,
                FullName = user.FullName,
                UserType = user.UserType
            };

            return viewModel;
        }

        /* public List<UserViewModel> GetAllUsersOfType(int userType)
         {

         }*/
    }
}