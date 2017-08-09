using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a Machine Learning job.
		/// </summary>
		IPutJobResponse PutJob<T>(Id id, Func<PutJobDescriptor<T>, IPutJobRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IPutJobResponse PutJob(IPutJobRequest request);

		/// <inheritdoc/>
		Task<IPutJobResponse> PutJobAsync<T>(Id id, Func<PutJobDescriptor<T>, IPutJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutJobResponse PutJob<T>(Id id, Func<PutJobDescriptor<T>, IPutJobRequest> selector = null) where T : class =>
			this.PutJob(selector.InvokeOrDefault(new PutJobDescriptor<T>(id)));

		/// <inheritdoc/>
		public IPutJobResponse PutJob(IPutJobRequest request) =>
			this.Dispatcher.Dispatch<IPutJobRequest, PutJobRequestParameters, PutJobResponse>(
				request,
				this.LowLevelDispatch.XpackMlPutJobDispatch<PutJobResponse>
			);

		/// <inheritdoc/>
		public Task<IPutJobResponse> PutJobAsync<T>(Id id, Func<PutJobDescriptor<T>, IPutJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.PutJobAsync(selector.InvokeOrDefault(new PutJobDescriptor<T>(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutJobRequest, PutJobRequestParameters, PutJobResponse, IPutJobResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlPutJobDispatchAsync<PutJobResponse>
			);
	}
}
