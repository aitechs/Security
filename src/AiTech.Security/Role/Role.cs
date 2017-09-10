using AiTech.LiteOrm;
using AiTech.LiteOrm.Database;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace AiTech.Security
{

    public interface IRole
    {
        string RoleName { get; set; }
        string Remarks { get; set; }

    }



    [Table("AccountRole")]
    public class Role : Entity, IRole
    {

        #region Default Properties
        public string RoleName { get; set; }
        public string Remarks { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public RolePrivilegeCollection RolePrivileges { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Role()
        {
            RolePrivileges = new RolePrivilegeCollection(this);
        }


        /// <summary>
        /// 
        /// </summary>
        public override void StartTrackingChanges()
        {
            OriginalValues = new Dictionary<string, object>()
            {
                {"RoleName", this.RoleName},
                {"Remarks", this.Remarks}
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, object> GetChangedValues()
        {
            var changes = new Dictionary<string, object>();

            if (!Equals(this.RoleName, OriginalValues["RoleName"])) changes.Add("RoleName", this.RoleName);
            if (!Equals(this.Remarks, OriginalValues["Remarks"])) changes.Add("Remarks", this.Remarks);



            return changes;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RoleName;
        }



        public bool Can(string privilege)
        {
            string query = @"select Enable Status
                            from AccountPrivilege p 
                                left join AccountRolePrivilege rp on p.Id = rp.PrivilegeId
                                left join AccountRole r on r.Id = rp.RoleId
                            where RoleId = @RoleId and PrivilegeName = @Name";

            using (var db = Connection.CreateConnection())
            {
                db.Open();

                var result = db.Query<int>(query, new { RoleId = this.Id, Name = privilege }).FirstOrDefault();

                return result != 0;
            }

        }
    }

}
