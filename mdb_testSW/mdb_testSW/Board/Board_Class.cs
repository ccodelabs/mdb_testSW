﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mdb_testSW
{
    abstract class Board_Class
    {
        //private State _state;

        public string BoardOperator { get; set; }
        public string SerialNumber { get; set; }
        public string BoardTime { get; set; }
        public string BoardCurrent { get; set; }
        public string BoardWorkstation { get; set; }
        public string BoardErrorDescription { get; set; }
        public int BoardTestStatus { get; set; }


        public class Test_types
        {
            public string Name { get; set; }
            public bool Status { get; set; }
        }
        List<Test_types> list_of_tests = new List<Test_types>();


        public abstract void StartTesting();


        //begin script
        public string[] Start_script(string arguments)
        {

            List<String> lines = new List<String>();

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "D:\\Trabalho\\CCode\\MDB_USB_MasterSlave\\C_Test_Suite\\Test_Suite\\Test_Suite\\Files\\mdb_test.exe",
                    //FileName = Path.GetFullPath(@"..\\Files\\mdb_test.exe"),
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            string line;
            proc.Start();
            if (!proc.WaitForExit(20000)) //20 seconds timeout
            {
                MessageBox.Show("No MDB Board Communication", "Check Cable Connection", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                Debug.WriteLine("Kill Process");
                proc.Kill();
            }
            while (!proc.StandardOutput.EndOfStream)
            {
                line = proc.StandardOutput.ReadLine();
                lines.Add(line);
            }
            String[] stringArray = lines.ToArray();
            return stringArray;
        }

        public abstract bool ConnectToMysql(string server, string database, string uid, string password);
        public abstract bool CheckRepeatedTest();
        public abstract void InsertTestResult();

    }
}
