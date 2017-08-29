using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	// TODO: Detector.java has detection rules, but these are not listed in the docs:
	// TODO: Investigate of these can be set wherever a detector can be passed
	[ContractJsonConverter(typeof(DetectorConverter))]
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

	internal class DetectorConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var jObject = JObject.Load(reader);
			var function = jObject["function"].Value<string>();

			switch (function)
			{
				case "count":
					return jObject.ToObject<CountDetector>(ElasticContractResolver.Empty);
				case "high_count":
					return jObject.ToObject<HighCountDetector>(ElasticContractResolver.Empty);
				case "low_count":
					return jObject.ToObject<LowCountDetector>(ElasticContractResolver.Empty);
				case "non_zero_count":
					return jObject.ToObject<NonZeroCountDetector>(ElasticContractResolver.Empty);
				case "high_non_zero_count":
					return jObject.ToObject<HighNonZeroCountDetector>(ElasticContractResolver.Empty);
				case "low_non_zero_count":
					return jObject.ToObject<LowNonZeroCountDetector>(ElasticContractResolver.Empty);
				case "distinct_count":
					return jObject.ToObject<DistinctCountDetector>(ElasticContractResolver.Empty);
				case "high_distinct_count":
					return jObject.ToObject<HighDistinctCountDetector>(ElasticContractResolver.Empty);
				case "low_distinct_count":
					return jObject.ToObject<LowDistinctCountDetector>(ElasticContractResolver.Empty);
				case "lat_long":
					return jObject.ToObject<LatLongDetector>(ElasticContractResolver.Empty);
				case "info_content":
					return jObject.ToObject<InfoContentDetector>(ElasticContractResolver.Empty);
				case "high_info_content":
					return jObject.ToObject<HighInfoContentDetector>(ElasticContractResolver.Empty);
				case "low_info_content":
					return jObject.ToObject<LowInfoContentDetector>(ElasticContractResolver.Empty);
				case "min":
					return jObject.ToObject<MinDetector>(ElasticContractResolver.Empty);
				case "max":
					return jObject.ToObject<MaxDetector>(ElasticContractResolver.Empty);
				case "median":
					return jObject.ToObject<MedianDetector>(ElasticContractResolver.Empty);
				case "high_median":
					return jObject.ToObject<HighMedianDetector>(ElasticContractResolver.Empty);
				case "low_median":
					return jObject.ToObject<LowMedianDetector>(ElasticContractResolver.Empty);
				case "mean":
					return jObject.ToObject<MeanDetector>(ElasticContractResolver.Empty);
				case "high_mean":
					return jObject.ToObject<HighMeanDetector>(ElasticContractResolver.Empty);
				case "low_mean":
					return jObject.ToObject<LowMeanDetector>(ElasticContractResolver.Empty);
				case "metric":
					return jObject.ToObject<MetricDetector>(ElasticContractResolver.Empty);
				case "varp":
					return jObject.ToObject<VarpDetector>(ElasticContractResolver.Empty);
				case "high_varp":
					return jObject.ToObject<HighVarpDetector>(ElasticContractResolver.Empty);
				case "low_varp":
					return jObject.ToObject<LowVarpDetector>(ElasticContractResolver.Empty);
				case "rare":
					return jObject.ToObject<RareDetector>(ElasticContractResolver.Empty);
				case "freq_rare":
					return jObject.ToObject<FreqRareDetector>(ElasticContractResolver.Empty);
				case "sum":
					 return jObject.ToObject<SumDetector>(ElasticContractResolver.Empty);
				case "high_sum":
					return jObject.ToObject<HighSumDetector>(ElasticContractResolver.Empty);
				case "low_sum":
					return jObject.ToObject<LowSumDetector>(ElasticContractResolver.Empty);
				case "non_null_sum":
					return jObject.ToObject<NonNullSumDetector>(ElasticContractResolver.Empty);
				case "high_non_null_sum":
					return jObject.ToObject<HighNonNullSumDetector>(ElasticContractResolver.Empty);
				case "low_non_null_sum":
					return jObject.ToObject<LowNonNullSumDetector>(ElasticContractResolver.Empty);
				case "time_of_day":
					return jObject.ToObject<TimeOfDayDetector>(ElasticContractResolver.Empty);
				case "time_of_week":
					return jObject.ToObject<TimeOfWeekDetector>(ElasticContractResolver.Empty);
				default:
					throw new JsonSerializationException($"Cannot deserialize detector for unknown function '{function}");
			}
		}

		public override bool CanConvert(Type objectType) => true;
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

	public class DetectorsDescriptor<T> : DescriptorPromiseBase<DetectorsDescriptor<T>, IList<IDetector>> where T : class
	{
		public DetectorsDescriptor() : base(new List<IDetector>()) {}

		public DetectorsDescriptor<T> Count(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunctions.Count))));

		public DetectorsDescriptor<T> HighCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunctions.HighCount))));

		public DetectorsDescriptor<T> LowCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunctions.LowCount))));

		public DetectorsDescriptor<T> NonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(CountFunctions.NonZeroCount))));

		public DetectorsDescriptor<T> HighNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(CountFunctions.HighNonZeroCount))));

		public DetectorsDescriptor<T> LowNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(CountFunctions.LowNonZeroCount))));

		public DetectorsDescriptor<T> DistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(CountFunctions.DistinctCount))));

		public DetectorsDescriptor<T> HighDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(CountFunctions.HighDistinctCount))));

		public DetectorsDescriptor<T> LowDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(CountFunctions.LowDistinctCount))));

		public DetectorsDescriptor<T> InfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunctions.InfoContent))));

		public DetectorsDescriptor<T> HighInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunctions.HighInfoContent))));

		public DetectorsDescriptor<T> LowInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunctions.LowInfoContent))));

		public DetectorsDescriptor<T> Min(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Min))));

		public DetectorsDescriptor<T> Max(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Max))));

		public DetectorsDescriptor<T> Median(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Median))));

		public DetectorsDescriptor<T> HighMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.HighMedian))));

		public DetectorsDescriptor<T> LowMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.LowMedian))));

		public DetectorsDescriptor<T> Mean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Mean))));

		public DetectorsDescriptor<T> HighMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.HighMean))));

		public DetectorsDescriptor<T> LowMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.LowMean))));

		public DetectorsDescriptor<T> Metric(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Metric))));

		public DetectorsDescriptor<T> Varp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.Varp))));

		public DetectorsDescriptor<T> HighVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.HighVarp))));

		public DetectorsDescriptor<T> LowVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunctions.LowVarp))));

		public DetectorsDescriptor<T> Rare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunctions.Rare))));

		public DetectorsDescriptor<T> FreqRare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunctions.FreqRare))));

		public DetectorsDescriptor<T> Sum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunctions.Sum))));

		public DetectorsDescriptor<T> HighSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunctions.HighSum))));

		public DetectorsDescriptor<T> LowSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunctions.LowSum))));

		public DetectorsDescriptor<T> NonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(SumFunctions.NonNullSum))));

		public DetectorsDescriptor<T> HighNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(SumFunctions.HighNonNullSum))));

		public DetectorsDescriptor<T> LowNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(SumFunctions.LowNonNullSum))));

		public DetectorsDescriptor<T> TimeOfDay(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunctions.TimeOfDay))));

		public DetectorsDescriptor<T> TimeOfWeek(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunctions.TimeOfWeek))));
	}
}
