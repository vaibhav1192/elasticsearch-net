using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analysis configuration for a Machine Learning job.
	/// </summary>
	public interface IAnalysisConfig
	{
		[JsonProperty("bucket_span")]
		Time BucketSpan { get; set; }

		[JsonProperty("categorization_field_name")]
		Field CategorizationFieldName { get; set; }

		[JsonProperty("categorization_filters")]
		IEnumerable<string> CategorizationFilters { get; set; }

		[JsonProperty("detectors")]
		IEnumerable<IDetector> Detectors { get; set; }

		[JsonProperty("influencers")]
		Fields Influencers { get; set; }

		[JsonProperty("latency")]
		Time Latency { get; set; }

		[JsonProperty("multivariate_by_fields")]
		bool? MultivariateByFields { get; set; }

		[JsonProperty("summary_count_field_name")]
		Field SummaryCountFieldName { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisConfig : IAnalysisConfig
	{
		/// <inheritdoc />
		public Time BucketSpan { get; set; }
		/// <inheritdoc />
		public Field CategorizationFieldName { get; set; }
		/// <inheritdoc />
		public IEnumerable<string> CategorizationFilters { get; set; }
		/// <inheritdoc />
		public IEnumerable<IDetector> Detectors { get; set; }
		/// <inheritdoc />
		public Fields Influencers { get; set; }
		/// <inheritdoc />
		public Time Latency { get; set; }
		/// <inheritdoc />
		public bool? MultivariateByFields { get; set; }
		/// <inheritdoc />
		public Field SummaryCountFieldName { get; set; }
	}

	public class AnalysisConfigDescriptor<T> : DescriptorBase<AnalysisConfigDescriptor<T>, IAnalysisConfig>, IAnalysisConfig where T : class
	{
		Time IAnalysisConfig.BucketSpan { get; set; }
		Field IAnalysisConfig.CategorizationFieldName { get; set; }
		IEnumerable<string> IAnalysisConfig.CategorizationFilters { get; set; }
		IEnumerable<IDetector> IAnalysisConfig.Detectors { get; set; }
		Fields IAnalysisConfig.Influencers { get; set; }
		Time IAnalysisConfig.Latency { get; set; }
		bool? IAnalysisConfig.MultivariateByFields { get; set; }
		Field IAnalysisConfig.SummaryCountFieldName { get; set; }

		public AnalysisConfigDescriptor<T> BucketSpan(Time bucketSpan) => Assign(a => a.BucketSpan = bucketSpan);

		public AnalysisConfigDescriptor<T> CategorizationFieldName(Field field) => Assign(a => a.CategorizationFieldName = field);

		public AnalysisConfigDescriptor<T> CategorizationFieldName(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.CategorizationFieldName = objectPath);

		public AnalysisConfigDescriptor<T> CategorizationFilters(IEnumerable<string> filters) => Assign(a => a.CategorizationFilters = filters);

		public AnalysisConfigDescriptor<T> CategorizationFilters(params string[] filters) => Assign(a => a.CategorizationFilters = filters);

		public AnalysisConfigDescriptor<T> Detectors(Func<DetectorsDescriptor, IPromise<IEnumerable<IDetector>>> selector) =>
			Assign(a => a.Detectors = selector.InvokeOrDefault(new DetectorsDescriptor()).Value);
	}
}
