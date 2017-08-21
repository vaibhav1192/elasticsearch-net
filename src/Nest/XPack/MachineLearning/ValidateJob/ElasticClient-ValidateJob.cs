using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	// TODO: Implement
	public partial interface IElasticClient
	{
		/// <summary>
		/// Validates a Machine Learning job
		/// </summary>
		IValidateJobResponse ValidateJob(Func<ValidateJobDescriptor, IValidateJobRequest> selector);

		/// <inheritdoc/>
		IValidateJobResponse ValidateJob(IValidateJobRequest request);

		/// <inheritdoc/>
		Task<IValidateJobResponse> ValidateJobAsync(Func<ValidateJobDescriptor, IValidateJobRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IValidateJobResponse ValidateJob(Func<ValidateJobDescriptor, IValidateJobRequest> selector = null) =>
			this.ValidateJob(selector.InvokeOrDefault(new ValidateJobDescriptor()));

		/// <inheritdoc/>
		public IValidateJobResponse ValidateJob(IValidateJobRequest request) =>
			this.Dispatcher.Dispatch<IValidateJobRequest, ValidateJobRequestParameters, ValidateJobResponse>(
				request,
				this.LowLevelDispatch.XpackMlValidateDispatch<ValidateJobResponse>
			);

		/// <inheritdoc/>
		public Task<IValidateJobResponse> ValidateJobAsync(Func<ValidateJobDescriptor, IValidateJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ValidateJobAsync(selector.InvokeOrDefault(new ValidateJobDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IValidateJobRequest, ValidateJobRequestParameters, ValidateJobResponse, IValidateJobResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlValidateDispatchAsync<ValidateJobResponse>
			);
	}
}
