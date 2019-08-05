using System;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace RulesBasedAlertSystem
{
    class ParseJson
    {
        public void Parse()
        {
            PatientInfo[] AllData;
            try
            {
                using (var stream = File.Open("test.json", FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(PatientInfo[]));
                    var data = (PatientInfo[])serializer.ReadObject(stream);
                    AllData = data;
                    AlertSystem altSystem = new AlertSystem();
                    altSystem.MainCheck(AllData);
                }
            }
            catch (SerializationException)
            {
                Console.WriteLine("File Is Empty");

            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Please specify a proper path.");
            }
        }
    }

}


