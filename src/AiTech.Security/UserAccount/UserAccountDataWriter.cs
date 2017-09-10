using AiTech.LiteOrm.Database;
using System.Data;
using System.Data.SqlClient;

namespace AiTech.Security
{
    public class UserAccountDataWriter : SqlMainDataWriter<UserAccount, UserAccountCollection>
    {
        public UserAccountDataWriter(string username, UserAccount item) : base(username, item) { }

        public UserAccountDataWriter(string username, UserAccountCollection items) : base(username, items) { }

        public override bool SaveChanges()
        {
            return Write(_ => _.Username);
        }

        protected override string CreateSqlInsertQuery()
        {
            return
                @"DECLARE @output table ( Id int, Created Datetime, CreatedBy nvarchar(20), Modified DateTime, ModifiedBy nvarchar(20)); 
                          INSERT INTO [AccountUser] ([Username],[Password],[RoleId],[EmployeeId],[CreatedBy],[ModifiedBy]) 
                             OUTPUT inserted.Id, inserted.Created, inserted.CreatedBy, inserted.Modified, inserted.ModifiedBy into @output
                          VALUES (@Username,@Password,@RoleId,@EmployeeId,@CreatedBy,@ModifiedBy)
                          SELECT * from @output";
        }

        protected override void CreateSqlInsertCommandParameters(SqlCommand cmd, UserAccount item)
        {
            cmd.Parameters.AddRange(new[]
            {

                new SqlParameter( "@Username", SqlDbType.NVarChar, 20) ,
                new SqlParameter( "@Password", SqlDbType.NVarChar, 200) ,
                new SqlParameter( "@RoleId", SqlDbType.Int, 0) ,
                new SqlParameter( "@EmployeeId", SqlDbType.Int, 0) ,
                new SqlParameter( "@CreatedBy", SqlDbType.NVarChar, 20) ,
                new SqlParameter( "@ModifiedBy", SqlDbType.NVarChar, 20)

            });



            cmd.Parameters["@Username"].Value = item.Username;
            cmd.Parameters["@Password"].Value = Password.Encrypt(item.Password);
            cmd.Parameters["@RoleId"].Value = item.RoleId;
            cmd.Parameters["@EmployeeId"].Value = item.EmployeeId;
            cmd.Parameters["@CreatedBy"].Value = DataWriterUsername;
            cmd.Parameters["@ModifiedBy"].Value = DataWriterUsername;
        }
    }
}
