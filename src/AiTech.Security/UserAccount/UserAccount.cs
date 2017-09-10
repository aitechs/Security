using AiTech.LiteOrm;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace AiTech.Security
{

    public interface IUserAccount
    {
        string Username { get; set; }
        string Password { get; set; }

    }



    /// <summary>
    /// 
    /// </summary>
    [Table("AccountUser")]
    public class UserAccount : Entity, IUserAccount
    {

        #region Default Properties

        public string Username { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public int EmployeeId { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Role RoleClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserAccount()
        {
            RoleClass = new Role();

        }


        /// <summary>
        ///  
        /// </summary>
        public override void StartTrackingChanges()
        {
            OriginalValues = new Dictionary<string, object>()
            {
                {"Username", this.Username},
                {"Password", this.Password},
                {"RoleId", this.RoleId}
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, object> GetChangedValues()
        {
            var changes = new Dictionary<string, object>();
            if (!Equals(this.Username, OriginalValues["Username"])) changes.Add("Username", this.Username);
            if (!Equals(this.Password, OriginalValues["Password"])) changes.Add("Password", this.Password);
            if (!Equals(this.RoleId, OriginalValues["RoleId"])) changes.Add("RoleId", this.RoleId);



            return changes;
        }


    }

}
