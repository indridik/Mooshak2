using Mooshak2.Models;
using Mooshak2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Mooshak2.DAL;

namespace Mooshak2.Services
{
    //
    //
    //{
    //private ApplicationDbContext _db;

    //    
    //    {
    //        _db = new ApplicationDbContext();
    //    }
    //    public UserViewModel GetUserByID(int userID)
    //    {
    //        var user = _db.Users.SingleOrDefault(x => x.ID == userID);
    //        if (user == null)
    //        {
    //            //TODO: henda error
    //            return null;
    //        }
    //        UserViewModel viewModel = new UserViewModel
    //        {
    //            ID = user.ID,
    //            UserName = user.UserName,
    //            FullName = user.FullName,
    //            UserType = user.UserType
    //        };

    //        return viewModel;
    //    }

    //     public List<UserViewModel> GetAllUsersOfType(int userType)
    //     {
    //        List<UserViewModel> viewModel = new List<UserViewModel>();
    //        var users = _db.Users.Where(x => x.UserType == userType).ToList();
    //        if(users == null)
    //        {
    //            //TODO: throwa error maaarrr
    //            return null;
    //        }
    //        foreach(var uss in users)
    //        {
    //            UserViewModel tempUser = new UserViewModel();
    //            tempUser.ID = uss.ID;
    //            tempUser.FullName = uss.FullName;
    //            tempUser.UserName = uss.UserName;
    //            tempUser.UserType = uss.UserType;
    //            viewModel.Add(tempUser);
    //        }
    //        return viewModel;
    //     }
    //}
}