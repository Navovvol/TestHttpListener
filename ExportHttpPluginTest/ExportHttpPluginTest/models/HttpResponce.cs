using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
	public class HttpResponce
	{
		[DataMember]
		[JsonProperty("httpStatusCode")]
		public int HttpStatusCode { get; set; }
		[DataMember]
		[JsonProperty("statusDescription")]
		public string StatusDescription { get; set; }
		[DataMember]
		[JsonProperty("responseUri")]
		public string ResponseUri { get; set; }
		[DataMember]
		[JsonProperty("contentLength")]
		public long ContentLength { get; set; }

		[DataMember]
		[JsonProperty("сontentEncoding")]
		public string ContentEncoding { get; set; }
		[DataMember]
		[JsonProperty("сontentType")]
		public string ContentType { get; set; }
		[DataMember]
		[JsonProperty("method")]
		public string Method { get; set; }

		[DataMember]
		[JsonProperty("server")]
		public string Server { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
