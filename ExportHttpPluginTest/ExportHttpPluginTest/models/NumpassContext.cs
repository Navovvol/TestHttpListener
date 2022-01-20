using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class NumpassContext
	{
		[DataMember]
		[JsonProperty("record")]
		public Record RecordContext { get; set; }

		[DataMember]
		[JsonProperty("paymentState")]
		public string PaymentState { get; set; }

		[DataMember]
		[JsonProperty("responseStatus")]
		public string ResponseStatus { get; set; }
		
		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}