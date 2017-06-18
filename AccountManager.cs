using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    public class AccountManager
    {
        public IEnumerable<Model.AccountUser> GetUsers()
        {
            try
            {
                using (var db = Common.DbConnection.CreateConnection())
                {
                    db.Open();
                    var result = db.Query<Model.AccountUser>("Select * from AccountUser");
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }


        public void Save(Model.AccountUser user)
        {
            var col = new Accounts.Model.AccountUserCollection();
            col.Add(user);
            col.SaveChanges("Encoder");
        }
    }
}
