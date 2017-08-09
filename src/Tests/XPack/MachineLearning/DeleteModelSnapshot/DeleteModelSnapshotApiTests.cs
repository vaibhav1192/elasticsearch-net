using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteModelSnapshot
{
	public class DeleteModelSnapshotApiTests : ApiIntegrationTestBase<XPackCluster, IDeleteModelSnapshotResponse, IDeleteModelSnapshotRequest, DeleteModelSnapshotDescriptor, DeleteModelSnapshotRequest>
	{
		public DeleteModelSnapshotApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a model snapshot, to allow it to be deleted

		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteModelSnapshot(CallIsolatedValue, CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteModelSnapshotAsync(CallIsolatedValue, CallIsolatedValue, f),
			request: (client, r) => client.DeleteModelSnapshot(r),
			requestAsync: (client, r) => client.DeleteModelSnapshotAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override DeleteModelSnapshotDescriptor NewDescriptor() =>
			new DeleteModelSnapshotDescriptor(CallIsolatedValue, CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> Fluent => f => f;

		protected override DeleteModelSnapshotRequest Initializer =>
			new DeleteModelSnapshotRequest(CallIsolatedValue, CallIsolatedValue);

		protected override void ExpectResponse(IDeleteModelSnapshotResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
