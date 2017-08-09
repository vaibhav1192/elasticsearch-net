using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteExpiredData
{
	public class DeleteExpiredDataApiTests : ApiIntegrationTestBase<XPackCluster, IDeleteExpiredDataResponse, IDeleteExpiredDataRequest, DeleteExpiredDataDescriptor, DeleteExpiredDataRequest>
	{
		public DeleteExpiredDataApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a datafed, to allow it to be deleted

		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteExpiredData(f),
			fluentAsync: (client, f) => client.DeleteExpiredDataAsync(f),
			request: (client, r) => client.DeleteExpiredData(r),
			requestAsync: (client, r) => client.DeleteExpiredDataAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override string UrlPath => $"_xpack/ml/_delete_expired_data";

		protected override bool SupportsDeserialization => true;

		protected override DeleteExpiredDataDescriptor NewDescriptor() => new DeleteExpiredDataDescriptor();

		protected override object ExpectJson => null;

		protected override Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> Fluent => f => f;

		protected override DeleteExpiredDataRequest Initializer => new DeleteExpiredDataRequest();

		protected override void ExpectResponse(IDeleteExpiredDataResponse response)
		{
			response.Deleted.Should().BeTrue();
		}
	}
}
