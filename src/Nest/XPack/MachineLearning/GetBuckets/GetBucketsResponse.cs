using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetBucketsResponse : IResponse
	{
		[JsonProperty("count")]
		int Count { get; }

		[JsonProperty("buckets")]
		IReadOnlyCollection<Bucket> Buckets { get; }
	}

	public class Bucket
	{
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochSecondsDateTimeJsonConverter))]
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
		public IReadOnlyCollection<BucketInfluencer> BucketInfluencers { get; internal set; } =
			EmptyReadOnly<BucketInfluencer>.Collection;

		[JsonProperty("processing_time_ms")]
		public double ProcessingTimeMs { get; internal set; }

		[JsonProperty("partition_scores")]
		public IReadOnlyCollection<PartitionScore> PartitionScores { get; internal set; } =
			EmptyReadOnly<PartitionScore>.Collection;

		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }
	}

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

	public class GetBucketsResponse : ResponseBase, IGetBucketsResponse
	{
		public int Count { get; internal set; }

		public IReadOnlyCollection<Bucket> Buckets { get; internal set; } =
			EmptyReadOnly<Bucket>.Collection;
	}
}
