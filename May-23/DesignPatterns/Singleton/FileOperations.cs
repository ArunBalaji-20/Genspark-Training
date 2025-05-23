using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Singleton
{
    public class FileOperations
    {
        private static FileOperations instance;
        private string path;

        private FileStream fileStream;
        private StreamWriter writer;
        private StreamReader reader;
        private FileOperations(string path) 
        {
            this.path = path;
            OpenFile();
        }

        public static FileOperations GetInstance(string path)
        {
            if (instance == null)
            {
                instance = new FileOperations(path);
                return instance;
            }
            return instance;
        }

        private void OpenFile()
        {
            fileStream = new FileStream(path,FileMode.OpenOrCreate, FileAccess.ReadWrite);
            reader= new StreamReader(fileStream);
            writer = new StreamWriter(fileStream);
        }
        public void ReadContents()
        {
            fileStream.Seek(0, SeekOrigin.Begin);
            string content= reader.ReadToEnd();
            Console.WriteLine("--contents---");
            Console.WriteLine(content);
        }

        public void WriteContents(string content)
        {
            writer.Write(content);
            writer.Flush();
            Console.WriteLine("content written");
        }
        public void Dispose()
        {
            writer?.Dispose();
            reader?.Dispose();
            fileStream?.Dispose();
            Console.WriteLine("Resources closed");
        }
        //close the readers ,writers
        //initialize the readers writers in constructor
    }
}


//----------testing
//    public class FileOperations
//{
//    private static FileOperations instance;
//    private string name { get; set; } = string.Empty;
//    private FileOperations() { }

//    public static FileOperations GetInstance(string n)
//    {
//        if (instance == null)
//        {
//            instance = new FileOperations();
//            instance.name = n;
//            return instance;
//        }
//        return instance;
//    }

//    public void TestFunction()
//    {
//        Console.WriteLine($"Testfunction called by: {name}");
//    }
//}