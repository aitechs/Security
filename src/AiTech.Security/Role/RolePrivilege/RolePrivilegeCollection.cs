using AiTech.LiteOrm;
using AiTech.LiteOrm.Database;
using Dapper;
using System.Linq;

namespace AiTech.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class RolePrivilegeCollection : EntityCollection<RolePrivilege>
    {
        private Role _parentRole;

        /// <summary>
        /// Constructor to assign ParentRole
        /// </summary>
        /// <param name="parentRole"></param>
        public RolePrivilegeCollection(Role parentRole)
        {
            _parentRole = parentRole;
        }

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public RolePrivilegeCollection()
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Load Privilege from Parent Role
        /// </summary>
        public void LoadItemsFromDb()
        {
            const string query = @"select IsNull(rp.Id,0) Id
                                    , IsNull(rp.RoleId,0) RoleId

                                    , p.Id PrivilegeId
                                    , PrivilegeName
                                    , DisplayName
                                    , DisplayOrder
                                    , Category 
                                    , IsNull(Enable,0) Enable
                                    from AccountPrivilege p left join (select * from AccountRolePrivilege where RoleId = @RoleId) rp on rp.PrivilegeId = p.id 
                                    order by category, displayorder";


            using (var db = Connection.CreateConnection())
            {
                db.Open();

                ItemCollection.Clear();

                dynamic results = db.Query<dynamic>(query, new { RoleId = _parentRole.Id });

                foreach (var result in results)
                {
                    var item = new RolePrivilege
                    {
                        Id = result.Id,
                        PrivilegeId = result.PrivilegeId,
                        Enable = result.Enable,

                        RoleId = _parentRole.Id,
                        RoleClass = _parentRole,

                        PrivilegeClass =
                        {
                            Id = result.PrivilegeId,
                            PrivilegeName = result.PrivilegeName,
                            DisplayName = result.DisplayName,
                            Category = result.Category
                        },

                        RowStatus = result.Id == 0 ? RecordStatus.NewRecord : RecordStatus.NoChanges
                    };


                    item.StartTrackingChanges();
                    ItemCollection.Add(item);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="privilege"></param>
        /// <returns></returns>
        public bool Can(string privilege)
        {
            var item = this.Items.FirstOrDefault(_ => _.PrivilegeClass.PrivilegeName == privilege);

            return item != null && item.Enable;
        }
    }
}
