using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IGetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: run a job in order to get results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeedStats(f),
			fluentAsync: (client, f) => client.GetDatafeedStatsAsync(f),
			request: (client, r) => client.GetDatafeedStats(r),
			requestAsync: (client, r) => client.GetDatafeedStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"_xpack/ml/datafeeds/_stats";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f;

		protected override void ExpectResponse(IGetDatafeedStatsResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetDatafeedStatsWithDatafeedIdApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IGetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsWithDatafeedIdApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: run a job in order to get results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeedStats(f),
			fluentAsync: (client, f) => client.GetDatafeedStatsAsync(f),
			request: (client, r) => client.GetDatafeedStats(r),
			requestAsync: (client, r) => client.GetDatafeedStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}/_stats";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f
			.DatafeedId(CallIsolatedValue);

		protected override GetDatafeedStatsRequest Initializer => new GetDatafeedStatsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetDatafeedStatsResponse response)
		{
			// TODO: Implement
		}
	}
}
