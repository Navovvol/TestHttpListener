using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TestHttpListener
{
	public class VideoSignalStateContext
	{
		[DataMember]
		[JsonProperty("videoChannelOrderId")]
		public int VideoChannelOrderId { get; set; }

		[DataMember]
		[JsonProperty("videoChannelName")]
		public string VideoChannelName { get; set; }

		[DataMember]
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }

		[DataMember]
		[JsonProperty("videoSignalState")]
		public string VideoSignalState { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
