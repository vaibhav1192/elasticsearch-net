using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CategoryDefinition
	{
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("category_id")]
		public long CategoryId { get; internal set; }

		[JsonProperty("terms")]
		public string Terms { get; internal set; }

		[JsonProperty("regex")]
		public string Regex { get; internal set; }

		[JsonProperty("max_matching_length")]
		public long MaxMatchingLength { get; internal set; }

		[JsonProperty("examples")]
		public IReadOnlyCollection<string> Examples { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}