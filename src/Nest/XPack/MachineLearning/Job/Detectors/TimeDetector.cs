using System;
using System.Linq.Expressions;

namespace Nest
{
	public class TimeFunctions
	{
		public static string TimeOfDay = "time_of_day";
		public static string TimeOfWeek = "time_of_day";
	}

	public interface ITimeDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public abstract class TimeDetectorBase : DetectorBase, ITimeDetector
	{
		protected TimeDetectorBase(string function) : base(function) {}

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class TimeOfDayDetector : TimeDetectorBase
	{
		public TimeOfDayDetector() : base(TimeFunctions.TimeOfDay) {}
	}

	public class TimeOfWeekDetector : TimeDetectorBase
	{
		public TimeOfWeekDetector() : base(TimeFunctions.TimeOfWeek) {}
	}

	public class TimeDetectorDescriptor<T> : DetectorDescriptorBase<TimeDetectorDescriptor<T>, ITimeDetector>, ITimeDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public TimeDetectorDescriptor(string function) : base(function) {}

		public TimeDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public TimeDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public TimeDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public TimeDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public TimeDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public TimeDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
