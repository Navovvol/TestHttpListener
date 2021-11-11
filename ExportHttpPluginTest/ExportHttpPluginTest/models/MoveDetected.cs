using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class MoveDetected
    {
		[DataMember]
		[JsonProperty("videoChannelId")]
		public int VideoChannelId { get; set; }

		[DataMember]
		[JsonProperty("videoChannelName")]
		public string VideoChannelName { get; set; }

		[DataMember]
		[JsonProperty("hasMotion")]
		public bool HasMotion { get; set; }

		[DataMember]
		[JsonProperty("timestamp")]
		public DateTimeOffset DateTimeOffset { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
