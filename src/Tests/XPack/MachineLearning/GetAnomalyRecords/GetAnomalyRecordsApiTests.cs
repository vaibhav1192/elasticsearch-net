using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetAnomalyRecords
{
	public class GetAnomalyRecordsApiTests : ApiIntegrationTestBase<XPackCluster, IGetAnomalyRecordsResponse, IGetAnomalyRecordsRequest, GetAnomalyRecordsDescriptor, GetAnomalyRecordsRequest>
	{
		public GetAnomalyRecordsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job, run it, to get results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetAnomalyRecords(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetAnomalyRecordsAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetAnomalyRecords(r),
			requestAsync: (client, r) => client.GetAnomalyRecordsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/records";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> Fluent => f => f;

		protected override GetAnomalyRecordsRequest Initializer => new GetAnomalyRecordsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetAnomalyRecordsResponse response)
		{
			// TODO: Implement
		}
	}
}
