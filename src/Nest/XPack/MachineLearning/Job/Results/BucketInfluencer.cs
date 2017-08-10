using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class BucketInfluencer
	{
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }

		[JsonProperty("influencer_field_name")]
		public string InfluencerFieldName { get; internal set; }

		[JsonProperty("initial_anomaly_score")]
		public double InitialAnomalyScore { get; internal set; }

		[JsonProperty("anomaly_score")]
		public double AnomalyScore { get; internal set; }

		[JsonProperty("raw_anomaly_score")]
		public double RawAnomalyScore { get; internal set; }

		[JsonProperty("probability")]
		public double Probability { get; internal set; }

		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochSecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		[JsonProperty("bucket_span")]
		public long BucketSpan { get; internal set; }

		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }
	}
}