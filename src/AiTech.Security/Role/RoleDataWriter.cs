using AiTech.LiteOrm;
using AiTech.LiteOrm.Database;
using System.Data;
using System.Data.SqlClient;

namespace AiTech.Security
{
    public class RoleDataWriter : SqlMainDataWriter<Role, RoleCollection>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="item"></param>
        public RoleDataWriter(string username, Role item) : base(username, item)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="items"></param>
        public RoleDataWriter(string username, RoleCollection items) : base(username, items)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string CreateSqlInsertQuery()
        {
            return @"DECLARE @output table ( Id int, Created Datetime, CreatedBy nvarchar(20), Modified DateTime, ModifiedBy nvarchar(20)); 
                          INSERT INTO [AccountRole] ([RoleName],[Remarks],[CreatedBy],[ModifiedBy]) 
                             OUTPUT inserted.Id, inserted.Created, inserted.CreatedBy, inserted.Modified, inserted.ModifiedBy into @output
                          VALUES (@RoleName,@Remarks,@CreatedBy,@ModifiedBy)
                          SELECT * from @output";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="item"></param>
        protected override void CreateSqlInsertCommandParameters(SqlCommand cmd, Role item)
        {
            cmd.Parameters.AddRange(new[]
            {

                new SqlParameter( "@RoleName", SqlDbType.NVarChar, 50) ,
                new SqlParameter( "@Remarks", SqlDbType.NVarChar, 50) ,
                new SqlParameter( "@CreatedBy", SqlDbType.NVarChar, 20) ,
                new SqlParameter( "@ModifiedBy", SqlDbType.NVarChar, 20)

            });



            cmd.Parameters["@RoleName"].Value = item.RoleName;
            cmd.Parameters["@Remarks"].Value = item.Remarks;
            cmd.Parameters["@CreatedBy"].Value = DataWriterUsername;
            cmd.Parameters["@ModifiedBy"].Value = DataWriterUsername;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool SaveChanges()
        {
            this.AfterItemSave += RoleDataWriter_AfterItemSave; ;
            return Write(_ => _.RoleName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleDataWriter_AfterItemSave(object sender, EntityEventArgs e)
        {
            var role = (Role)e.ItemData;


            if (role.RowStatus == RecordStatus.NewRecord)
            {
                //Copy Id to Children
                foreach (var item in role.RolePrivileges.Items)
                    item.RoleId = role.Id;
            }

            //Write ROle RolePrivileges
            var dataWriter = new RolePrivilegeDataWriter(DataWriterUsername, role.RolePrivileges);
            dataWriter.SaveChanges(e.Connection, e.Transaction);
        }
    }
}
