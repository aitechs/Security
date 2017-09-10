using AiTech.LiteOrm;
using AiTech.LiteOrm.Database;
using Dapper;

namespace AiTech.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAccountCollection : EntityCollection<UserAccount>
    {
        /// <summary>
        /// 
        /// </summary>
        public void LoadAllItemsFromDb()
        {
            const string query = @"select u.*, r.*
                                    from AccountUser u inner
                                    join AccountRole r on u.RoleId = r.Id";

            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var results = db.Query<UserAccount, Role, UserAccount>(query, (u, r) =>
                  {
                      u.RoleClass = r;

                      u.Password = Password.Decrypt(u.Password);
                      return u;
                  });

                this.LoadItemsWith(results);
            }
        }

    }
}
