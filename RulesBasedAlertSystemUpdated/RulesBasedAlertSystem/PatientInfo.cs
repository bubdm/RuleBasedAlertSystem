using System;
using System.Runtime.Serialization;


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
}
