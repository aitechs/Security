using AiTech.LiteOrm.Database;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace AiTech.Security
{
    public class UserAccountDataReader
    {
        private void DecryptPasswordAccounts(IEnumerable<UserAccount> accounts)
        {

            if (accounts == null) return;
            foreach (var item in accounts)
                DecryptPasswordAccount(item);
        }

        private void DecryptPasswordAccount(IUserAccount item)
        {
            if (item == null) return;
            item.Password = Password.Decrypt(item.Password);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserAccount> GetAllItems()
        {
            const string query = "SELECT * FROM AccountUser";

            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var results = db.Query<UserAccount>(query);
                DecryptPasswordAccounts(results);
                return results;
            }
        }


        public UserAccount GetItemWithUsername(string username)
        {
            const string query = "SELECT * FROM AccountUser where Username = @Username";

            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var result = db.Query<UserAccount>(query, new { Username = username }).FirstOrDefault();
                DecryptPasswordAccount(result);
                return result;
            }
        }
    }
}
