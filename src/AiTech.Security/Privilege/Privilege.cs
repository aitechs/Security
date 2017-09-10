using AiTech.LiteOrm;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace AiTech.Security
{

    public interface IPrivilege
    {
        int DisplayOrder { get; set; }
        string PrivilegeName { get; set; }
        string DisplayName { get; set; }
        string Category { get; set; }

    }



    [Table("AccountPrivilege")]
    public class Privilege : Entity, IPrivilege
    {

        #region Default Properties
        public int DisplayOrder { get; set; }
        public string PrivilegeName { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }

        #endregion


        public override void StartTrackingChanges()
        {
            OriginalValues = new Dictionary<string, object>()
            {
                {"DisplayOrder", this.DisplayOrder},
                {"PrivilegeName", this.PrivilegeName},
                {"DisplayName", this.DisplayName},
                {"Category", this.Category}
            };
        }

        public override Dictionary<string, object> GetChangedValues()
        {
            var changes = new Dictionary<string, object>();
            if (!Equals(this.DisplayOrder, OriginalValues["DisplayOrder"])) changes.Add("DisplayOrder", this.DisplayOrder);
            if (!Equals(this.PrivilegeName, OriginalValues["PrivilegeName"])) changes.Add("PrivilegeName", this.PrivilegeName);
            if (!Equals(this.DisplayName, OriginalValues["DisplayName"])) changes.Add("DisplayName", this.DisplayName);
            if (!Equals(this.Category, OriginalValues["Category"])) changes.Add("Category", this.Category);



            return changes;
        }


    }

}
