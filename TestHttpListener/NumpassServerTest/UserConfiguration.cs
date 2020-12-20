using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NumpassServerTest
{
	[DataContract]
	public class UserConfiguration
	{
		[DataMember]
		[JsonProperty("scanings_interval")]
		public int ScaningsInterval { get; set; }

		[DataMember]
		[JsonProperty("send_prices_interval")]
		public int SendPricesInterval { get; set; }

		[DataMember]
		[JsonProperty("price_time_delta")]
		public int PriceTimeDelta { get; set; }
	}
	
	[DataContract]
	public class Session
	{
		[DataMember]
		[JsonProperty("id")]
		public int Id { get; set; }
	}
}