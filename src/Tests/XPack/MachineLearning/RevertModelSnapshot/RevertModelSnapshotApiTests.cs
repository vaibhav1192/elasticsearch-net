using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.RevertModelSnapshot
{
	public class RevertModelSnapshotApiTests : ApiIntegrationTestBase<XPackCluster, IRevertModelSnapshotResponse, IRevertModelSnapshotRequest, RevertModelSnapshotDescriptor, RevertModelSnapshotRequest>
	{
		public RevertModelSnapshotApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a job
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RevertModelSnapshot(CallIsolatedValue, CallIsolatedValue, f),
			fluentAsync: (client, f) => client.RevertModelSnapshotAsync(CallIsolatedValue, CallIsolatedValue, f),
			request: (client, r) => client.RevertModelSnapshot(r),
			requestAsync: (client, r) => client.RevertModelSnapshotAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_xpack/ml/anomaly_detectors/{CallIsolatedValue}/model_snapshots/{CallIsolatedValue}/_revert";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => new
		{
			delete_intervening_results = true
		};

		protected override Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> Fluent => f => f
			.DeleteInterveningResults();

		protected override RevertModelSnapshotRequest Initializer => new RevertModelSnapshotRequest(CallIsolatedValue, CallIsolatedValue)
		{
			DeleteInterveningResults = true
		};

		protected override void ExpectResponse(IRevertModelSnapshotResponse response)
		{
			response.Acknowledged.Should().BeTrue();
			response.Model.Should().NotBeNull();
		}
	}
}
