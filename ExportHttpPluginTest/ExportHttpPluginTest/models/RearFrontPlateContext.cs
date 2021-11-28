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
	public class RearFrontPlateContext
	{
		[DataMember]
		[JsonProperty("record")]
		public Record RecordContext { get; set; }
		[DataMember]
		[JsonProperty("rearPlate")]
		public string RearPlate { get; set; }
		[DataMember]
		[JsonProperty("checkpointName")]
		public string CheckpointName { get; set; }
		[DataMember]
		[JsonProperty("checkpointGuid")]
		public string CheckpointGuid { get; set; }
		[DataMember]
		[JsonProperty("relatedRecordId")]
		public long RelatedRecordId { get; set; }
		[DataMember]
		[JsonProperty("platesIsEqual")]
		public bool PlatesIsEqual { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
