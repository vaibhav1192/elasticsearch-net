using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class SumFunctions
	{
		public static string Sum = "sum";
		public static string HighSum = "high_sum";
		public static string LowSum = "low_sum";
		public static string NonNullSum = "non_null_sum";
		public static string HighNonNullSum = "high_non_null_sum";
		public static string LowNonNullSum = "low_non_null_sum";
	}

	public interface ISumDetector : IDetector, IFieldNameDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public interface INonNullSumDetector : IDetector, IFieldNameDetector, IByFieldNameDetector, IPartitionFieldNameDetector
	{
	}

	public abstract class SumDetectorBase : DetectorBase, ISumDetector
	{
		public Field FieldName { get; set; }
		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected SumDetectorBase(string function) : base(function)
		{
		}
	}

	public abstract class NonNullSumDetectorBase : DetectorBase, INonNullSumDetector
	{
		public Field FieldName { get; set; }
		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected NonNullSumDetectorBase(string function) : base(function)
		{
		}
	}

	public class SumDetector : SumDetectorBase
	{
		public SumDetector() : base(SumFunctions.Sum) {}
	}

	public class HighSumDetector : SumDetectorBase
	{
		public HighSumDetector() : base(SumFunctions.HighSum) {}
	}

	public class LowSumDetector : SumDetectorBase
	{
		public LowSumDetector() : base(SumFunctions.LowSum) {}
	}

	public class NonNullSumDetector : NonNullSumDetectorBase
	{
		public NonNullSumDetector() : base(SumFunctions.NonNullSum) {}
	}

	public class HighNonNullSumDetector : NonNullSumDetectorBase
	{
		public HighNonNullSumDetector() : base(SumFunctions.HighNonNullSum) {}
	}

	public class LowNonNullSumDetector : NonNullSumDetectorBase
	{
		public LowNonNullSumDetector() : base(SumFunctions.LowNonNullSum) {}
	}

	public class SumDetectorDescriptor<T> : DetectorDescriptorBase<SumDetectorDescriptor<T>, ISumDetector>, ISumDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public SumDetectorDescriptor(string function) : base(function) {}

		public SumDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public SumDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public SumDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public SumDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public SumDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public SumDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public SumDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public SumDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class NonNullSumDetectorDescriptor<T> : DetectorDescriptorBase<NonNullSumDetectorDescriptor<T>, INonNullSumDetector>, INonNullSumDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public NonNullSumDetectorDescriptor(string function) : base(function) {}

		public NonNullSumDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public NonNullSumDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public NonNullSumDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public NonNullSumDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public NonNullSumDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public NonNullSumDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
