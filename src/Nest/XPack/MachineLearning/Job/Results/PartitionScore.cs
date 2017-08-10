using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class PartitionScore
	{
		[JsonProperty("partition_field_name")]
		public string PartitionFieldName { get; internal set; }

		[JsonProperty("partition_field_value")]
		public string PartitionFieldValue { get; internal set; }

		[JsonProperty("initial_record_score")]
		public double InitialRecordScore { get; internal set; }

		[JsonProperty("record_score")]
		public double RecordScore { get; internal set; }

		[JsonProperty("probability")]
		public double Probability { get; internal set; }
	}
}