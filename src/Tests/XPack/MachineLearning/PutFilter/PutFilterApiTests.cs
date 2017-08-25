using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.PutFilter
{
	public class PutFilterApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IPutFilterResponse, IPutFilterRequest, PutFilterDescriptor, PutFilterRequest>
	{
		public PutFilterApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutFilter(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutFilterAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutFilter(r),
			requestAsync: (client, r) => client.PutFilterAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"/_xpack/ml/filters/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override PutFilterDescriptor NewDescriptor() => new PutFilterDescriptor(CallIsolatedValue);

		protected override object ExpectJson => new
		{
			type = "filter_type",
			items = new[] { "item_1", "item_2" }
		};

		protected override Func<PutFilterDescriptor, IPutFilterRequest> Fluent => f => f
			.Type("filter_type")
			.Items("item_1", "item_2");

		protected override PutFilterRequest Initializer =>
			new PutFilterRequest(CallIsolatedValue)
			{
				Type = "filter_type",
				Items = new[] { "item_1", "item_2" }
			};

		protected override void ExpectResponse(IPutFilterResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
