using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPostJobDataResponse : IResponse
	{
		/// <summary>
		/// The unique identifier for the job
		/// </summary>
		[JsonProperty("job_id")]
		string JobId { get; }

		[JsonProperty("processed_record_count")]
		long ProcessedRecordCount { get; }

		[JsonProperty("processed_field_count")]
		long ProcessedFieldCount { get; }

		[JsonProperty("input_bytes")]
		long InputBytes { get; }

		[JsonProperty("input_field_count")]
		long InputFieldCount { get; }

		[JsonProperty("invalid_date_count")]
		long InvalidDateCount { get; }

		[JsonProperty("missing_field_count")]
		long MissingFieldCount { get; }

		[JsonProperty("out_of_order_timestamp_count")]
		long OutOfOrderTimestampCount { get; }

		[JsonProperty("empty_bucket_count")]
		long EmptyBucketCount { get; }

		[JsonProperty("sparse_bucket_count")]
		long SparseBucketCount { get; }

		[JsonProperty("bucket_count")]
		long BucketCount { get; }

		[JsonProperty("last_data_time")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset LastDataTime { get; }

		[JsonProperty("input_record_count")]
		long InputRecordCount { get; }
	}

	/// <inheritdoc />
	public class PostJobDataResponse : ResponseBase, IPostJobDataResponse
	{
		/// <inheritdoc />
		public string JobId { get; internal set; }

		/// <inheritdoc />
		public long ProcessedRecordCount { get; internal set; }

		/// <inheritdoc />
		public long ProcessedFieldCount { get; internal set; }

		/// <inheritdoc />
		public long InputBytes { get; internal set; }

		/// <inheritdoc />
		public long InputFieldCount { get; internal set; }

		/// <inheritdoc />
		public long InvalidDateCount { get; internal set; }

		/// <inheritdoc />
		public long MissingFieldCount { get; internal set; }

		/// <inheritdoc />
		public long OutOfOrderTimestampCount { get; internal set; }

		/// <inheritdoc />
		public long EmptyBucketCount { get; internal set; }

		/// <inheritdoc />
		public long SparseBucketCount { get; internal set; }

		/// <inheritdoc />
		public long BucketCount { get; internal set; }

		/// <inheritdoc />
		public DateTimeOffset LastDataTime { get; internal set; }

		/// <inheritdoc />
		public long InputRecordCount { get; internal set; }
	}
}
