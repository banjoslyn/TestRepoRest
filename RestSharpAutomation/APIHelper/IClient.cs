using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public interface IClient : IDisposable  //This interface is inheriting from the IDispoable Interface of the .net framework
    {

        RestClient GetClient(); //Whatever class implements this interface will provide an implementation of this method.



    }
}
