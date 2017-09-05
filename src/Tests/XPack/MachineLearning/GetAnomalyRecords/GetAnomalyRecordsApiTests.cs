using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.GetAnomalyRecords
{
	public class GetAnomalyRecordsApiTests : MachineLearningIntegrationTestBase<IGetAnomalyRecordsResponse, IGetAnomalyRecordsRequest, GetAnomalyRecordsDescriptor, GetAnomalyRecordsRequest>
	{
		public GetAnomalyRecordsApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
			}
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
			response.ShouldBeValid();
			response.Count.Should().Be(0);
			response.Records.Should().BeEmpty();
		}
	}
}
