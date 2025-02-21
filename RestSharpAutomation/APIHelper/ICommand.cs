using Microsoft.Testing.Platform.Extensions.TestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public interface ICommand
    {
        IResponse ExecuteRequest(); //Method for receiving the Response in string format.
        IResponse<T> ExecuteRequest<T>(); //Method for receiving the Response in a de-serialize object.

        byte[] DownloadData();
        Task<byte[]> DownloadDataAsync();


    }
}
