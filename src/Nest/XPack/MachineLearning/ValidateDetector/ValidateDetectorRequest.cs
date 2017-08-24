using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ValidateDetectorRequestConverter))]
	public partial interface IValidateDetectorRequest
	{
		[JsonIgnore]
		IDetector Detector { get; set; }
	}

	/// <inheritdoc />
	public partial class ValidateDetectorRequest
	{
		/// <inheritdoc />
		public IDetector Detector { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlValidateDetector")]
	public partial class ValidateDetectorDescriptor<T> where T : class
	{
		IDetector IValidateDetectorRequest.Detector { get; set; }

		public ValidateDetectorDescriptor<T> Count(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunctions.Count)));

		public ValidateDetectorDescriptor<T> HighCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunctions.HighCount)));

		public ValidateDetectorDescriptor<T> LowCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunctions.LowCount)));

		public ValidateDetectorDescriptor<T> NonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(CountFunctions.NonZeroCount)));

		public ValidateDetectorDescriptor<T> HighNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(CountFunctions.HighNonZeroCount)));

		public ValidateDetectorDescriptor<T> LowNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(CountFunctions.LowNonZeroCount)));

		public ValidateDetectorDescriptor<T> DistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(CountFunctions.DistinctCount)));

		public ValidateDetectorDescriptor<T> HighDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(CountFunctions.HighDistinctCount)));

		public ValidateDetectorDescriptor<T> LowDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(CountFunctions.LowDistinctCount)));

		public ValidateDetectorDescriptor<T> InfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunctions.InfoContent)));

		public ValidateDetectorDescriptor<T> HighInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunctions.HighInfoContent)));

		public ValidateDetectorDescriptor<T> LowInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunctions.LowInfoContent)));

		public ValidateDetectorDescriptor<T> Min(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Min)));

		public ValidateDetectorDescriptor<T> Max(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Max)));

		public ValidateDetectorDescriptor<T> Median(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Median)));

		public ValidateDetectorDescriptor<T> HighMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.HighMedian)));

		public ValidateDetectorDescriptor<T> LowMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.LowMedian)));

		public ValidateDetectorDescriptor<T> Mean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Mean)));

		public ValidateDetectorDescriptor<T> HighMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.HighMean)));

		public ValidateDetectorDescriptor<T> LowMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.LowMean)));

		public ValidateDetectorDescriptor<T> Metric(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Metric)));

		public ValidateDetectorDescriptor<T> Varp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Varp)));

		public ValidateDetectorDescriptor<T> HighVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.HighVarp)));

		public ValidateDetectorDescriptor<T> LowVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.LowVarp)));

		public ValidateDetectorDescriptor<T> Rare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunctions.Rare)));

		public ValidateDetectorDescriptor<T> FreqRare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunctions.FreqRare)));

		public ValidateDetectorDescriptor<T> Sum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunctions.Sum)));

		public ValidateDetectorDescriptor<T> HighSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunctions.HighSum)));

		public ValidateDetectorDescriptor<T> LowSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunctions.LowSum)));

		public ValidateDetectorDescriptor<T> NonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(SumFunctions.NonNullSum)));

		public ValidateDetectorDescriptor<T> HighNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(SumFunctions.HighNonNullSum)));

		public ValidateDetectorDescriptor<T> LowNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(SumFunctions.LowNonNullSum)));

		public ValidateDetectorDescriptor<T> TimeOfDay(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunctions.TimeOfDay)));

		public ValidateDetectorDescriptor<T> TimeOfWeek(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunctions.TimeOfWeek)));
	}

	internal class ValidateDetectorRequestConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IValidateDetectorRequest)value;
			if (request == null)
			{
				writer.WriteNull();
				return;
			}

			serializer.Serialize(writer, request.Detector);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override bool CanConvert(Type objectType) => true;
	}
}
