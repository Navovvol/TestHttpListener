using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace TestHttpListener
{
	public class RFIDReader
	{
		[DataMember]
		[JsonProperty("readerId")]
		public string ReaderId { get; set; }

		[DataMember]
		[JsonProperty("readerName")]
		public string ReaderName { get; set; }

		[DataMember]
		[JsonProperty("identifierData")]
		public string IdentifierData { get; set; }
		[DataMember]
		[JsonProperty("timestamp")]
		public DateTime? TimeStamp { get; set; }
		[DataMember]
		[JsonProperty("antennas")]
		public int[] Antennas { get; set; }
		[DataMember]
		[JsonProperty("plateNumber")]
		public string Plate { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}

}
