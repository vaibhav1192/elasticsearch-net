using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetDatafeeds
{
	public class GetDatafeedsApiTests : ApiIntegrationTestBase<XPackCluster, IGetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: run a job in order to get bucket results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeeds(f),
			fluentAsync: (client, f) => client.GetDatafeedsAsync(f),
			request: (client, r) => client.GetDatafeeds(r),
			requestAsync: (client, r) => client.GetDatafeedsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"_xpack/ml/datafeeds";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f;

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetDatafeedsWithDatafeedIdApiTests : ApiIntegrationTestBase<XPackCluster, IGetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsWithDatafeedIdApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: run a job in order to get bucket results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeeds(f),
			fluentAsync: (client, f) => client.GetDatafeedsAsync(f),
			request: (client, r) => client.GetDatafeeds(r),
			requestAsync: (client, r) => client.GetDatafeedsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f
			.DatafeedId(CallIsolatedValue);

		protected override GetDatafeedsRequest Initializer => new GetDatafeedsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			// TODO: Implement
		}
	}
}
