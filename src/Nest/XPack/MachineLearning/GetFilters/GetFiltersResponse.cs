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
}
