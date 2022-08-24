using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Kit.library
{
    class WebUtils
    {

       /**
      * generate web request
      * 
      * */
        public static HttpWebRequest GenerateWebRequest(string credential, string url, string method)
        {
            // Have to use HttpWebRequest class in order to set the Accept property.
            // Can't set the Accept header directly - C# throws an error.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";
            request.Accept = "application/vnd.github.v3+json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36";

            if (credential.Contains(":"))
            {
                request.Headers.Add("Authorization", "Basic " +
                                                       Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credential)));
            }
            else
            {
                request.Headers.Add("Authorization", "token " + credential);
            }

            return request;
        }



        /**
        * generate raw file web request
        * 
        * */
        public static HttpWebRequest GenerateRawFileWebRequest(string credential, string url)
        {
            // Have to use HttpWebRequest class in order to set the Accept property.
            // Can't set the Accept header directly - C# throws an error.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/vnd.github.v3.raw+raw";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36";

            if (credential.Contains(":"))
            {
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credential)));
            }
            else
            {
                request.Headers.Add("Authorization", "token " + credential);
            }

            return request;
        }


        /**
        * get web response string
        * 
        * */
        public static async Task<string> GetRequestResponseString(HttpWebRequest request)
        {
            var responseAsync = await request.GetResponseAsync();
            var responseFromServer = "";
            using (var dataStream = responseAsync.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                var reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }
            // Close the response.
            responseAsync.Close();

            return responseFromServer;
        }


        /**
        * get web response
        * 
        * */
        public static async Task<WebResponse> GetRequestResponse(HttpWebRequest request)
        {
            var responseAsync = await request.GetResponseAsync();
            return responseAsync;
        }


        /**
        * ignore SSL
        * 
        * */
        public static void IgnoreSSL()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

    }
}
