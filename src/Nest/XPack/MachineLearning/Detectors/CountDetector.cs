using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class CountFunctions
	{
		public static string Count = "count";
		public static string HighCount = "high_count";
		public static string LowCount = "low_count";
		public static string NonZeroCount = "non_zero_count";
		public static string LowNonZeroCount = "low_non_zero_count";
		public static string HighNonZeroCount = "high_non_zero_count";
		public static string DistinctCount = "distinct_count";
		public static string LowDistinctCount = "low_distinct_count";
		public static string HighDistinctCount = "high_distinct_count";
	}

	public interface ICountDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public interface INonZeroCountDetector : IDetector, IByFieldNameDetector, IPartitionFieldNameDetector
	{
	}

	public interface IDistinctCountDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector, IFieldNameDetector
	{
	}

	public abstract class CountDetectorBase : DetectorBase, ICountDetector
	{
		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected CountDetectorBase(string function) : base(function)
		{
		}
	}

	public abstract class NonZeroCountDetectorBase : DetectorBase, INonZeroCountDetector
	{
		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected NonZeroCountDetectorBase(string function) : base(function)
		{
		}
	}

	public abstract class DistinctCountDetectorBase : DetectorBase, IDistinctCountDetector
	{
		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field FieldName { get; set; }

		protected DistinctCountDetectorBase(string function) : base(function)
		{
		}
	}

	public class CountDetectorDescriptor<T> : DetectorDescriptorBase<CountDetectorDescriptor<T>, ICountDetector>, ICountDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public CountDetectorDescriptor(string function) : base(function) {}

		public CountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public CountDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public CountDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public CountDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public CountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public CountDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class NonZeroCountDetectorDescriptor<T> : DetectorDescriptorBase<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector>, INonZeroCountDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public NonZeroCountDetectorDescriptor(string function) : base(function) {}

		public NonZeroCountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public NonZeroCountDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public NonZeroCountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public NonZeroCountDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class DistinctCountDetectorDescriptor<T> : DetectorDescriptorBase<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector>, IDistinctCountDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public DistinctCountDetectorDescriptor(string function) : base(function) {}

		public DistinctCountDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public DistinctCountDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public DistinctCountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public DistinctCountDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public DistinctCountDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public DistinctCountDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public DistinctCountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public DistinctCountDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class CountDetector : CountDetectorBase
	{
		public CountDetector() : base(CountFunctions.Count) {}
	}

	public class HighCountDetector : CountDetectorBase
	{
		public HighCountDetector() : base(CountFunctions.HighCount) {}
	}

	public class LowCountDetector : CountDetectorBase
	{
		public LowCountDetector() : base(CountFunctions.LowCount) {}
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is anomalous, but it ignores cases where the bucket count is zero.
	/// Use this function if you know your data is sparse or has gaps and the gaps are not important.
	/// </summary>
	public class NonZeroCountDetector : NonZeroCountDetectorBase
	{
		public NonZeroCountDetector() : base(CountFunctions.NonZeroCount) {}
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is unusually high and it ignores cases where the bucket count is zero.
	/// </summary>
	public class HighNonZeroCountDetector : NonZeroCountDetectorBase
	{
		public HighNonZeroCountDetector() : base(CountFunctions.HighNonZeroCount) {}
	}

	public class DistinctCountDetector : DistinctCountDetectorBase
	{
		public DistinctCountDetector() : base(CountFunctions.DistinctCount) {}
	}

	public class HighDistinctCountDetector : DistinctCountDetectorBase
	{
		public HighDistinctCountDetector() : base(CountFunctions.HighDistinctCount) {}
	}

	public class LowDistinctCountDetector : DistinctCountDetectorBase
	{
		public LowDistinctCountDetector() : base(CountFunctions.LowDistinctCount) {}
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is unusually low and it ignores cases where the bucket count is zero.
	/// </summary>
	public class LowNonZeroCountDetector : NonZeroCountDetectorBase
	{
		public LowNonZeroCountDetector() : base(CountFunctions.LowNonZeroCount) {}
	}
}
