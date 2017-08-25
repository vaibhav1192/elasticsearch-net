using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.CloseJob
{
	public class CloseJobApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, ICloseJobResponse, ICloseJobRequest, CloseJobDescriptor, CloseJobRequest>
	{
		public CloseJobApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: open a job, to allow it to be closed

		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CloseJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.CloseJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.CloseJob(r),
			requestAsync: (client, r) => client.CloseJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_close";

		protected override bool SupportsDeserialization => true;

		protected override CloseJobDescriptor NewDescriptor() => new CloseJobDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<CloseJobDescriptor, ICloseJobRequest> Fluent => f => f;

		protected override CloseJobRequest Initializer =>
			new CloseJobRequest(CallIsolatedValue);

		protected override void ExpectResponse(ICloseJobResponse response)
		{
			response.Closed.Should().BeTrue();
		}
	}
}
