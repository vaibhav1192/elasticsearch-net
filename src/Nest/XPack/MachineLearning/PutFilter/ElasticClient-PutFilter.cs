using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a filter for a Machine Learning job.
		/// </summary>
		IPutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector);

		/// <inheritdoc/>
		IPutFilterResponse PutFilter(IPutFilterRequest request);

		/// <inheritdoc/>
		Task<IPutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPutFilterResponse> PutFilterAsync(IPutFilterRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector) =>
			this.PutFilter(selector.InvokeOrDefault(new PutFilterDescriptor(filterId)));

		/// <inheritdoc/>
		public IPutFilterResponse PutFilter(IPutFilterRequest request) =>
			this.Dispatcher.Dispatch<IPutFilterRequest, PutFilterRequestParameters, PutFilterResponse>(
				request,
				this.LowLevelDispatch.XpackMlPutFilterDispatch<PutFilterResponse>
			);

		/// <inheritdoc/>
		public Task<IPutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PutFilterAsync(selector.InvokeOrDefault(new PutFilterDescriptor(filterId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutFilterResponse> PutFilterAsync(IPutFilterRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutFilterRequest, PutFilterRequestParameters, PutFilterResponse, IPutFilterResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlPutFilterDispatchAsync<PutFilterResponse>
			);
	}
}
