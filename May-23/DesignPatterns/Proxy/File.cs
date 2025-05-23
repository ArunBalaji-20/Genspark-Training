using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy
{
    public class File : IFile
    {
        private string FileName = "C:\\Notes\\may23\\sample.txt";
        public void Read()
        {
            try
            {
                Console.WriteLine("[Access Granted] Reading sensitive file content...");

                FileStream fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);
                string content = reader.ReadToEnd();
                Console.WriteLine("--contents---");
                Console.WriteLine(content);

                reader?.Dispose();
                fileStream?.Dispose();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
