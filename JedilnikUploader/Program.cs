using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;

namespace JedilnikUploader
{
    class Program
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            bool internet = CheckForInternetConnection();
            if (internet) {
                try {
                    string text = System.IO.File.ReadAllText(@"C:\Jedilnik\jedilnik.csv");
                    uploadString("http://app.gimvic.org/APIv2/jedilnikAPI/uploadFile.php", text);
                    uploadString("http://gimvicapp.404.si/menuUpload", text);
                } catch {}
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public static void uploadString(String url, String text) { 

            WebClient webClient = new WebClient();

            NameValueCollection formData = new NameValueCollection();
            formData["data"] = text;

            byte[] responseBytes = webClient.UploadValues(url, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            webClient.Dispose();
        }
    }
}
