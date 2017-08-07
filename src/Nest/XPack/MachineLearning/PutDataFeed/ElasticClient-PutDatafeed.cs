using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a Machine Learning datafeed.
		/// You must create a job before you create a datafeed. You can associate only one datafeed to each job.
		/// </summary>
		IPutDatafeedResponse PutDatafeed(Id id, Func<PutDatafeedDescriptor, IPutDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IPutDatafeedResponse PutDatafeed(IPutDatafeedRequest request);

		/// <inheritdoc/>
		Task<IPutDatafeedResponse> PutDatafeedAsync(Id id, Func<PutDatafeedDescriptor, IPutDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutDatafeedResponse PutDatafeed(Id id, Func<PutDatafeedDescriptor, IPutDatafeedRequest> selector = null) =>
			this.PutDatafeed(selector.InvokeOrDefault(new PutDatafeedDescriptor(id)));

		/// <inheritdoc/>
		public IPutDatafeedResponse PutDatafeed(IPutDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IPutDatafeedRequest, PutDatafeedRequestParameters, PutDatafeedResponse>(
				request,
				this.LowLevelDispatch.XpackMlPutDatafeedDispatch<PutDatafeedResponse>
			);

		/// <inheritdoc/>
		public Task<IPutDatafeedResponse> PutDatafeedAsync(Id id, Func<PutDatafeedDescriptor, IPutDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PutDatafeedAsync(selector.InvokeOrDefault(new PutDatafeedDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutDatafeedRequest, PutDatafeedRequestParameters, PutDatafeedResponse, IPutDatafeedResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlPutDatafeedDispatchAsync<PutDatafeedResponse>
			);
	}
}
