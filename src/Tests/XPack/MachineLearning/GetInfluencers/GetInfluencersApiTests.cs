using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetInfluencers
{
	public class GetInfluencersApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IGetInfluencersResponse, IGetInfluencersRequest, GetInfluencersDescriptor, GetInfluencersRequest>
	{
		public GetInfluencersApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetInfluencers(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetInfluencersAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetInfluencers(r),
			requestAsync: (client, r) => client.GetInfluencersAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/influencers";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetInfluencersDescriptor, IGetInfluencersRequest> Fluent => f => f;

		protected override GetInfluencersRequest Initializer =>
			new GetInfluencersRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetInfluencersResponse response)
		{
			// TODO: Implement
		}
	}
}
