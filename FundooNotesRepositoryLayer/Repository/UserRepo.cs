using FundooNotesModelLayer;
using FundooNotesRepositoryLayer.IRepository;
using FundooNotesRepositoryLayer.MSMQ_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesRepositoryLayer.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext userDbContext;
        private readonly IMSMQService msmq;
        private readonly IConfiguration configuration;
        private readonly IDistributedCache cache;
        private readonly string cacheKey;
        public UserRepo(UserContext userDbContext,IMSMQService msmq, IConfiguration configuration, IDistributedCache cache)
        {
            this.msmq = msmq;
            this.userDbContext = userDbContext;
            this.configuration = configuration;
            this.cache = cache;
            this.cacheKey = "User";
        }
      
        public string PasswordEncryption(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encData_byte);
                return encodedPassword;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        public UserRegistration AddUser(UserRegistration objUser)
        {
            string encodePassword = PasswordEncryption(objUser.Password);
            objUser.Password = encodePassword;
            this.userDbContext.Users.Add(objUser);
            var result = this.userDbContext.SaveChanges();
            if (result != 0)
            { 
                return objUser;
            }
            return null ;
        }
        public UserRegistration Login(UserLogin login)
        {
            var dataFromCache = cache.GetString(cacheKey);
            var data = JsonConvert.DeserializeObject<List<UserRegistration>>(dataFromCache);
            if (data != null)
            {
                foreach (var kvp in data)
                {
                    string decryptPassword = Decryptdata(kvp.Password);
                    if (kvp.Email == login.Email && decryptPassword==login.Password)
                    {
                        return kvp;
                    }
                }
            }
            var result = this.userDbContext.Users.Where<UserRegistration>(details => details.Email == login.Email).FirstOrDefault();
            if (result != null)
            {
                string decryptPassword = Decryptdata(result.Password);
                if(decryptPassword==login.Password)
                    return result;
            }
            return null;
        }

        public UserRegistration ResetPassword(ResetUserPassword reset)
        {
            
            var result = this.userDbContext.Users.Where<UserRegistration>(user => user.Email == reset.Email).FirstOrDefault();
            if (result != null)
            {
                result.Password = reset.Password;
                this.userDbContext.Users.Update(result);
                var saveResult=this.userDbContext.SaveChanges();
                if (saveResult != 0)
                {
                    return result;
                }
            }
            return null;

        }
        public string ForgetPassword(ForgetPassword forget)
        {
            var result = this.userDbContext.Users.Where<UserRegistration>(user => user.Email == forget.Email).FirstOrDefault();
            if (result != null)
            {
                string decode = Decryptdata(result.Password);
                this.msmq.AddToQueue(forget.Email);
                return "Success";

            }
            return "Error";
        }
        public IEnumerable<UserRegistration> GetAllUser()
        {
            if (this.cache.GetString(cacheKey) != null)
            {
                var data = JsonConvert.DeserializeObject<List<UserRegistration>>(this.cache.GetString(cacheKey));
                return data;
            }
            else
            {
                var result = this.userDbContext.Users.ToList<UserRegistration>();
                if (result != null)
                {
                    this.cache.SetString(this.cacheKey, JsonConvert.SerializeObject(result));

                }
                return result;
            }
        }
        
    }
}