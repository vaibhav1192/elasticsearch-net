using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IStopDatafeedResponse StopDatafeed(Id id, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IStopDatafeedResponse StopDatafeed(IStopDatafeedRequest request);

		/// <inheritdoc/>
		Task<IStopDatafeedResponse> StopDatafeedAsync(Id id, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IStopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStopDatafeedResponse StopDatafeed(Id id, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null) =>
			this.StopDatafeed(selector.InvokeOrDefault(new StopDatafeedDescriptor(id)));

		/// <inheritdoc/>
		public IStopDatafeedResponse StopDatafeed(IStopDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IStopDatafeedRequest, StopDatafeedRequestParameters, StopDatafeedResponse>(
				request,
				this.LowLevelDispatch.XpackMlStopDatafeedDispatch<StopDatafeedResponse>
			);

		/// <inheritdoc/>
		public Task<IStopDatafeedResponse> StopDatafeedAsync(Id id, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.StopDatafeedAsync(selector.InvokeOrDefault(new StopDatafeedDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IStopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IStopDatafeedRequest, StopDatafeedRequestParameters, StopDatafeedResponse, IStopDatafeedResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlStopDatafeedDispatchAsync<StopDatafeedResponse>
			);
	}
}
