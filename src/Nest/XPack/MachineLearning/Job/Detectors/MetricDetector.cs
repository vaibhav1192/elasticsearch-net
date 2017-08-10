using System;
using System.Linq.Expressions;

namespace Nest
{
	public class MetricFunctions
	{
		public static string Min = "min";
		public static string Max = "max";
		public static string Median = "median";
		public static string HighMedian = "high_median";
		public static string LowMedian = "low_median";
		public static string Mean = "mean";
		public static string HighMean = "high_mean";
		public static string LowMean = "low_mean";
		public static string Metric = "metric";
		public static string Varp = "varp";
		public static string HighVarp = "high_varp";
		public static string LowVarp = "low_varp";
	}

	public interface IMetricDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector, IFieldNameDetector
	{
	}

	public abstract class MetricDetectorBase : DetectorBase, IGeographicDetector
	{
		protected MetricDetectorBase(string function) : base(function) {}

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
		public Field FieldName { get; set; }
	}

	public class MinDetector : MetricDetectorBase
	{
		public MinDetector() : base(MetricFunctions.Min) {}
	}

	public class MaxDetector : MetricDetectorBase
	{
		public MaxDetector() : base(MetricFunctions.Max) {}
	}

	public class MedianDetector : MetricDetectorBase
	{
		public MedianDetector() : base(MetricFunctions.Median) {}
	}

	public class HighMedianDetector : MetricDetectorBase
	{
		public HighMedianDetector() : base(MetricFunctions.HighMedian) {}
	}

	public class LowMedianDetector : MetricDetectorBase
	{
		public LowMedianDetector() : base(MetricFunctions.LowMedian) {}
	}

	public class MeanDetector : MetricDetectorBase
	{
		public MeanDetector() : base(MetricFunctions.Mean) {}
	}

	public class HighMeanDetector : MetricDetectorBase
	{
		public HighMeanDetector() : base(MetricFunctions.HighMean) {}
	}

	public class LowMeanDetector : MetricDetectorBase
	{
		public LowMeanDetector() : base(MetricFunctions.LowMean) {}
	}

	public class MetricDetector : MetricDetectorBase
	{
		public MetricDetector() : base(MetricFunctions.Metric) {}
	}

	public class VarpDetector : MetricDetectorBase
	{
		public VarpDetector() : base(MetricFunctions.Varp) {}
	}

	public class HighVarpDetector : MetricDetectorBase
	{
		public HighVarpDetector() : base(MetricFunctions.HighVarp) {}
	}

	public class LowVarpDetector : MetricDetectorBase
	{
		public LowVarpDetector() : base(MetricFunctions.LowVarp) {}
	}

	public class MetricDetectorDescriptor<T> : DetectorDescriptorBase<MetricDetectorDescriptor<T>, IMetricDetector>, IMetricDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public MetricDetectorDescriptor(string function) : base(function) {}

		public MetricDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public MetricDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public MetricDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public MetricDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public MetricDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public MetricDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public MetricDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public MetricDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
