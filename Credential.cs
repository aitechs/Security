using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Common;

namespace Accounts
{
    public class Credential
    {
        public static AccountToken GetToken(string userName, string password)
        {
            using (var db = Common.DbConnection.CreateConnection())
            {
                //return db.ConnectionString;
                db.Open();
                var ret = db.Query<AccountUser>("Select username from AccountUser where username = @user and password = @pwd", new { user = userName, pwd = password }).FirstOrDefault();
                if (ret == null) return null;

                var appToken = new AccountToken() {
                    Username = userName,
                    Token = TokenGenerator.Generate(128),
                    Created = DateTime.Now,
                    Expiration = DateTime.Now.AddMinutes(10)
                    };

                db.Insert<AccountToken>(appToken);
                
                return appToken;
            }

        }

        public static bool IsValidToken(string token)
        {            
            using (var db = DbConnection.CreateConnection())
            {
                db.Open();
                var ret = db.Query<AccountUser>("Select username from AccountToken where token = @Token and Expiration > getdate()", new { Token = token }).FirstOrDefault();

                if (ret == null) return false;

                return true;
            }
        }

    }
}
