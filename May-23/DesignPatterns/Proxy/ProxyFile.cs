using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy
{
    public class ProxyFile : IFile
    {
        private IFile _file;
        private string FileName = "C:\\Notes\\may23\\sample.txt";
        private User user;
        public ProxyFile(User users)
        {
            user = users;
        }
        public void Read()
        {
            switch (user.Role.ToLower())
            {
                case "admin":
                    _file = new File();
                    _file.Read(); // calling real file class to read the contents.
                    break;

                case "user":
                    try
                    {
                        Console.WriteLine($"[Limited Access] Showing metadata of {FileName} only.");
                        FileInfo fileInfo = new FileInfo(FileName);
                        Console.WriteLine($" Size: {fileInfo.Length} bytes");
                        Console.WriteLine($" Created: {fileInfo.CreationTime}");
                        break;
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    

                case "guest":
                default:
                    Console.WriteLine("[Access Denied] You do not have permission to read this file.");
                    break;
            }

        }
    }
}
