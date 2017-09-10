using System.Data;
using System.Data.SqlClient;
using AiTech.LiteOrm.Database;

namespace AiTech.Security
{
    public class AccountService
    {
        private readonly string _connectionString;

        public AccountService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateAccount(UserAccount user, string encoder)
        {

            using (var db = new SqlConnection(_connectionString))
            {
                const string query = @"DECLARE @output table ( Id int, Created Datetime, CreatedBy nvarchar(20), Modified DateTime, ModifiedBy nvarchar(20)); 
                          INSERT INTO [AccountUser] ([Username],[Password],[CreatedBy],[ModifiedBy]) 
                             OUTPUT inserted.Id, inserted.Created, inserted.CreatedBy, inserted.Modified, inserted.ModifiedBy into @output
                          VALUES (@Username,@Password,@CreatedBy,@ModifiedBy)
                          SELECT * from @output";
                db.Open();

                using (var cmd = new SqlCommand(query, db))
                {
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter( "@Username", SqlDbType.NVarChar, 20) ,
                        new SqlParameter( "@Password", SqlDbType.NVarChar, 200) ,
                        new SqlParameter( "@CreatedBy", SqlDbType.NVarChar, 20) ,
                        new SqlParameter( "@ModifiedBy", SqlDbType.NVarChar, 20)

                    });



                    cmd.Parameters["@Username"].Value = user.Username;
                    cmd.Parameters["@Password"].Value = Password.Encrypt(user.Password);
                    cmd.Parameters["@CreatedBy"].Value = encoder;
                    cmd.Parameters["@ModifiedBy"].Value = encoder;


                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        DatabaseAction.UpdateItemRecordInfo(item, reader);
                    }


                }
            }
        }
    }
