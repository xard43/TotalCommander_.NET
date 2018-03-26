using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MenagerOfFiles.DiscElements
{
    public abstract class ElementsOfDisc
    {
        private string discpath;

        public ElementsOfDisc(string discpath)
        {
            this.discpath = discpath;
        }

        public string DiscPath
        {
            get
            {
                return discpath;
            }
        }


        public abstract DateTime GetCreationTime
        {
            get;
        }
        //public string GetCreationTime
        //{
        //    get
        //    {
        //        DateTime a;
        //        a = Directory.GetCreationTimeUtc(discpath);
        //        return a.ToString();
        //    }
        //}

        public abstract string Name
        {
            get;
        }

        
    }
}
