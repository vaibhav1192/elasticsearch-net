using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class RareFunctions
	{
		public static string Rare = "rare";
		public static string FreqRare = "freq_rare";
	}

	public interface IRareDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public abstract class RareDetectorBase : DetectorBase, IRareDetector
	{
		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected RareDetectorBase(string function) : base(function)
		{
		}
	}

	public class RareDetectorDescriptor<T> : DetectorDescriptorBase<RareDetectorDescriptor<T>, IRareDetector>, IRareDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public RareDetectorDescriptor(string function) : base(function) {}

		public RareDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public RareDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public RareDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public RareDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public RareDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public RareDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class RareDetector : RareDetectorBase
	{
		public RareDetector() : base(RareFunctions.Rare) {}
	}

	public class FreqRareDetector : RareDetectorBase
	{
		public FreqRareDetector() : base(RareFunctions.FreqRare) {}
	}
}
