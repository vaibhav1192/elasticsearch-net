using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDetector
	{
		[JsonProperty("detector_description")]
		string DetectorDescription { get; set; }

		[JsonProperty("exclude_frequent")]
		ExcludeFrequent? ExcludeFrequent { get; set; }

		[JsonProperty("function")]
		string Function { get; }

		[JsonProperty("use_null")]
		bool? UseNull { get; set; }

		[JsonProperty("detector_index")]
		int? DetectorIndex { get; set; }
	}

	public interface IFieldNameDetector : IDetector
	{
		[JsonProperty("field_name")]
		Field FieldName { get; set; }
	}

	public interface IByFieldNameDetector : IDetector
	{
		[JsonProperty("by_field_name")]
		Field ByFieldName { get; set; }
	}

	public interface IOverFieldNameDetector : IDetector
	{
		[JsonProperty("over_field_name")]
		Field OverFieldName { get; set; }
	}

	public interface IPartitionFieldNameDetector : IDetector
	{

		[JsonProperty("partition_field_name")]
		Field PartitionFieldName { get; set; }
	}

	public abstract class DetectorBase : IDetector
	{
		protected DetectorBase(string function)
		{
			Function = function;
		}

		public string DetectorDescription { get; set; }
		public ExcludeFrequent? ExcludeFrequent { get; set; }
		public string Function { get; }
		public bool? UseNull { get; set; }
		public int? DetectorIndex { get; set; }
	}

	public abstract class DetectorDescriptorBase<TDetectorDescriptor, TDetectorInterface> : DescriptorBase<TDetectorDescriptor, TDetectorInterface>, IDetector
		where TDetectorDescriptor : DetectorDescriptorBase<TDetectorDescriptor, TDetectorInterface>, TDetectorInterface
		where TDetectorInterface : class, IDetector
	{
		private readonly string _function;

		string IDetector.DetectorDescription { get; set; }
		ExcludeFrequent? IDetector.ExcludeFrequent { get; set; }
		string IDetector.Function => _function;
		bool? IDetector.UseNull { get; set; }
		int? IDetector.DetectorIndex { get; set; }

		protected DetectorDescriptorBase(string function) => _function = function;

		public TDetectorDescriptor DetectorDescription(string description) =>
			Assign(a => a.DetectorDescription = description);

		public TDetectorDescriptor ExcludeFrequent(ExcludeFrequent excludeFrequent) =>
			Assign(a => a.ExcludeFrequent = excludeFrequent);

		public TDetectorDescriptor UseNull(bool useNull = true) =>
			Assign(a => a.UseNull = useNull);

		public TDetectorDescriptor DetectorIndex(int detectorIndex) =>
			Assign(a => a.DetectorIndex = detectorIndex);
	}

	public class DetectorsDescriptor : DescriptorPromiseBase<DetectorsDescriptor, IList<IDetector>>
	{
		public DetectorsDescriptor() : base(new List<IDetector>()) {}

		public DetectorsDescriptor Count<T>(Func<CountDetectorDescriptor<T>, ICountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new CountDetectorDescriptor<T>(CountFunctions.Count))));

		public DetectorsDescriptor HighCount<T>(Func<CountDetectorDescriptor<T>, ICountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new CountDetectorDescriptor<T>(CountFunctions.HighCount))));

		public DetectorsDescriptor LowCount<T>(Func<CountDetectorDescriptor<T>, ICountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new CountDetectorDescriptor<T>(CountFunctions.LowCount))));

		public DetectorsDescriptor NonZeroCount<T>(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new NonZeroCountDetectorDescriptor<T>(CountFunctions.NonZeroCount))));

		public DetectorsDescriptor HighNonZeroCount<T>(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new NonZeroCountDetectorDescriptor<T>(CountFunctions.HighNonZeroCount))));

		public DetectorsDescriptor LowNonZeroCount<T>(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new NonZeroCountDetectorDescriptor<T>(CountFunctions.LowNonZeroCount))));

		public DetectorsDescriptor DistinctCount<T>(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new DistinctCountDetectorDescriptor<T>(CountFunctions.DistinctCount))));

		public DetectorsDescriptor HighDistinctCount<T>(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new DistinctCountDetectorDescriptor<T>(CountFunctions.HighDistinctCount))));

		public DetectorsDescriptor LowDistinctCount<T>(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector) where T : class =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new DistinctCountDetectorDescriptor<T>(CountFunctions.LowDistinctCount))));
	}
}
