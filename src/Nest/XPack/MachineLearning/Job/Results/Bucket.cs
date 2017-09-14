using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class Bucket
	{
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		[JsonProperty("anomaly_score")]
		public double AnomalyScore { get; internal set; }

		[JsonProperty("bucket_span")]
		public Time BucketSpan { get; internal set; }

		[JsonProperty("initial_anomaly_score")]
		public double InitialAnomalyScore { get; internal set; }

		[JsonProperty("event_count")]
		public long EventCount { get; internal set; }

		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }

		[JsonProperty("bucket_influencers")]
		public IReadOnlyCollection<BucketInfluencer> BucketInfluencers { get; internal set; } = EmptyReadOnly<BucketInfluencer>.Collection;

		[JsonProperty("processing_time_ms")]
		public double ProcessingTimeMs { get; internal set; }

		[JsonProperty("partition_scores")]
		public IReadOnlyCollection<PartitionScore> PartitionScores { get; internal set; } = EmptyReadOnly<PartitionScore>.Collection;

		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }
	}
}
