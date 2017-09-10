using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AiTech.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService
    {
        private readonly string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public AuthenticationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public CredentialToken GetCredentialToken(string username)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var appToken = new CredentialToken()
                {
                    Username = username,
                    Token = TokenGenerator.Generate(128),

                    WindowsUsername = Environment.UserName,
                    MachineName = Environment.MachineName,
                    IpAddress = Network.GetIpAddresses()

                };

                var ret = db.Execute(@"INSERT INTO AccountToken (Username, Token, WindowsUsername, MachineName, IpAddress, ModifiedBy, CreatedBy) VALUES 
                                    (@Username, @Token, @WindowsUsername, @MachineName, @IpAddress, @Encoder, @Encoder)",
                    new
                    {
                        Username = appToken.Username,
                        Token = appToken.Token,
                        WindowsUsername = appToken.WindowsUsername,
                        MachineName = appToken.MachineName,
                        IpAddress = appToken.IpAddress,
                        Encoder = username
                    });

                return ret > 0 ? appToken : null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Authenticate(string userName, string password)
        {
            const string query = "Select u.Id, Username, EmployeeId, r.Id, r.RoleName, r.Remarks from AccountUser u left join AccountRole r on u.RoleId = r.Id" +
                                 " where username = @user and password = @pwd";

            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();


                var encryptedPassword = Password.Encrypt(password);

                var user = db.Query<UserAccount, Role, UserAccount>(query, (u, r) =>
                {
                    u.Password = password;
                    u.RoleId = r.Id;
                    u.RoleClass = r;
                    return u;
                }, new { user = userName, pwd = encryptedPassword }).FirstOrDefault();

                user?.StartTrackingChanges();

                return user;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserAccount> AuthenticateAsync(string userName, string password)
        {
            var result = Task.Factory.StartNew(() => Authenticate(userName, password));
            return await result;
        }
    }
}
