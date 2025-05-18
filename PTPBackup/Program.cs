using MediaDevices;

namespace PTPBackup
{
    internal class Program
    {
        private static string SRC_BASE = "\\Internal shared storage\\DCIM\\Camera";
        private static string TARGET_BASE = "C:\\Users\\449085\\Desktop\\TE";
        //private static string TARGET = ".";
        static void Main(string[] args)
        {
            // Hunt for phone
            MediaDevice? phone = MediaDevice.GetDevices().ToList().Where(dev => dev.Description == "motorola edge 50 pro").FirstOrDefault();
            try
            {
                if (phone != null)
                {
                    if (!phone.IsConnected)
                    {
                        phone.Connect();
                    }
                    // list files
                    string[] files = phone.GetFiles(SRC_BASE);
                    //foreach (var item in files)
                    //{
                    //    Console.WriteLine(item);
                    //}
                    Console.WriteLine(files.Length);
                    string file = files[0];
                    string[] parts = file.Split("\\");
                    string testFile = parts[parts.Length - 1]; ;
                    Console.WriteLine(testFile);
                    phone.DownloadFile(file, TARGET_BASE + "\\" + testFile);
                }
            }
            catch (NotConnectedException ncex)
            {
                Console.WriteLine(ncex.Message);
            }
            catch (UnauthorizedAccessException uex)
            {
                Console.WriteLine(uex.Message);
            }
            finally
            {
                if (phone != null)
                {
                    phone.Disconnect();
                }
            }
        }
    }
}
