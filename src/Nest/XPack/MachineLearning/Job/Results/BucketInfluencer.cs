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

		[JsonProperty("influencer_field_value")]
		public string InfluencerFieldValue { get; internal set; }

		[JsonProperty("initial_influencer_score")]
		public double InitialInfluencerScore { get; internal set; }

		[JsonProperty("influencer_score")]
		public double InfluencerScore { get; internal set; }

		[JsonProperty("raw_anomaly_score")]
		public double RawAnomalyScore { get; internal set; }

		[JsonProperty("probability")]
		public double Probability { get; internal set; }

		[JsonProperty("bucket_span")]
		public long BucketSpan { get; internal set; }

		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }

		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
