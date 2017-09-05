using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.XPack.MachineLearning.ValidateJob
{
	public class ValidateJobApiTests : MachineLearningIntegrationTestBase<IValidateJobResponse, IValidateJobRequest, ValidateJobDescriptor<Metric>, ValidateJobRequest>
	{
		public ValidateJobApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ValidateJob(f),
			fluentAsync: (client, f) => client.ValidateJobAsync(f),
			request: (client, r) => client.ValidateJob(r),
			requestAsync: (client, r) => client.ValidateJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/_validate";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<ValidateJobDescriptor<Metric>, IValidateJobRequest> Fluent => f => f;
		protected override ValidateJobRequest Initializer => new ValidateJobRequest();

		protected override void ExpectResponse(IValidateJobResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
