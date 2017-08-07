using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Creates a datafeed
	/// </summary>
	public partial interface IPutDatafeedRequest
	{
		/// <summary>
		/// If set, the datafeed performs aggregation searches
		/// </summary>
		[JsonProperty("aggregations")]
		AggregationDictionary Aggregations { get; set; }

		/// <summary>
		/// Specifies how data searches are split into time chunks.
		/// </summary>
		[JsonProperty("chunking_config")]
		IChunkingConfig ChunkingConfig { get; set; }

		/// <summary>
		/// The interval at which scheduled queries are made while the datafeed runs in real time.
		/// The default value is either the bucket span for short bucket spans, or, for longer bucket spans,
		/// a sensible fraction of the bucket span.
		/// </summary>
		[JsonProperty("frequency")]
		Time Frequency { get; set; }

		///<summary>A list of index names to search within. Wildcards are supported</summary>
		[JsonProperty("indices")]
		Indices Indices { get; set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the job.
		/// </summary>
		[JsonProperty("job_id")]
		Id JobId { get; set; }

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// The number of seconds behind real time that data is queried.
		/// For example, if data from 10:04 a.m. might not be searchable in Elasticsearch until 10:06 a.m.,
		/// set this property to 120 seconds. The default value is 60s.
		/// </summary>
		[JsonProperty("query_delay")]
		Time QueryDelay { get; set; }

		/// <summary>
		/// Specifies scripts that evaluate custom expressions and returns script fields to the datafeed. T
		/// he detector configuration in a job can contain functions that use these script fields.
		/// </summary>
		[JsonProperty("script_fields")]
		IScriptFields ScriptFields { get; set; }

		/// <summary>
		/// The size parameter that is used in Elasticsearch searches
		/// </summary>
		[JsonProperty("scroll_size")]
		int? ScrollSize { get; set; }

		///<summary>A list of types to search for within the specified indices</summary>
		[JsonProperty("types")]
		Types Types { get; set; }
	}

	/// <inheritdoc />
	public partial class PutDatafeedRequest
	{
		/// <inheritdoc />
		public AggregationDictionary Aggregations { get; set; }

		/// <inheritdoc />
		public IChunkingConfig ChunkingConfig { get; set; }

		/// <inheritdoc />
		public Time Frequency { get; set; }

		/// <inheritdoc />
		public Indices Indices { get; set; }

		/// <inheritdoc />
		public Id JobId { get; set; }

		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public Time QueryDelay { get; set; }

		/// <inheritdoc />
		public IScriptFields ScriptFields { get; set; }

		/// <inheritdoc />
		public int? ScrollSize { get; set; }

		/// <inheritdoc />
		public Types Types { get; set; }
	}

	[DescriptorFor("XpackMlPutDatafeed")]
	public partial class PutDatafeedDescriptor
	{
		AggregationDictionary IPutDatafeedRequest.Aggregations { get; set; }
		IChunkingConfig IPutDatafeedRequest.ChunkingConfig { get; set; }
		Time IPutDatafeedRequest.Frequency { get; set; }
		Indices IPutDatafeedRequest.Indices { get; set; }
		Id IPutDatafeedRequest.JobId { get; set; }
		QueryContainer IPutDatafeedRequest.Query { get; set; }
		Time IPutDatafeedRequest.QueryDelay { get; set; }
		IScriptFields IPutDatafeedRequest.ScriptFields { get; set; }
		int? IPutDatafeedRequest.ScrollSize { get; set; }
		Types IPutDatafeedRequest.Types { get; set; }

		/// <inheritdoc />
		public PutDatafeedDescriptor Aggregations<T>(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) where T : class =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		/// <inheritdoc />
		public PutDatafeedDescriptor ChunkingConfig(Func<ChunkingConfigDescriptor, IChunkingConfig> selector) =>
			Assign(a => a.ChunkingConfig = selector.InvokeOrDefault(new ChunkingConfigDescriptor()));

		/// <inheritdoc />
		public PutDatafeedDescriptor Frequency(Time frequency) => Assign(a => a.Frequency = frequency);

		/// <inheritdoc />
		public PutDatafeedDescriptor Indices(Indices indices) => Assign(a => a.Indices = indices);

		///<summary>a shortcut into calling Indices(typeof(T))</summary>
		public PutDatafeedDescriptor Indices<T>() where T : class => Assign(a => a.Indices = (Indices)typeof(T));

		///<summary>A shortcut into calling Indices(Indices.All)</summary>
		public PutDatafeedDescriptor AllIndices() => this.Indices(Nest.Indices.All);

		/// <inheritdoc />
		public PutDatafeedDescriptor JobId(Id jobId) => Assign(a => a.JobId = jobId);

		/// <inheritdoc />
		public PutDatafeedDescriptor Query<T>(Func<QueryContainerDescriptor<T>, QueryContainer> query) where T : class =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc />
		public PutDatafeedDescriptor QueryDelay(Time queryDelay) => Assign(a => a.QueryDelay = queryDelay);

		/// <inheritdoc />
		public PutDatafeedDescriptor ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		/// <inheritdoc />
		public PutDatafeedDescriptor ScrollSize(int scrollSize) => Assign(a => a.ScrollSize = scrollSize);

		/// <inheritdoc />
		public PutDatafeedDescriptor Types(Types types) => Assign(a => a.Types = types);

		///<summary>a shortcut into calling Types(typeof(T))</summary>
		public PutDatafeedDescriptor Types<T>() where T : class =>
			Assign(a => a.Types = (Types)typeof(T));

		///<summary>a shortcut into calling Types(Types.All)</summary>
		public PutDatafeedDescriptor AllTypes() => this.Types(Nest.Types.All);
	}
}
