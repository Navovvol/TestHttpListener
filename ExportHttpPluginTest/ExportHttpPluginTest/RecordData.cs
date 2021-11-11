using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestHttpListener
{
	public class CarRecognizedContext
	{
		[DataMember]
		[JsonProperty("recordData")]
		public RecordData? RecordDataContext { get; }

		public override string ToString() => JsonConvert.SerializeObject(this);
	}
	public class RecordData
	{
		[DataMember]
		[JsonProperty("timeStamp")]
		public DateTime TimeStamp { get; set; }

		[DataMember]
		[JsonProperty("plate")]
		public string Plate { get; set; }

		[DataMember]
		[JsonProperty("plateStencil")]
		public string PlateStencil { get; set; }

		[DataMember]
		[JsonProperty("status")]
		public string Status { get; set; }

		[DataMember]
		[JsonProperty("direction")]
		public int Direction { get; set; }

		[DataMember]
		[JsonProperty("passage")]
		public int Passage { get; set; }

		[DataMember]
		[JsonProperty("videoChannel")]
		public int VideoChannel { get; set; }

		[DataMember]
		[JsonProperty("videoChannelName")]
		public string VideoChannelName { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this);
	}
}
