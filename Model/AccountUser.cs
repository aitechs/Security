using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using AiTech.CrudPattern;

namespace Accounts.Model
{
    [Table("AccountUser")]
    public class AccountUser : Entity
    {
        Dictionary<string, object> OriginalValues;

        #region Default Properties

        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        protected void InitializeTrackingChanges()
        {
            OriginalValues = new Dictionary<string, object>();

            OriginalValues.Add("Username", this.Username);
            OriginalValues.Add("Password", this.Password);
        }

        protected Dictionary<string, object> GetChanges()
        {
            var changes = new Dictionary<string, object>();


            if (!Equals(this.Username, OriginalValues["Username"]))
                changes.Add("Username", this.Username);

            if (!Equals(this.Password, OriginalValues["Password"]))
                changes.Add("Password", this.Password);


            return changes;
        }


    }



}
