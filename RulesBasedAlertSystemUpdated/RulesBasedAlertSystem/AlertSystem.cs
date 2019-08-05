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
    public class AlertSystem
    {
        public static int CheckSPO2 = 0;
        public static int CheckTemp = 0;
        public static int CheckPulseRate = 0;
        public void MainCheck(PatientInfo[] items)
        {
            Test testobj = new Test();
            //TIMER
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < items.Length; i++)
            {
                while (timer.Elapsed.TotalSeconds < 10) ;
                bool check = true;
                int checkPR = 0;
                bool checkT = true;

                int spo2lower = 0;
                int spo2higher = 100;
                int prlower = 30;
                int prhigher = 254;
                int templower = 97;
                int temphigher = 99;

                CheckSPO2 = 0;
                CheckTemp = 0;
                CheckPulseRate = 0;

                Console.WriteLine("======================");

                if (timer.Elapsed.Seconds == 10)
                {
                    int spo2high = 95;
                    int spo2mid1 = 91;
                    int spo2mid2 = 90;
                    int spo2low = 70;
                    String Spo2No = "SPO2 Critical? : No";
                    String Spo2Yes = "SPO2 Critical? : Yes";


                    // Check for Oxygen Saturation(SPO2)
                    if (items[i].SPO2 > spo2high)
                    {
                        Console.WriteLine(Spo2No);
                        Console.WriteLine("Normal healthy individual ");
                        check = true;
                    }
                    else if (items[i].SPO2 >= spo2mid1 && items[i].SPO2 <= spo2high && check == true)
                    {
                        Console.WriteLine(Spo2No);
                        Console.WriteLine(
                            "Clinically acceptable, but low. Patient may be a smoker, or be unhealthy.");
                        check = true;

                    }
                    else if (items[i].SPO2 >= spo2low && items[i].SPO2 <= spo2mid2 && check == true)
                    {
                        Console.WriteLine(Spo2Yes);
                        Console.WriteLine("Hypoxemia. Unhealthy and unsafe level. ");
                        check = false;


                    }
                    else if (items[i].SPO2 < spo2low && check == true)
                    {
                        Console.WriteLine(Spo2Yes);
                        Console.WriteLine("Extreme lack of oxygen, ischemic diseases may occur.");
                        check = false;

                    }

                    int prlow = 40;
                    int prmid = 60;
                    int prmid2 = 100;
                    int prhigh = 220;

                    String PRNo = "PR Critical? : No";
                    String PRYes = "PR Critical? : Yes";


                    // Check for Pulse Rate(PR)


                    if (items[i].pulseRate < prlow && checkPR == 0)
                    {
                        Console.WriteLine(PRYes);
                        Console.WriteLine("Below healthy resting heart rates.");
                        checkPR = 1;


                    }
                    else if (items[i].pulseRate >= prlow && items[i].pulseRate <= prmid && checkPR == 0)
                    {
                        Console.WriteLine("PR Critical? : Yes/No");
                        Console.WriteLine("Resting heart rate for sleeping.");
                        checkPR = 2;


                    }
                    else if (items[i].pulseRate >= prmid && items[i].pulseRate <= prmid2 && checkPR == 0)
                    {
                        Console.WriteLine(PRNo);
                        Console.WriteLine("Healthy adult resting heartrate.");


                    }

                    else if (items[i].pulseRate >= prmid2 && items[i].pulseRate <= prhigh && checkPR == 0)
                    {
                        Console.WriteLine(PRYes);
                        Console.WriteLine(
                            "Acceptable if measured during exercise. Not acceptable if resting heartrate.");
                        checkPR = 1;


                    }
                    else if (items[i].pulseRate > prhigh && checkPR == 0)
                    {
                        Console.WriteLine(PRYes);
                        Console.WriteLine("Abnormally high heart rate. ");
                        checkPR = 1;

                    }


                    int templow = 97;
                    int temphigh = 99;

                    if (items[i].temperature >= templow && items[i].temperature <= temphigh && checkT == true)
                    { Console.WriteLine("Normal Temperature"); }
                    else
                    {
                        Console.WriteLine("Abnormal Temperature");
                        checkT = false;
                    }


                    if (items[i].SPO2 < spo2lower || items[i].SPO2 > spo2higher)
                        CheckSPO2 = 1;

                    if (items[i].pulseRate < prlower || items[i].pulseRate > prhigher)
                        CheckPulseRate = 1;

                    if (items[i].temperature < templower || items[i].temperature > temphigher)
                        CheckTemp = 1;
                    
                    //TESTING 

                    //SPO2
                    testobj.GetValueSPO2(CheckSPO2);
                    if (CheckSPO2 == 1)
                        testobj.SPO2TC1(CheckSPO2);
                    else
                        testobj.SPO2TC0(CheckSPO2);

                    //PULSE RATE

                    testobj.GetValuePR(CheckPulseRate);
                    if (checkPR == 0)
                        testobj.PRTC0(CheckPulseRate);
                    else

                        testobj.PRTC1(CheckPulseRate);

                    //TEMPERATURE

                    testobj.GetValueTemp(CheckTemp);
                    if (CheckTemp == 1)
                        testobj.TempTC0(CheckTemp);
                    else
                        testobj.TempTC1(CheckTemp);

                }

                timer.Restart();
                if (check != true || checkT != true || checkPR != 0)
                    Console.WriteLine("CRITICAL");
                else
                {
                    Console.WriteLine("HEALTHY");
                }

                Console.WriteLine("SPo2: " + items[i].SPO2 + "Pulse Rate: " + items[i].pulseRate + "Temperature: " + items[i].temperature);


            }
            timer.Stop();
        }



        static void Main(string[] args)
        {

            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("          R U L E     B A S E D     A L E R T     S Y S T E M     ");
            Console.WriteLine("------------------------------------------------------------------");
            //LoadJson();
            ParseJson obj = new ParseJson();
            obj.Parse();

            //new CaseStudy1.Json();
            //Json objJson = new Json();

        }
    }



}


