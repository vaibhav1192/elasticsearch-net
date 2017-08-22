using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
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
