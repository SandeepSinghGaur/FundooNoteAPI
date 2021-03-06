using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesManagerLayer.Manager
{
    public class UserManager :IUserManager
    {
        private readonly IUserRepo userRepo;
        public UserManager(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserRegistration AddUser(UserRegistration objUser)
        {
            return this.userRepo.AddUser(objUser);
        }

        public string ForgetPassword(ForgetPassword forget)
        {
            return this.userRepo.ForgetPassword(forget);
        }

        public IEnumerable<UserRegistration> GetAllUser()
        {
            return this.userRepo.GetAllUser();
        }

        public UserRegistration Login(UserLogin login)
        {
            return this.userRepo.Login(login);
        }

        public UserRegistration ResetPassword(ResetUserPassword reset)
        {
            return this.userRepo.ResetPassword(reset);
        }
    }
}
