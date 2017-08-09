using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a Machine Learning job.
		/// A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
		/// </summary>
		IDeleteJobResponse DeleteJob(Id id, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null);

		/// <inheritdoc/>
		IDeleteJobResponse DeleteJob(IDeleteJobRequest request);

		/// <inheritdoc/>
		Task<IDeleteJobResponse> DeleteJobAsync(Id id, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteJobResponse DeleteJob(Id id, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null) =>
			this.DeleteJob(selector.InvokeOrDefault(new DeleteJobDescriptor(id)));

		/// <inheritdoc/>
		public IDeleteJobResponse DeleteJob(IDeleteJobRequest request) =>
			this.Dispatcher.Dispatch<IDeleteJobRequest, DeleteJobRequestParameters, DeleteJobResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlDeleteJobDispatch<DeleteJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteJobResponse> DeleteJobAsync(Id id, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteJobAsync(selector.InvokeOrDefault(new DeleteJobDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteJobRequest, DeleteJobRequestParameters, DeleteJobResponse, IDeleteJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlDeleteJobDispatchAsync<DeleteJobResponse>(p, c)
			);
	}
}
