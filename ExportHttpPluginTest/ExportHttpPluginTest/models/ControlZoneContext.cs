using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ExportHttpPluginTest
{
	[DataContract]
	public class ControlZoneContext
	{
		[DataMember]
		[JsonProperty("record")]
		public Record RecordContext { get; set; }

		/// <summary>Gets the assigned zone identifier.</summary>
		/// <value>The assigned zone identifier.</value>
		[DataMember]
		[JsonProperty("assignedZoneId")]
		public string AssignedZoneId { get; set; }

		/// <summary>Gets the control zone identifier.</summary>
		/// <value>The control zone identifier.</value>
		[DataMember]
		[JsonProperty("controlZoneId")]
		public long ControlZoneId { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
