using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MenagerOfFiles.DiscElements
{

    class MyFile : ElementsOfDisc
    {
        private string path2;

        public MyFile(string path2) : base(path2)
        {
            this.path2 = path2;
        }
        public string Path2
        {
            get
            {
                return path2;
            }
        }
        public override string Name
        {
            get
            {
                FileInfo info = new FileInfo(DiscPath);
                return info.Name;
            }
        }
        public List<MyFile> GetAllFiles()
        {
            string[] subFiles = Directory.GetFiles(DiscPath);

            List<MyFile> result = new List<MyFile>();
            foreach (string file in subFiles)
            {
                result.Add(new MyFile(file));
            }
            return result;

        }
        
        public long SizeOfFile
        {
            
            get
            {
                
                FileInfo info = new FileInfo(path2);
                return info.Length/1000;
            }
        }
        public override DateTime GetCreationTime
        {
            get
            {
                return Directory.GetCreationTime(path2);
            }
        }
    }


}
