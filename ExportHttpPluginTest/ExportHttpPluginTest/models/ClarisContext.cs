using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class ClarisContext
	{
		[DataMember]
		[JsonProperty("plate")]
		public string Plate { get; set; }
		[DataMember]
		[JsonProperty("passesCount")]
		public int PassesCount { get; set; }
		[DataMember]
		[JsonProperty("passage")]
		public string Passage { get; set; }
		[DataMember]
		[JsonProperty("videoChannelOrderId")]
		public int? VideoChannelOrderId { get; set; }
		
		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}