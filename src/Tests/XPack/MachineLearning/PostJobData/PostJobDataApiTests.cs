using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.PostJobData
{
	public class PostJobDataApiTests : MachineLearningIntegrationTestBase<IPostJobDataResponse, IPostJobDataRequest, PostJobDataDescriptor, PostJobDataRequest>
	{
		public PostJobDataApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PostJobData(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PostJobDataAsync(CallIsolatedValue, f),
			request: (client, r) => client.PostJobData(r),
			requestAsync: (client, r) => client.PostJobDataAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_data";
		protected override bool SupportsDeserialization => false;
		protected override PostJobDataDescriptor NewDescriptor() => new PostJobDataDescriptor(CallIsolatedValue);
		protected override object ExpectJson => null;
		protected override Func<PostJobDataDescriptor, IPostJobDataRequest> Fluent => f => f;
		protected override PostJobDataRequest Initializer => new PostJobDataRequest(CallIsolatedValue);

		protected override void ExpectResponse(IPostJobDataResponse response)
		{
		}
	}
}
