using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public interface IJiraClient : IClient
    {
        void Login(IJiraUser user);
        void Logout();

    }
    public interface IJiraUser { } //We don't need any specific behaviour for Users, so this is an empty interface or marker interface.
}
