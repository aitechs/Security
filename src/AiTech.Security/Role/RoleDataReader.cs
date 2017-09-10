using AiTech.LiteOrm.Database;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace AiTech.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleDataReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetAllRoleNames()
        {
            const string query = "SELECT Id, RoleName FROM [AccountRole]";

            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var result = db.Query<Role>(query);

                if (result == null) return null;

                var roleNames = result as IList<Role> ?? result.ToList();
                foreach (var item in roleNames)
                    item.StartTrackingChanges();

                return roleNames;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleWithId(int id)
        {
            const string query = "SELECT * FROM [AccountRole] where Id = @Id";

            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var result = db.Query<Role>(query, new { Id = id }).FirstOrDefault();


                if (result == null) return null;

                result.StartTrackingChanges();
                return result;

            }
        }

    }
}
