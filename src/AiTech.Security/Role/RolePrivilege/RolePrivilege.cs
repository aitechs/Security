using AiTech.LiteOrm;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace AiTech.Security
{

    public interface IRolePrivilege
    {
        int RoleId { get; set; }
        int PrivilegeId { get; set; }
        bool Enable { get; set; }

    }



    [Table("AccountRolePrivilege")]
    public class RolePrivilege : Entity, IRolePrivilege
    {

        #region Default Properties
        public int RoleId { get; set; }
        public int PrivilegeId { get; set; }
        public bool Enable { get; set; }

        #endregion


        public Privilege PrivilegeClass { get; set; }
        public Role RoleClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RolePrivilege()
        {
            PrivilegeClass = new Privilege();
            RoleClass = new Role();

        }


        /// <summary>
        /// 
        /// </summary>
        public override void StartTrackingChanges()
        {
            OriginalValues = new Dictionary<string, object>()
            {
                {"RoleId", this.RoleId},
                {"PrivilegeId", this.PrivilegeId},
                {"Enable", this.Enable}
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, object> GetChangedValues()
        {
            var changes = new Dictionary<string, object>();
            if (!Equals(this.RoleId, OriginalValues["RoleId"])) changes.Add("RoleId", this.RoleId);
            if (!Equals(this.PrivilegeId, OriginalValues["PrivilegeId"])) changes.Add("PrivilegeId", this.PrivilegeId);
            if (!Equals(this.Enable, OriginalValues["Enable"])) changes.Add("Enable", this.Enable);



            return changes;
        }


    }

}
