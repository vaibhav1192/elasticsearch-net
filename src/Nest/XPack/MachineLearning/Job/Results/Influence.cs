using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Influence
	{
		[JsonProperty("influencer_field_name")]
		public string InfluencerFieldName { get; internal set; }

		[JsonProperty("influencer_field_values")]
		public IReadOnlyCollection<string> InfluencerFieldValues { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}