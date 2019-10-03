using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProcessNotes
{
    public sealed class DataHandler
    {


        private static readonly object locker = new object();
        private static DataHandler instance = null;
        private int selectedIndex;
        private Process[] processList;

        public Process[] ProcessList
        {
            get
            {
                Process[] copy = new Process[processList.Length];
                Array.Copy(processList, copy, processList.Length);
                return copy;
            }
            private set { }
        }
        private DataHandler()
        {
            processList = Process.GetProcesses();
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
            }
        }

        Dictionary<int, string> Comments = new Dictionary<int, string>();

        public static DataHandler Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new DataHandler();
                    }
                    return instance;
                }
            }
        }

        public string Get(int key)
        {
            string result = "";

            if (Comments.ContainsKey(key))
            {
                result = Comments[key];
            }

            return result;
        }


        public void Set(int key, string value)
        {
            if (Comments.ContainsKey(key))
            {
                Comments[key] = value;
            }
            else
            {
                Comments.Add(key, value);
            }
        }

        

        public bool ContainsID(int ID)
        {
            return Comments.Keys.Contains(ID);
        }
    }
}
    