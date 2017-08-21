using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IGetFiltersResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("filters")]
		IReadOnlyCollection<MachineLearningFilter> Filters { get; }
	}

	public class GetFiltersResponse : ResponseBase, IGetFiltersResponse
	{
		public long Count { get; }

		public IReadOnlyCollection<MachineLearningFilter> Filters { get; internal set; } = EmptyReadOnly<MachineLearningFilter>.Collection;
	}

	[JsonObject]
	public class MachineLearningFilter
	{
		// TODO: Looks like type is only included when INCLUDE_TYPE_KEY set. Determine if this needs to be exposed
		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("filter_id")]
		public string Id { get; internal set; }

		[JsonProperty("items")]
		public IReadOnlyCollection<string> Items { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
