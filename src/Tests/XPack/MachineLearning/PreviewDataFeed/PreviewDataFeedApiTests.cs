using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.PreviewDatafeed
{
	public class PreviewDatafeedApiTests : MachineLearningIntegrationTestBase<IPreviewDatafeedResponse, IPreviewDatafeedRequest, PreviewDatafeedDescriptor, PreviewDatafeedRequest>
	{
		public PreviewDatafeedApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PreviewDatafeed(CallIsolatedValue + "-datafeed", f),
			fluentAsync: (client, f) => client.PreviewDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			request: (client, r) => client.PreviewDatafeed(r),
			requestAsync: (client, r) => client.PreviewDatafeedAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed/_preview";
		protected override bool SupportsDeserialization => false;
		protected override PreviewDatafeedDescriptor NewDescriptor() => new PreviewDatafeedDescriptor(CallIsolatedValue + "-datafeed");
		protected override object ExpectJson => null;
		protected override Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> Fluent => f => f;
		protected override PreviewDatafeedRequest Initializer => new PreviewDatafeedRequest(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IPreviewDatafeedResponse response)
		{
			response.IsValid.Should().BeTrue();
		}
	}
}
