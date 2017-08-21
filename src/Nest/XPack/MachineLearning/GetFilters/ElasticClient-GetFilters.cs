using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves filters for Machine Learning jobs.
		/// </summary>
		IGetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null);

		/// <inheritdoc/>
		IGetFiltersResponse GetFilters(IGetFiltersRequest request);

		/// <inheritdoc/>
		Task<IGetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null) =>
			this.GetFilters(selector.InvokeOrDefault(new GetFiltersDescriptor()));

		/// <inheritdoc/>
		public IGetFiltersResponse GetFilters(IGetFiltersRequest request) =>
			this.Dispatcher.Dispatch<IGetFiltersRequest, GetFiltersRequestParameters, GetFiltersResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlGetFiltersDispatch<GetFiltersResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetFiltersAsync(selector.InvokeOrDefault(new GetFiltersDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetFiltersRequest, GetFiltersRequestParameters, GetFiltersResponse, IGetFiltersResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlGetFiltersDispatchAsync<GetFiltersResponse>(p, c)
			);
	}
}
