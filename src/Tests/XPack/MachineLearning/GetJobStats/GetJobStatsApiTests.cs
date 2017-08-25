using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetJobStats
{
	public class GetJobStatsApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IGetJobStatsResponse, IGetJobStatsRequest, GetJobStatsDescriptor, GetJobStatsRequest>
	{
		public GetJobStatsApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetJobStats(f),
			fluentAsync: (client, f) => client.GetJobStatsAsync(f),
			request: (client, r) => client.GetJobStats(r),
			requestAsync: (client, r) => client.GetJobStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/_stats";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetJobStatsDescriptor, IGetJobStatsRequest> Fluent => f => f;

		protected override GetJobStatsRequest Initializer => new GetJobStatsRequest();

		protected override void ExpectResponse(IGetJobStatsResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetJobStatsWithJobIdApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IGetJobStatsResponse, IGetJobStatsRequest, GetJobStatsDescriptor, GetJobStatsRequest>
	{
		public GetJobStatsWithJobIdApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetJobStats(f),
			fluentAsync: (client, f) => client.GetJobStatsAsync(f),
			request: (client, r) => client.GetJobStats(r),
			requestAsync: (client, r) => client.GetJobStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_stats";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetJobStatsDescriptor, IGetJobStatsRequest> Fluent => f => f
			.JobId(CallIsolatedValue);

		protected override GetJobStatsRequest Initializer =>
			new GetJobStatsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetJobStatsResponse response)
		{
			// TODO: Implement
		}
	}
}
