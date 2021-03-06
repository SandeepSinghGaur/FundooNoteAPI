using FundooNotesModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesRepositoryLayer.IRepository
{
    public interface IUserRepo
    {
        UserRegistration AddUser(UserRegistration objUser);
        UserRegistration Login(UserLogin login);
        UserRegistration ResetPassword(ResetUserPassword reset);
        string ForgetPassword(ForgetPassword forget);
        IEnumerable<UserRegistration> GetAllUser();


    }
}
