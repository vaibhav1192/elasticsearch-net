using System.Collections.Generic;
using Newtonsoft.Json;

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
		public long Count { get; internal set; }

		public IReadOnlyCollection<MachineLearningFilter> Filters { get; internal set; } = EmptyReadOnly<MachineLearningFilter>.Collection;
	}
}
