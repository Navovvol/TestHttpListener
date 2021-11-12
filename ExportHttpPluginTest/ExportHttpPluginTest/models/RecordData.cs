using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestHttpListener
{
	public class CarRecognizedContext
	{
		[DataMember]
		[JsonProperty("recordData")]
		public RecordData? RecordDataContext { get; set; }

		[DataMember]
		[JsonProperty("recognizedVehicleType")]
		public string RecognizedVehicleType { get; set; }

		[DataMember]
		[JsonProperty("recognizedVehicleSide")]
		public string RecognizedVehicleSide { get; set; }
		[DataMember]
		[JsonProperty("decisionSource")]
		public string DecisionSource { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
	public class RecordData
	{
		[DataMember]
		[JsonProperty("bestFrame")]
		public DateTime BestFrame { get; set; }
		[DataMember]
		[JsonProperty("firstFrame")]
		public DateTime FirstFrame { get; set; }
		[DataMember]
		[JsonProperty("lastFrame")]
		public DateTime LastFrame { get; set; }

		[DataMember]
		[JsonProperty("plate")]
		public string Plate { get; set; }

		[DataMember]
		[JsonProperty("estimate")]
		public double Estimate { get; set; }

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
		[JsonProperty("videoChannelOrderId")]
		public int VideoChannelOrderId { get; set; }

		[DataMember]
		[JsonProperty("videoChannelName")]
		public string VideoChannelName { get; set; }

		[DataMember]
		[JsonProperty("speed")]
		public float? Speed { get; set; }
		[DataMember]
		[JsonProperty("flags")]
		public string Flags { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
