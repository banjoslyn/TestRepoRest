using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Command;
using System.IO;
using FluentAssertions;
using RestSharp;
using System.Data;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class TestFileDownloadUsingFramework
    {
        private static IClient client;
        private static IClient authClient;
        private static RestApiExecutor restApiExecutor;
        private static RestClientOptions options;

        private readonly string BaseUrl = "https://content.dropboxapi.com/2";
        private static string Token = "sl.u.AFir9JRDNCUaxmAYbJColKkSPbUSXOeFWTy7G6ikzbxauO13R2MstKBUBNn-41T_unxvuBcflY5tr-PO_A_VjMLcqSyV0lutGPLkodJnjTJoQGsxl65zlE_J5nzeVgsgv092qKWohecKUBPh4E_4wRU7GnmmKl9yFvzWJTIrc5hoXTzXKzfIgY2tQQuiqv92HVInxw0QX-NBbZGb5owx42fLsXGi5StVTLhOCyiLPKMayPYmLWSf1A8z38Ss8iItHHyAzXBKxYqu_irSPYfQ6Rw3TNF42vibFLHhVwa8tSzEqaJf518jjduxPbq8Q9sAyd9iqOICBhuFC0e3tQRf_0kqmSoC96GY22vW0tmilmhx1bzmZ_rvV_c7AEM2SkUJhDCePgUe32Z6LS2az8_j_IoUQAUFCAGlfKdYsVb_UhOmhq5AxkDcKdJecHQr59VvSEsy-hMu3DFGMJ2_HVTIFVBwy43uZbkkVfqclKq9knG2yIzFpe25SGaWeqRYQMhPyy1fxg4-wfMSK_ZNDFCOYH_Qq1diLq5EpIJ5mGnEBccMGtodABAmp5PEIoo0QTF8DBjCwpRSjhhCG8TEA3SGrYbig0-MbnPKE1V1NyKGDvtb3gQ5PStTE2Dv9nV49-6uOeZ07dJEoehkUkr5OaEHlFmzyzwye_zU_cnobr7XwUcUbAJAEfusAwOZPwqc-hrKUNzfvKHQtR9T0-__Hpu6R8jBzjw77CkM6U7cIn5BF9zmen1eRY9OU3OlluISMDcP9gD9GhI8iwAxZ1yvitiIQDwkPTCN2Murt1QES5HCwEXAZS4WkkufgO-9BjB4llfUYuK8gE_fZr26mKg29u5wi5Vf0AmFcUTw9alHpbD_8E4ciIsmZaUbZKCwJ2YF4sJ6rvCDtHzRhvfID4gR8uVyvZONRFsrCRJN_rsb7o5XwtR3vaZ_7J9MVYL97m5ozu17M9bOFqFGk4nro5Rs2bF5c989ku0k4CGONdmqKT-LA-g3bffee8MYCA5MQM2IMoxoFanVGnbSRJKpYfHSL7Aq9xbkUUMt_6CNj70s2T5ptLHE8ehuqyafvMkbbj8tA90U8eQoMijLr2eHKOZoQK-zwF4xas1r_PcW9V9EkiTx4VN5zY-3CMVu9XT93_cSjbIWpkYHIFwLoPYE4GImPIZeNZ4iCTAr1-UzPYH1qdbIkdC75PyFitY3edyrUYTCMfQ_Riers1htAaMljBi3KIQYE6xTk0eyIG9ZoEUaZ3LpbmE6UowWM9vuMaz9WffAv7xYamwFcrRqF-_h8_fFuxnE_oQChQdSD3vF1CFnal8rY-k8wnd6PfK31fHH_xXWW8uw6WRbjrqliglO1AolFYZAp9QISxmRBAXDiTBuTCG0itZgSnRs0wP1_f-o6TlDcIpBmwGc8FvpUScgu3iiWERFcofQvdvok1_yFzbANjW0Kfm4Sg";
        //Above Token is for the DropBox application MyClientBANJ


        //ClassInitialize - a method that contains code that must be used before any of the tests in the test class have run
        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            var options = new RestClientOptions
            {

                ThrowOnAnyError = true
            };
            // Create the Tracer Client
            client = new TracerClient(options);
            // Invoke the decorator with the tracer client impl to associate the token information.
            authClient = new AuthenticationDecorator(client, new JwtAuthenticator(Token));
            // Create the Executor
            restApiExecutor = new RestApiExecutor();           

        }

        //ClassCleanup - a method that contains code to be used after all the tests in the test class have run
        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        public void DownloadFile_UsingFramework()
        {
            
            var fileName = "RestSharp_API_Framework_Usage.png";
            var contextPath = "/files/download";

            var location = "{\"path\":\"/" + fileName + "\"}";

            var postRequest = new PostRequestBuilder().WithUrl(BaseUrl +
                contextPath).WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", location } });

            var command = new DownloadRequestCommand(postRequest, authClient);
            restApiExecutor.SetCommand(command);

            var data = restApiExecutor.DowloadData();
            File.WriteAllBytes("NewNew " + fileName, data);
            File.Exists("NewNew " + fileName).Should().BeTrue();

        }

        // Assignment - Parallel Download of Multiple Files using Framework API
        [TestMethod]
        public void DownloadFiles_UsingFramework()
        {

            var contextPath = "/files/download";

            // Meta data for the first file
            var fileNameOne = "WIN_20240705_13_30_21_Pro.jpg";
            var locationOne = "{\"path\":\"/" + fileNameOne + "\"}";

            // Meta data for the second file
            var fileNameTwo = "RestSharp_API_Framework_Usage.png";
            var locationTwo = "{\"path\":\"/" + fileNameTwo + "\"}";



            // Create the Post Request for first file
            var postRequest1 = new PostRequestBuilder().WithUrl(BaseUrl +
                contextPath).WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", locationOne } });

            // Create the Post Request for second file
            var postRequest2 = new PostRequestBuilder().WithUrl(BaseUrl +
                contextPath).WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", locationTwo } });

            // Create the DownloadRequestCommand for the first post request
            var command = new DownloadRequestCommand(postRequest1, authClient);

            // Set the command for the RestApiExecutor.
            restApiExecutor.SetCommand(command);

            // Call DownloadDataAsync method to download first file.
            var data1 = restApiExecutor.DownloadDataAsync(); /* A separate thread, to the thread being used for the test itself, is created for this task.*/



            // Create the DownloadRequestCommand for the second request
            command = new DownloadRequestCommand(postRequest2, authClient);

            // Set the command for the RestApiExecutor.
            restApiExecutor.SetCommand(command);

            // Call DownloadDataAsync method to download second file.
            var data2 = restApiExecutor.DownloadDataAsync(); ; /* Another separate thread, to the thread being used for the test itself and 
                                                                       * the thread of task1, is created for task2.*/

            // Wait for download task
            Task.WaitAll(data1, data2);

            File.WriteAllBytes("New1fileone " + fileNameOne, data1.Result);
            File.WriteAllBytes("New2filetwo " + fileNameTwo, data2.Result);

            File.Exists("New1fileone " + fileNameOne).Should().BeTrue();
            File.Exists("New2filetwo " + fileNameTwo).Should().BeTrue();

        }

    }
}
