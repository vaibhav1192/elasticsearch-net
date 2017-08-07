using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IModelPlot
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("terms")]
		Fields Terms { get; set; }
	}

	public class ModelPlot : IModelPlot
	{
		public bool? Enabled { get; set; }
		public Fields Terms { get; set; }
	}

	public class ModelPlotDescriptor<T> : DescriptorBase<ModelPlotDescriptor<T>, IModelPlot>, IModelPlot where T : class
	{
		bool? IModelPlot.Enabled { get; set; }
		Fields IModelPlot.Terms { get; set; }

		public ModelPlotDescriptor<T> Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);

		public ModelPlotDescriptor<T> Terms(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Terms = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public ModelPlotDescriptor<T> Terms(Fields fields) => Assign(a => a.Terms = fields);
	}
}
