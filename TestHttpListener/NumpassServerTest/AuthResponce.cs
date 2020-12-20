using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NumpassServerTest
{
	[DataContract]
	public class AuthResponse
	{
		[JsonProperty("expiry")]
		[DataMember]
		public DateTimeOffset Expiry { get; set; }

		[JsonProperty("token")]
		[DataMember]
		public string Token { get; set; }

		[JsonProperty("user")]
		[DataMember]
		public NumpassUser User { get; set; }
	}
}