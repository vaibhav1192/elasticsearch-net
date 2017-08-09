using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedApiTests : ApiIntegrationTestBase<XPackCluster, IDeleteDatafeedResponse, IDeleteDatafeedRequest, DeleteDatafeedDescriptor, DeleteDatafeedRequest>
	{
		public DeleteDatafeedApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a datafed, to allow it to be deleted

		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteDatafeed(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteDatafeedAsync(CallIsolatedValue, f),
			request: (client, r) => client.DeleteDatafeed(r),
			requestAsync: (client, r) => client.DeleteDatafeedAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override DeleteDatafeedDescriptor NewDescriptor() => new DeleteDatafeedDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> Fluent => f => f;

		protected override DeleteDatafeedRequest Initializer => new DeleteDatafeedRequest(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteDatafeedResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
