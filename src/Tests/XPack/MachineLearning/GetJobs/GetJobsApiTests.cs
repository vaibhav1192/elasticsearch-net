using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetJobs
{
	public class GetJobsApiTests : MachineLearningIntegrationTestBase<IGetJobsResponse, IGetJobsRequest, GetJobsDescriptor, GetJobsRequest>
	{
		public GetJobsApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetJobs(f),
			fluentAsync: (client, f) => client.GetJobsAsync(f),
			request: (client, r) => client.GetJobs(r),
			requestAsync: (client, r) => client.GetJobsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetJobsDescriptor, IGetJobsRequest> Fluent => f => f;
		protected override GetJobsRequest Initializer => new GetJobsRequest();

		protected override void ExpectResponse(IGetJobsResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetJobsWithJobIdApiTests : MachineLearningIntegrationTestBase<IGetJobsResponse, IGetJobsRequest, GetJobsDescriptor, GetJobsRequest>
	{
		public GetJobsWithJobIdApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetJobs(f),
			fluentAsync: (client, f) => client.GetJobsAsync(f),
			request: (client, r) => client.GetJobs(r),
			requestAsync: (client, r) => client.GetJobsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetJobsDescriptor, IGetJobsRequest> Fluent => f => f.JobId(CallIsolatedValue);
		protected override GetJobsRequest Initializer => new GetJobsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetJobsResponse response)
		{
			// TODO: Implement
		}
	}
}
