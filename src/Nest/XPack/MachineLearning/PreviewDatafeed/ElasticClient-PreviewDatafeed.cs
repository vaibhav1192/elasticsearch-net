using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Opens a Machine Learning job.
		/// A job must be opened in order for it to be ready to receive and analyze data.
		/// A job can be opened and closed multiple times throughout its lifecycle.
		/// </summary>
		IPreviewDatafeedResponse PreviewDatafeed(Id id, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IPreviewDatafeedResponse PreviewDatafeed(IPreviewDatafeedRequest request);

		/// <inheritdoc/>
		Task<IPreviewDatafeedResponse> PreviewDatafeedAsync(Id id, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPreviewDatafeedResponse> PreviewDatafeedAsync(IPreviewDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPreviewDatafeedResponse PreviewDatafeed(Id id, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null) =>
			this.PreviewDatafeed(selector.InvokeOrDefault(new PreviewDatafeedDescriptor(id)));

		/// <inheritdoc/>
		public IPreviewDatafeedResponse PreviewDatafeed(IPreviewDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IPreviewDatafeedRequest, PreviewDatafeedRequestParameters, PreviewDatafeedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlPreviewDatafeedDispatch<PreviewDatafeedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IPreviewDatafeedResponse> PreviewDatafeedAsync(Id id, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PreviewDatafeedAsync(selector.InvokeOrDefault(new PreviewDatafeedDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPreviewDatafeedResponse> PreviewDatafeedAsync(IPreviewDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPreviewDatafeedRequest, PreviewDatafeedRequestParameters, PreviewDatafeedResponse, IPreviewDatafeedResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlPreviewDatafeedDispatchAsync<PreviewDatafeedResponse>(p, c)
			);
	}
}
