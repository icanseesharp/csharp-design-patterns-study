using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Journal
    {
        public readonly List<string> Entries = new List<string>();
        public int Count = 0;
        public int AddEntry(string entry)
        {
            Entries.Add(string.Format("{0}: {1}\n", ++Count, entry));
            return Count;
        }

        public void RemoveEntry(int index)
        {
            Entries.RemoveAt(index);
        }

        //This method breaks Single Responsibility (SRP), to maintain SRP, we've implemented Persistence class
        public void Save(Journal j, string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if (!File.Exists(fileName) || overwrite)
            {
                File.WriteAllText(fileName, j.ToString());
            }
        }
    }
    public class SingleResponsibility
    {
        //public static void Main()
        //{
        //    Journal j = new Journal();
        //    j.AddEntry("Today I worked on SRP");
        //    j.AddEntry("Today I worked on UoW");

        //    Persistence p = new Persistence();
        //    var fileName = @"D:\temp.txt";

        //    p.SaveToFile(j, fileName, true);
        //    Process.Start(fileName);
        //}
    }
}
