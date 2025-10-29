using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus
{
    internal static class AuthLog
    {
        public static bool IsAuthenticated { get; private set; }

        private static Dictionary<string, string> _loginPasswordPairs = new Dictionary<string, string>();

        public static void AddUser(string username, string password)
        {
            _loginPasswordPairs.Add(username, password);
        }

        public static void AuthenticateUser(string username, string password)
        {
            if (_loginPasswordPairs.ContainsKey(username) && _loginPasswordPairs[username].Equals(password))
            {
                IsAuthenticated = true;
            }
            else
            {
                IsAuthenticated = false;
            }
        }
    }
}
