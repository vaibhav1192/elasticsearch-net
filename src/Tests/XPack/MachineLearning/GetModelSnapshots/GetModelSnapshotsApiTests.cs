using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetModelSnapshots
{
	public class GetModelSnapshotsApiTests : MachineLearningIntegrationTestBase<IGetModelSnapshotsResponse, IGetModelSnapshotsRequest, GetModelSnapshotsDescriptor, GetModelSnapshotsRequest>
	{
		public GetModelSnapshotsApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetModelSnapshots(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetModelSnapshotsAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetModelSnapshots(r),
			requestAsync: (client, r) => client.GetModelSnapshotsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> Fluent => f => f;
		protected override GetModelSnapshotsDescriptor NewDescriptor() => new GetModelSnapshotsDescriptor(CallIsolatedValue);
		protected override GetModelSnapshotsRequest Initializer => new GetModelSnapshotsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetModelSnapshotsResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetModelSnapshotsWithSnapshotIdApiTests : MachineLearningIntegrationTestBase<IGetModelSnapshotsResponse, IGetModelSnapshotsRequest, GetModelSnapshotsDescriptor, GetModelSnapshotsRequest>
	{
		public GetModelSnapshotsWithSnapshotIdApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetModelSnapshots(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetModelSnapshotsAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetModelSnapshots(r),
			requestAsync: (client, r) => client.GetModelSnapshotsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> Fluent => f => f.SnapshotId(CallIsolatedValue);
		protected override GetModelSnapshotsDescriptor NewDescriptor() => new GetModelSnapshotsDescriptor(CallIsolatedValue);
		protected override GetModelSnapshotsRequest Initializer => new GetModelSnapshotsRequest(CallIsolatedValue, CallIsolatedValue);

		protected override void ExpectResponse(IGetModelSnapshotsResponse response)
		{
			// TODO: Implement
		}
	}
}
