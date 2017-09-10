using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AiTech.Security
{
    public class UserAccountService
    {
        private string _currentUsername;
        private readonly string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUsername"></param>
        /// <param name="connectionString"></param>
        public UserAccountService(string currentUsername, string connectionString)
        {
            _currentUsername = currentUsername;
            _connectionString = connectionString;
        }



        /// <summary>
        ///  Get List of User Accounts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserAccount> GetUserAccounts()
        {
            const string query = "SELECT * FROM [AccountUser]";

            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var results = db.Query<UserAccount>(query);

                if (results == null) return null;

                return results.OrderBy(_ => _.Username);

            }
        }





    }
}
