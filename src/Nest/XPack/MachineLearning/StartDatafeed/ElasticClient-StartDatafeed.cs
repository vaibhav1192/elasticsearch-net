using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IStartDatafeedResponse StartDatafeed(Id id, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IStartDatafeedResponse StartDatafeed(IStartDatafeedRequest request);

		/// <inheritdoc/>
		Task<IStartDatafeedResponse> StartDatafeedAsync(Id id, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IStartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStartDatafeedResponse StartDatafeed(Id id, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null) =>
			this.StartDatafeed(selector.InvokeOrDefault(new StartDatafeedDescriptor(id)));

		/// <inheritdoc/>
		public IStartDatafeedResponse StartDatafeed(IStartDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IStartDatafeedRequest, StartDatafeedRequestParameters, StartDatafeedResponse>(
				request,
				this.LowLevelDispatch.XpackMlStartDatafeedDispatch<StartDatafeedResponse>
			);

		/// <inheritdoc/>
		public Task<IStartDatafeedResponse> StartDatafeedAsync(Id id, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.StartDatafeedAsync(selector.InvokeOrDefault(new StartDatafeedDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IStartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IStartDatafeedRequest, StartDatafeedRequestParameters, StartDatafeedResponse, IStartDatafeedResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlStartDatafeedDispatchAsync<StartDatafeedResponse>
			);
	}
}
