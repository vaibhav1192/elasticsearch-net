using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Forces any buffered data to be processed by the Machine Learning job.
		/// </summary>
		IFlushJobResponse FlushJob(Id id, Func<FlushJobDescriptor, IFlushJobRequest> selector = null);

		/// <inheritdoc/>
		IFlushJobResponse FlushJob(IFlushJobRequest request);

		/// <inheritdoc/>
		Task<IFlushJobResponse> FlushJobAsync(Id id, Func<FlushJobDescriptor, IFlushJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IFlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IFlushJobResponse FlushJob(Id id, Func<FlushJobDescriptor, IFlushJobRequest> selector = null) =>
			this.FlushJob(selector.InvokeOrDefault(new FlushJobDescriptor(id)));

		/// <inheritdoc/>
		public IFlushJobResponse FlushJob(IFlushJobRequest request) =>
			this.Dispatcher.Dispatch<IFlushJobRequest, FlushJobRequestParameters, FlushJobResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlFlushJobDispatch<FlushJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IFlushJobResponse> FlushJobAsync(Id id, Func<FlushJobDescriptor, IFlushJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.FlushJobAsync(selector.InvokeOrDefault(new FlushJobDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IFlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IFlushJobRequest, FlushJobRequestParameters, FlushJobResponse, IFlushJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlFlushJobDispatchAsync<FlushJobResponse>(p, c)
			);
	}
}
