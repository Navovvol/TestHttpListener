using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class CarRecordedContext
	{
		[DataMember]
		[JsonProperty("record")]
		public Record? RecordContext { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

	}
	[DataContract]
	public class Record
	{
		[DataMember]
		[JsonProperty("id")]
		public long Id { get; set; }

		[DataMember]
		[JsonProperty("timeStamp")]
		public DateTime TimeStamp { get; set; }

		[DataMember]
		[JsonProperty("plate")]
		public string Plate { get; set; }

		[DataMember]
		[JsonProperty("plateStencil")]
		public string PlateStencil { get; set; }

		[DataMember]
		[JsonProperty("status")]
		public string Status { get; set; }

		[DataMember]
		[JsonProperty("direction")]
		public string Direction { get; set; }

		[DataMember]
		[JsonProperty("passage")]
		public string Passage { get; set; }

		[DataMember]
		[JsonProperty("videoChannel")]
		public int VideoChannel { get; set; }

		[DataMember]
		[JsonProperty("videoChannelName")]
		public string VideoChannelName { get; set; }

		[DataMember]
		[JsonProperty("videoChannelId")]
		public long VideoChannelId { get; set; }

		[DataMember]
		[JsonProperty("vehicleDatabaseName")]
		public string VehicleDatabaseName { get; set; }

		[DataMember]
		[JsonProperty("serverId")]
		public long? ServerId { get; set; }

		[DataMember]
		[JsonProperty("serverGuid")]
		public Guid? ServerGuid { get; set; }

		[DataMember]
		[JsonProperty("serverName")]
		public string ServerName { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
