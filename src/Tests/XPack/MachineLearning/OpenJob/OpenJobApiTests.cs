using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.OpenJob
{
	public class OpenJobApiTests : ApiIntegrationTestBase<XPackCluster, IOpenJobResponse, IOpenJobRequest, OpenJobDescriptor, OpenJobRequest>
	{
		public OpenJobApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job, to allow it to be opened

		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.OpenJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.OpenJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.OpenJob(r),
			requestAsync: (client, r) => client.OpenJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_open";

		protected override bool SupportsDeserialization => true;

		protected override OpenJobDescriptor NewDescriptor() => new OpenJobDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<OpenJobDescriptor, IOpenJobRequest> Fluent => f => f;

		protected override OpenJobRequest Initializer =>
			new OpenJobRequest(CallIsolatedValue);

		protected override void ExpectResponse(IOpenJobResponse response)
		{
			response.Opened.Should().BeTrue();
		}
	}
}
