using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.PutDatafeed
{
	public class PutDatafeedApiTests : ApiIntegrationTestBase<XPackCluster, IPutDatafeedResponse, IPutDatafeedRequest, PutDatafeedDescriptor<Project>, PutDatafeedRequest>
	{
		public PutDatafeedApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutDatafeed(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutDatafeedAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutDatafeed(r),
			requestAsync: (client, r) => client.PutDatafeedAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override PutDatafeedDescriptor<Project> NewDescriptor() => new PutDatafeedDescriptor<Project>(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<PutDatafeedDescriptor<Project>, IPutDatafeedRequest> Fluent => f => f
			//TODO
			;

		protected override PutDatafeedRequest Initializer =>
			new PutDatafeedRequest(CallIsolatedValue);

		protected override void ExpectResponse(IPutDatafeedResponse response)
		{
			response.ShouldBeValid();


		}
	}
}
