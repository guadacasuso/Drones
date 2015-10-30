using System.Runtime.Serialization;


namespace DroneEH.Common.Contracts
{
    [DataContract]
    public class MetricEvent    {

        [DataMember]
        public int DroneId { get; set; }
        [DataMember]
        public int Altitude { get; set; }
       

    }
}
