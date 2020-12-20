using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NumpassServerTest
{
	[DataContract]
	public class NumpassUser
	{
		[JsonProperty("username")]
		[DataMember]
		public string Username { get; set; }
	}
}