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

    [DataContract]
    public class PatientInfo
    {
        [DataMember] public string pid;
        [DataMember] public double SPO2;
        [DataMember] public int pulseRate;
        [DataMember] public double temperature;
    }

    public class AlertSystem 
    {
        public static void ParseJson()
        {
            PatientInfo[] AllData;
            try
            {
                using (var stream = File.Open("test.json", FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(PatientInfo[]));
                    var data = (PatientInfo[]) serializer.ReadObject(stream);
                    AllData = data;

                    MainChecker(AllData);
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

        public static int CheckSPO2 = 0;
        public static int CheckTemp = 0;
        public static int CheckPulseRate = 0;
        public static void  MainChecker(PatientInfo[] items)
        {   
            
            Test testobj = new Test();
            //TIMER
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < items.Length; i++)
            {
                while (timer.Elapsed.TotalSeconds < 2) ;
                int check = 0;
                int checkPR = 0;
                int checkT = 0;
                CheckSPO2 = 0;
                CheckTemp = 0;
                CheckPulseRate = 0;

                if (items[i].SPO2 > 100 || items[i].SPO2 < 0)
                    CheckSPO2 = 1;
                
                if (items[i].pulseRate > 254 || items[i].pulseRate < 30)
                    CheckPulseRate = 1;
                
                if (items[i].temperature < 97 || items[i].temperature > 99)
                    CheckTemp = 1;
                

            

                Console.WriteLine("======================");
               
                if (timer.Elapsed.Seconds == 2)
                {
                    

                    // Check for Oxygen Saturation(SPO2)
                    if (items[i].SPO2 > 95)
                    {
                         Console.WriteLine("SPO2 Critical? : No");
                         Console.WriteLine("Normal healthy individual ");
                         check = 0;
                    }
                    else if (items[i].SPO2 >= 91 && items[i].SPO2 <= 95 && check == 0)
                    {
                        Console.WriteLine("SPO2 Critical? : No");
                        Console.WriteLine(
                            "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.");
                        check = 0;
                    
                    }
                    else if (items[i].SPO2 >= 70 && items[i].SPO2 <= 90 && check == 0)
                    {
                        Console.WriteLine("SPO2 Critical? : Yes");
                        Console.WriteLine("Hypoxemia. Unhealthy and unsafe level. ");
                        check = 1;
                        
                        
                    }
                    else if (items[i].SPO2 < 70 && check == 0)
                    {
                        Console.WriteLine("SPO2 Critical? : Yes");
                        Console.WriteLine("Extreme lack of oxygen, ischemic diseases may occur.");
                        check = 1;
                      
                    }
                    
                    
                    // Check for Pulse Rate(PR)
                    
                    
                    if (items[i].pulseRate < 40 && checkPR == 0)
                    {
                        Console.WriteLine("PR Critical? : Yes");
                        Console.WriteLine("Below healthy resting heart rates.");
                        checkPR = 1;
                        
                        
                    }
                    else if (items[i].pulseRate >= 40 && items[i].pulseRate <= 60 && checkPR == 0)
                    {
                        Console.WriteLine("PR Critical? : Yes/No");
                        Console.WriteLine("Resting heart rate for sleeping.");
                        checkPR = 2;
                        
                        
                    }
                    else if (items[i].pulseRate >= 60 && items[i].pulseRate <= 100 && checkPR == 0)
                    {
                        Console.WriteLine("PR Critical? : No");
                        Console.WriteLine("Healthy adult resting heartrate.");
                        

                    }

                    else if (items[i].pulseRate >= 100 && items[i].pulseRate <= 220 && checkPR == 0)
                    {
                            Console.WriteLine("PR Critical? : Yes");
                            Console.WriteLine(
                                "Acceptable if measured during exercise. Not acceptable if resting heartrate.");
                            checkPR = 1;
                            
                           
                    }
                    else if (items[i].pulseRate > 220 && checkPR==0)
                    {
                        Console.WriteLine("PR Critical? : Yes");
                        Console.WriteLine("Abnormally high heart rate. ");
                        checkPR = 1;
                        
                    }

                    
                    
                    if (items[i].temperature >= 97 && items[i].temperature <= 99 && checkT == 0)
                    { Console.WriteLine("Normal Temperature"); }
                    else
                    {
                        checkT = 1;
                    }

                    //TESTING 

                    //SPO2
                    testobj.GetValueSPO2(CheckSPO2);
                    if(CheckSPO2==1)
                    testobj.SPO2TC1(CheckSPO2);
                    else
                    testobj.SPO2TC0(CheckSPO2);

                    //PULSE RATE

                    testobj.GetValuePR(CheckPulseRate);
                    if(checkPR==0)
                    testobj.PRTC0(CheckPulseRate);
                    else
                   
                    testobj.PRTC1(CheckPulseRate);

                    //TEMPERATURE

                    testobj.GetValueTemp(CheckTemp);
                    if(CheckTemp==0)
                    testobj.TempTC0(CheckTemp);
                    else
                    testobj.TempTC1(CheckTemp);
                    
                }

                timer.Restart();
                if(check!=0||checkT!=0||checkPR!=0)
                    Console.WriteLine("CRITICAL");
                else
                {
                    Console.WriteLine("HEALTHY");
                }
            }
            timer.Stop();
        }


        static void Main(string[] args)
        {

            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("          R U L E     B A S E D     A L E R T     S Y S T E M     ");
            Console.WriteLine("------------------------------------------------------------------");
            //LoadJson();
            ParseJson();
            //new CaseStudy1.Json();
            //Json objJson = new Json();

        }
    }



}
