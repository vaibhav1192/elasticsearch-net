using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IPutJobResponse, IPutJobRequest, PutJobDescriptor<Project>, PutJobRequest>
	{
		public PutJobApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutJob(r),
			requestAsync: (client, r) => client.PutJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override PutJobDescriptor<Project> NewDescriptor() => new PutJobDescriptor<Project>(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<PutJobDescriptor<Project>, IPutJobRequest> Fluent => f => f
			.AnalysisConfig(a => a
				.BucketSpan("1d")
			);

		protected override PutJobRequest Initializer =>
			new PutJobRequest(CallIsolatedValue);

		protected override void ExpectResponse(IPutJobResponse response)
		{
			response.ShouldBeValid();
		}
	}
}
