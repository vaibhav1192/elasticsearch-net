using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Stores model information along with the results.
	/// It provides a more detailed view into anomaly detection.
	/// </summary>
	public interface IModelPlot
	{
		/// <summary>
		/// Enables calculation and storage of the model bounds for each entity that is being analyzed.
		/// By default, this is not enabled.
		/// </summary>
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		/// <summary>
		/// Limits data collection to this list of partition or by field values.
		/// If terms are not specified, no filtering is applied.
		/// </summary>
		/// <remarks>
		/// This is experimental. Only the specified terms can be viewed when using the Single Metric Viewer.
		/// </remarks>
		[JsonProperty("terms")]
		Fields Terms { get; set; }
	}

	/// <inheritdoc />
	public class ModelPlot : IModelPlot
	{
		/// <inheritdoc />
		public bool? Enabled { get; set; }
		/// <inheritdoc />
		public Fields Terms { get; set; }
	}

	/// <inheritdoc />
	public class ModelPlotDescriptor<T> : DescriptorBase<ModelPlotDescriptor<T>, IModelPlot>, IModelPlot where T : class
	{
		bool? IModelPlot.Enabled { get; set; }
		Fields IModelPlot.Terms { get; set; }

		/// <inheritdoc />
		public ModelPlotDescriptor<T> Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);

		/// <inheritdoc />
		public ModelPlotDescriptor<T> Terms(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Terms = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc />
		public ModelPlotDescriptor<T> Terms(Fields fields) => Assign(a => a.Terms = fields);
	}
}
