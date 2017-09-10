using AiTech.LiteOrm.Database;
using System.Data;
using System.Data.SqlClient;

namespace AiTech.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class RolePrivilegeDataWriter : SqlDataWriter<RolePrivilege, RolePrivilegeCollection>
    {
        public RolePrivilegeDataWriter(string username, RolePrivilege item) : base(username, item)
        {
        }

        public RolePrivilegeDataWriter(string username, RolePrivilegeCollection items) : base(username, items)
        {
        }

        protected override string CreateSqlInsertQuery()
        {
            return
                @"DECLARE @output table ( Id int, Created Datetime, CreatedBy nvarchar(20), Modified DateTime, ModifiedBy nvarchar(20)); 
                          INSERT INTO [AccountRolePrivilege] ([RoleId],[PrivilegeId],[Enable]) 
                             OUTPUT inserted.Id, inserted.Created, inserted.CreatedBy, inserted.Modified, inserted.ModifiedBy into @output
                          VALUES (@RoleId,@PrivilegeId,@Enable)
                          SELECT * from @output";
        }


        protected override void CreateSqlInsertCommandParameters(SqlCommand cmd, RolePrivilege item)
        {
            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter( "@RoleId", SqlDbType.Int) ,
                new SqlParameter( "@PrivilegeId", SqlDbType.Int) ,
                new SqlParameter( "@Enable", SqlDbType.Bit) ,
                new SqlParameter( "@CreatedBy", SqlDbType.NVarChar, 20) ,
                new SqlParameter( "@ModifiedBy", SqlDbType.NVarChar, 20)
            });

            cmd.Parameters["@RoleId"].Value = item.RoleId;
            cmd.Parameters["@PrivilegeId"].Value = item.PrivilegeId;
            cmd.Parameters["@Enable"].Value = item.Enable;
            cmd.Parameters["@CreatedBy"].Value = DataWriterUsername;
            cmd.Parameters["@ModifiedBy"].Value = DataWriterUsername;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="trn"></param>
        /// <returns></returns>
        public override bool SaveChanges(SqlConnection db, SqlTransaction trn)
        {
            return Write(_ => _.PrivilegeClass.PrivilegeName, db, trn);
        }
    }
}
