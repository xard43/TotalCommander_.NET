using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MenagerOfFiles.DiscElements
{
    class MyDirectory : ElementsOfDisc
    {
        private string path2;
        
        public MyDirectory(string path2) : base(path2)
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
        public MyDirectory PathMyDir
        {
            get
            {
                return new MyDirectory(path2);
            }
        }
        public override string Name
        {
            get
            {
                DirectoryInfo name = new DirectoryInfo(DiscPath);
                return name.Name;
            }
        }
        public List<MyDirectory> GetSubDirs()
        {
           
            string[] SubDirs = Directory.GetDirectories(path2);
            List<MyDirectory> result = new List<MyDirectory>();
            foreach (string dir in SubDirs)
            {
                result.Add(new MyDirectory(dir));
            }
           return result;
        }

        public string ParentDir
        {
            get
            {
                
                return Path.GetDirectoryName(path2);
            }
        }
        public override DateTime GetCreationTime
        {
            get
            {
                return Directory.GetCreationTime(path2);
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
       
        public List<ElementsOfDisc> GetAllFilesAndDirectories()
        {
            List<ElementsOfDisc> result = new List<ElementsOfDisc>();
            result.AddRange(GetSubDirs());
            result.AddRange(GetAllFiles());
            return result;
        }
        public IOrderedEnumerable<ElementsOfDisc> GetFilesAndDirectoriesSoryByCreationTime()
        {

            List<ElementsOfDisc> result = GetAllFilesAndDirectories();

            var sortedList = result.OrderBy(p => p.GetCreationTime);
            sortedList.Reverse();
            return sortedList;


        }
        //public List<ElementsOfDisc> GetFilesAndDirectoriesSoryByCreationTime()
        //{
        //    List<ElementsOfDisc> result = new List<ElementsOfDisc>();
        //    result.AddRange(GetAllFilesAndDirectories());

        //    result = result.OrderByDescending(a => a.GetCreationTime).ToList();
        //    return result;
        //}
    }
}
