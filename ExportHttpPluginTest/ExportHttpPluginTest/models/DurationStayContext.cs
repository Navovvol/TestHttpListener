using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class DurationStayContext
	{
		[DataMember]
		[JsonProperty("quantity")]
		public int Quantity { get; set; }
		
		[DataMember]
		[JsonProperty("recordsList")]
		public List<Record> RecordsListContext { get; set; }
		
		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}