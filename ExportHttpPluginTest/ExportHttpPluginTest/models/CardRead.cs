using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExportHttpPluginTest
{
	public class CardRead
	{
		[DataMember]
		[JsonProperty("accessCardNumber")]
		public int AccessCardNumber { get; set; }
		[DataMember]
		[JsonProperty("rawNumbers")]
		public int[] RawNumbers { get; set; }
		[DataMember]
		[JsonProperty("cardReaderName")]
		public string CardReaderName { get; set; }
		[DataMember]
		[JsonProperty("cardReaderGuid")]
		public string CardReaderGuid { get; set; }
		[DataMember]
		[JsonProperty("plateNumber")]
		public string Plate { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
