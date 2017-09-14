using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A request to retrieve job results for one or more buckets.
	/// </summary>
	public partial interface IGetBucketsRequest
	{
		/// <summary>
		/// Returns buckets with anomaly scores higher than this value.
		/// </summary>
		[JsonProperty("anomaly_score")]
		double? AnomalyScore { get; set; }

		/// <summary>
		/// If true, the buckets are sorted in descending order.
		/// </summary>
		[JsonProperty("desc")]
		bool? Desc { get; set; }

		/// <summary>
		/// Returns buckets with timestamps earlier than this time.
		/// </summary>
		[JsonProperty("end")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// If true, the output excludes interim results. By default, interim results are included.
		/// </summary>
		[JsonProperty("exclude_interim")]
		bool? ExcludeInterim { get; set; }

		/// <summary>
		/// If true, the output includes anomaly records.
		/// </summary>
		[JsonProperty("expand")]
		bool? Expand { get; set; }

		/// <summary>
		/// Specifies pagination for the buckets
		/// </summary>
		[JsonProperty("page")]
		IPage Page { get; set; }

		/// <summary>
		/// Specifies the sort field for the requested buckets. By default, the buckets are sorted by the timestamp field.
		/// </summary>
		[JsonProperty("sort")]
		Field Sort { get; set; }

		/// <summary>
		/// Returns buckets with timestamps after this time.
		/// </summary>
		[JsonProperty("start")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? Start { get; set; }

		/// <summary>
		/// Returns buckets with matching timestamps.
		/// </summary>
		[JsonProperty("timestamp")]
		//[JsonConverter(typeof(EpochStringMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? Timestamp { get; set; }
	}

	/// <inheritdoc />
	public partial class GetBucketsRequest
	{
		/// <inheritdoc />
		public double? AnomalyScore { get; set; }
		/// <inheritdoc />
		public bool? Desc { get; set; }
		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }
		/// <inheritdoc />
		public bool? ExcludeInterim { get; set; }
		/// <inheritdoc />
		public bool? Expand { get; set; }
		/// <inheritdoc />
		public IPage Page { get; set; }
		/// <inheritdoc />
		public Field Sort { get; set; }
		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }
		/// <inheritdoc />
		public DateTimeOffset? Timestamp { get; set; }
	}

	public interface IPage
	{
		/// <summary>
		/// Skips the specified number of buckets.
		/// </summary>
		[JsonProperty("from")]
		int? From { get; set; }

		/// <summary>
		/// Specifies the maximum number of buckets to obtain.
		/// </summary>
		[JsonProperty("size")]
		int? Size { get; set; }
	}

	public class PageDescriptor : DescriptorBase<PageDescriptor, IPage>, IPage
	{
		int? IPage.From { get; set; }
		int? IPage.Size { get; set; }

		public PageDescriptor From(int from) => Assign(a => a.From = from);

		public PageDescriptor Size(int size) => Assign(a => a.Size = size);
	}

	public class Page : IPage
	{
		public int? From { get; set; }

		public int? Size { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetBuckets")]
	public partial class GetBucketsDescriptor
	{
		double? IGetBucketsRequest.AnomalyScore { get; set; }
		bool? IGetBucketsRequest.Desc { get; set; }
		DateTimeOffset? IGetBucketsRequest.End { get; set; }
		bool? IGetBucketsRequest.ExcludeInterim { get; set; }
		bool? IGetBucketsRequest.Expand { get; set; }
		IPage IGetBucketsRequest.Page { get; set; }
		Field IGetBucketsRequest.Sort { get; set; }
		DateTimeOffset? IGetBucketsRequest.Start { get; set; }
		DateTimeOffset? IGetBucketsRequest.Timestamp { get; set; }

		/// <inheritdoc />
		public GetBucketsDescriptor AnomalyScore(double anomalyScore) => Assign(a => a.AnomalyScore = anomalyScore);

		/// <inheritdoc />
		public GetBucketsDescriptor Desc(bool desc = true) => Assign(a => a.Desc = desc);

		/// <inheritdoc />
		public GetBucketsDescriptor End(DateTimeOffset end) => Assign(a => a.End = end);

		/// <inheritdoc />
		public GetBucketsDescriptor ExcludeInterim(bool excludeInterim = true) => Assign(a => a.ExcludeInterim = excludeInterim);

		/// <inheritdoc />
		public GetBucketsDescriptor Expand(bool expand = true) => Assign(a => a.Expand = expand);

		/// <inheritdoc />
		public GetBucketsDescriptor Page(Func<PageDescriptor, IPage> selector) =>
			Assign(a => a.Page = selector?.Invoke(new PageDescriptor()));

		/// <inheritdoc />
		public GetBucketsDescriptor Sort(Field field) => Assign(a => a.Sort = field);

		/// <inheritdoc />
		public GetBucketsDescriptor Start(DateTimeOffset start) => Assign(a => a.Start = start);

		/// <inheritdoc />
		public GetBucketsDescriptor Timestamp(DateTimeOffset timestamp) => Assign(a => a.Timestamp = timestamp);
	}
}
