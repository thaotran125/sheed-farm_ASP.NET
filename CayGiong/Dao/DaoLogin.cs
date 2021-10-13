using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CayGiong.Models;

namespace CayGiong.Dao
{
    public class DaoLogin
    {
        public Account getUserByid(string username)
        {
            using(var context=new DB_SEED_FARMEntities())
            {
               return context.Accounts.Where(x => x.userName.Equals(username)).SingleOrDefault();
                
            }
        }

        public int Login(string username, string pass)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                string password = MD5Hash(Base64Encode(pass));
                var result= context.PR_Login_user(username, password).Count();
                if (result >0)
                    return 1;
                else
                {
                    var check = context.Accounts.Where(x => x.userName == username).Count();
                    if (check==0)
                        return -1;
                    else
                        return 0;
                }
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }



        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        //public List<PRgetNhanVienAccount_Result> getNhanVienNotExistsAcc()
        //{
        //    using(var context=new DB_SEED_FARMEntities())
        //    {
        //        return context.PRgetNhanVienAccount().ToList();
        //    }
        //}
    }
}