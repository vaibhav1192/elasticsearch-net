using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	public class DeleteFilterApiTests : MachineLearningIntegrationTestBase<IDeleteFilterResponse, IDeleteFilterRequest, DeleteFilterDescriptor, DeleteFilterRequest>
	{
		public DeleteFilterApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putfilterResponse = client.PutFilter(callUniqueValue.Value, f => f
					.Type("filter_type")
					.Items("item_1", "item_2"));

				if (!putfilterResponse.IsValid)
					throw new Exception("Problem setting up PutFilter for integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteFilter(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteFilterAsync(CallIsolatedValue, f),
			request: (client, r) => client.DeleteFilter(r),
			requestAsync: (client, r) => client.DeleteFilterAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"_xpack/ml/filters/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => true;
		protected override DeleteFilterDescriptor NewDescriptor() => new DeleteFilterDescriptor(CallIsolatedValue);
		protected override object ExpectJson => null;
		protected override Func<DeleteFilterDescriptor, IDeleteFilterRequest> Fluent => f => f;
		protected override DeleteFilterRequest Initializer => new DeleteFilterRequest(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteFilterResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
