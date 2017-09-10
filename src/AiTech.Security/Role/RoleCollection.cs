using AiTech.LiteOrm;
using AiTech.LiteOrm.Database;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace AiTech.Security
{
    public class RoleCollection : EntityCollection<Role>
    {

        public void LoadAllItems()
        {
            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var result = db.Query<Role>("Select * from AccountRole");


                var enumerable = result as IList<Role> ?? result.ToList();
                foreach (var item in enumerable)
                {
                    item.StartTrackingChanges();
                }


                LoadItemsWith(enumerable);
            }

        }

    }
}
