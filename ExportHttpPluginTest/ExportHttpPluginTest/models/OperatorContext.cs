using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class OperatorContext
	{
		[DataMember]
		[JsonProperty("dateTimeEvent")]
		public DateTime DateTimeEvent { get; set; }

		[DataMember]
		[JsonProperty("operatorUsername")]
		public string OperatorUsername { get; set; }

		[DataMember]
		[JsonProperty("operatorDisplayName")]
		public string OperatorDisplayName { get; set; }

		[DataMember]
		[JsonProperty("isOperatorOnWorkPlace")]
		public bool IsOperatorOnWorkPlace { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
