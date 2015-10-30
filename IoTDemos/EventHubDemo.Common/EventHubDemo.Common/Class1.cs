using System.Runtime.Serialization;


namespace EventHubDemo.Common.Contracts

    [DataContract]
    public class MetricEvent    {
        [DataMember]
        public int DeviceId { get; set; }
        [DataMember]
       public int Temperature { get; set; }
    }
}
