using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalysisLimits
	{
		[JsonProperty("categorization_examples_limit")]
		long? CategorizationExamplesLimit { get; set; }

		[JsonProperty("model_memory_limit")]
		long? ModelMemoryLimit { get; set; }
	}

	public class AnalysisLimits : IAnalysisLimits
	{
		public long? CategorizationExamplesLimit { get; set; }
		public long? ModelMemoryLimit { get; set; }
	}

	public class AnalysisLimitsDescriptor : DescriptorBase<AnalysisLimitsDescriptor, IAnalysisLimits>, IAnalysisLimits
	{
		long? IAnalysisLimits.CategorizationExamplesLimit { get; set; }
		long? IAnalysisLimits.ModelMemoryLimit { get; set; }

		public AnalysisLimitsDescriptor CategorizationExamplesLimit(long categorizationExamplesLimit) =>
			Assign(a => a.CategorizationExamplesLimit = categorizationExamplesLimit);

		public AnalysisLimitsDescriptor ModelMemoryLimit(long modelMemoryLimit) =>
			Assign(a => a.ModelMemoryLimit = modelMemoryLimit);
	}
}
