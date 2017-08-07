using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDataDescription
	{
		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("time_field")]
		Field TimeField { get; set; }

		[JsonProperty("time_format")]
		string TimeFormat { get; set; }
	}

	public class DataDescription : IDataDescription
	{
		public string Format { get; set; }
		public Field TimeField { get; set; }
		public string TimeFormat { get; set; }
	}

	public class DataDescriptionDescriptor<T> : DescriptorBase<DataDescriptionDescriptor<T>, IDataDescription>, IDataDescription
	{
		string IDataDescription.Format { get; set; }
		Field IDataDescription.TimeField { get; set; }
		string IDataDescription.TimeFormat { get; set; }

		public DataDescriptionDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DataDescriptionDescriptor<T> TimeField(Field timeField) => Assign(a => a.TimeField = timeField);

		public DataDescriptionDescriptor<T> TimeField(Expression<Func<T, object>> objectPath) => Assign(a => a.TimeField = objectPath);

		public DataDescriptionDescriptor<T> TimeFormat(string timeTormat) => Assign(a => a.TimeFormat = timeTormat);
	}
}
