using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IStartDatafeedRequest
	{
		[JsonProperty("timeout")]
		Time Timeout { get; set; }

		[JsonProperty("start")]
		[JsonConverter(typeof(EpochSecondsDateTimeJsonConverter))]
		DateTimeOffset? Start { get; set; }

		[JsonProperty("end")]
		[JsonConverter(typeof(EpochSecondsDateTimeJsonConverter))]
		DateTimeOffset? End { get; set; }
	}

	/// <inheritdoc />
	public partial class StartDatafeedRequest
	{
		/// <inheritdoc />
		public Time Timeout { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlStartDatafeed")]
	public partial class StartDatafeedDescriptor
	{
		Time IStartDatafeedRequest.Timeout { get; set; }
		DateTimeOffset? IStartDatafeedRequest.Start { get; set; }
		DateTimeOffset? IStartDatafeedRequest.End { get; set; }

		/// <inheritdoc />
		public StartDatafeedDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);

		/// <inheritdoc />
		public StartDatafeedDescriptor Start(DateTimeOffset start) => Assign(a => a.Start = start);

		/// <inheritdoc />
		public StartDatafeedDescriptor End(DateTimeOffset end) => Assign(a => a.End = end);
	}
}
