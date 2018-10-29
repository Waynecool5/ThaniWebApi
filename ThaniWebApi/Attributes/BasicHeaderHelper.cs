using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaniWebApi.Attributes
{
    #region Usings

    using System;
    using System.Text;

    #endregion

    /// <summary>
    /// The authorization header helper.
    /// </summary>
    public static class AuthorizationHeaderHelper
    {
        #region Public Methods

        /// <summary>
        /// Gets basic header.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public static string GetBasic(string userName, string password) => $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"))}";

        /// <summary>
        /// Gets basic header.
        /// </summary>
        public static string GetBasic() => GetBasic("Wayneo", "dedan");

        #endregion
    }
}
