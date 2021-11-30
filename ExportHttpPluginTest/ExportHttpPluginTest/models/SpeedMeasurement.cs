using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
    public class SpeedMeasurement
    {
		[DataMember]
		[JsonProperty("record")]
		public Record RecordContext { get; set; }

		[DataMember]
		[JsonProperty("videoChannelId")]
		public int VideoChannelId { get; set; }

		[DataMember]
		[JsonProperty("videoChannelName")]
		public string VideoChannelName { get; set; }

		[DataMember]
		[JsonProperty("speed")]
		public double Speed { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
