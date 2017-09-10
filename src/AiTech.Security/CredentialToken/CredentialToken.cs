using Dapper.Contrib.Extensions;
using System;

namespace AiTech.Security
{
    public interface ICredentialToken
    {
        string Username { get; set; }
        string Token { get; set; }
        DateTime Expiration { get; set; }
        string WindowsUsername { get; set; }
        string MachineName { get; set; }
        string IpAddress { get; set; }

    }



    [Table("CredentialToken")]
    public class CredentialToken : ICredentialToken
    {
        #region Default Properties

        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string WindowsUsername { get; set; }
        public string MachineName { get; set; }
        public string IpAddress { get; set; }

        #endregion

    }

}
