using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using RulesBasedAlertSystem;

namespace RulesBasedAlertSystem
{
    [TestFixture]

    public class Test : AlertSystem
    {

        public static int val;

        [Test]
        [TestCaseSource("DivideCases")]
        public void SPO2TC0(int checkspo2)
        {
            Assert.AreEqual(0, checkspo2);
        }

        [Test]
        [TestCaseSource("DivideCases")]
        public void SPO2TC1(int CheckSPO2)
        {
            if (CheckSPO2 == 0)
                CheckSPO2 = 1;
            Assert.AreEqual(1, CheckSPO2);
        }

        [Test]
        [TestCaseSource("DivideCases")]
        public void PRTC1(int CheckPR)
        {
            if (CheckPR == 0)
                CheckPR = 1;
            Assert.AreEqual(1, CheckPR);
        }

        [Test]
        [TestCaseSource("DivideCases")]
        public void PRTC0(int CheckPR)
        {

            Assert.AreEqual(0, CheckPR);
        }

        [Test]
        [TestCaseSource("DivideCases")]
        public void TempTC0(int CheckTemp)
        {
            if (CheckTemp == 0)
                CheckTemp = 1;
            Assert.AreEqual(1, CheckTemp);
        }

        [Test]
        [TestCaseSource("DivideCases")]
        public void TempTC1(int CheckTemp)
        {

            Assert.AreEqual(0, CheckTemp);
        }

        static object[] DivideCases =
        {
            new object[] {val}
        };


        public void GetValueSPO2(int spo2)
        {

            AlertSystem obj = new AlertSystem();
            val = spo2;

        }

        public void GetValuePR(int pr)
        {
            AlertSystem obj1 = new AlertSystem();
            val = pr;
        }

        public void GetValueTemp(int temp)
        {
            AlertSystem obj2 = new AlertSystem();
            val = temp;
        }





    }

}
