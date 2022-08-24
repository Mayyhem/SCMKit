using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Octokit;

namespace Kit.library
{
    class GitHubUtils
    {

       /**
       * authenticate to GitHub
       * 
       * */
        public static dynamic AuthToGitHub(string credentials, string url)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                GitHubClient client = null;
                Credentials auth = null;
                Uri uri = new Uri(url);

                client = new GitHubClient(new ProductHeaderValue("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36"), uri);

                // if username/password auth being used
                if (credentials.Contains(":"))
                {
                    string[] theCreds = credentials.Split(':');
                    auth = new Credentials(theCreds[0], theCreds[1]);
                }

                // if token auth being used
                else
                {
                    auth = new Credentials(credentials);
                }

                client.Credentials = auth;

                return client;

            }

            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("[-] ERROR: Could not authenticate to URL with credentials provided. Exception: " + ex.ToString());
                Console.WriteLine("");
                Environment.Exit(1);
                return null;
            }

        }


    }

}
