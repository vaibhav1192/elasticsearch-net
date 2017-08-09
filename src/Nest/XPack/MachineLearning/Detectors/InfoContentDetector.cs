using System;
using System.Linq.Expressions;

namespace Nest
{
	public class InfoContentFunctions
	{
		public static string InfoContent = "info_content";
		public static string HighInfoContent = "high_info_content";
		public static string LowInfoContent = "low_info_content";
	}

	public interface IInfoContentDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector, IFieldNameDetector
	{
	}

	public abstract class InfoContentDetectorBase : DetectorBase, IInfoContentDetector
	{
		protected InfoContentDetectorBase(string function) : base(function) {}

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
		public Field FieldName { get; set; }
	}

	public class InfoContentDetector : InfoContentDetectorBase
	{
		public InfoContentDetector() : base(InfoContentFunctions.InfoContent) {}
	}

	public class HighInfoContentDetector : InfoContentDetectorBase
	{
		public HighInfoContentDetector() : base(InfoContentFunctions.HighInfoContent) {}
	}

	public class LowInfoContentDetector : InfoContentDetectorBase
	{
		public LowInfoContentDetector() : base(InfoContentFunctions.LowInfoContent) {}
	}

	public class InfoContentDetectorDescriptor<T> : DetectorDescriptorBase<InfoContentDetectorDescriptor<T>, IInfoContentDetector>, IInfoContentDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public InfoContentDetectorDescriptor(string function) : base(function) {}

		public InfoContentDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public InfoContentDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public InfoContentDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public InfoContentDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public InfoContentDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public InfoContentDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public InfoContentDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public InfoContentDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
