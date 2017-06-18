using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Dapper;

namespace Accounts
{
    public class UserCollection
    {
        public IEnumerable<AccountUser> Items { get; set; }

        ICollection<AccountUser> collection = new List<AccountUser>();
        public UserCollection()
        {
            Items = collection;
        }

        public void LoadItems()
        {
            try
            {
                using (var db = Common.DbConnection.CreateConnection())
                {
                    db.Open();
                    var result = db.Query<AccountUser>("Select * from AccountUser");

                    foreach (var item in result)
                        collection.Add(item);
                }
            } catch
            {
                throw;
            }
        }
    }
}
