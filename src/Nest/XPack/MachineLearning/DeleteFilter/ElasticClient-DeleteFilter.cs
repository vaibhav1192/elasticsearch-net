using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Delete a filter for Machine Learning.
		/// </summary>
		IDeleteFilterResponse DeleteFilter(Id id, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null);

		/// <inheritdoc/>
		IDeleteFilterResponse DeleteFilter(IDeleteFilterRequest request);

		/// <inheritdoc/>
		Task<IDeleteFilterResponse> DeleteFilterAsync(Id id, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteFilterResponse DeleteFilter(Id id, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null) =>
			this.DeleteFilter(selector.InvokeOrDefault(new DeleteFilterDescriptor(id)));

		/// <inheritdoc/>
		public IDeleteFilterResponse DeleteFilter(IDeleteFilterRequest request) =>
			this.Dispatcher.Dispatch<IDeleteFilterRequest, DeleteFilterRequestParameters, DeleteFilterResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlDeleteFilterDispatch<DeleteFilterResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteFilterResponse> DeleteFilterAsync(Id id, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteFilterAsync(selector.InvokeOrDefault(new DeleteFilterDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteFilterRequest, DeleteFilterRequestParameters, DeleteFilterResponse, IDeleteFilterResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlDeleteFilterDispatchAsync<DeleteFilterResponse>(p, c)
			);
	}
}
