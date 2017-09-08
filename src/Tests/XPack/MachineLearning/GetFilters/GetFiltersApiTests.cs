using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetFilters
{
	public class GetFiltersApiTests : MachineLearningIntegrationTestBase<IGetFiltersResponse, IGetFiltersRequest, GetFiltersDescriptor, GetFiltersRequest>
	{
		public GetFiltersApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putFilterResponse = client.PutFilter(callUniqueValue.Value, f => f
					.Type("filter_type")
					.Items("item_1", "item_2"));

				if (!putFilterResponse.IsValid)
					throw new Exception("Problem setting up PutFilter for integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetFilters(f),
			fluentAsync: (client, f) => client.GetFiltersAsync(f),
			request: (client, r) => client.GetFilters(r),
			requestAsync: (client, r) => client.GetFiltersAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/ml/filters/";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetFiltersDescriptor, IGetFiltersRequest> Fluent => f => f;

		protected override void ExpectResponse(IGetFiltersResponse response)
		{
			response.ShouldBeValid();
			response.Filters.Count.Should().BeGreaterOrEqualTo(1);
			response.Count.Should().BeGreaterOrEqualTo(1);
		}
	}

	public class GetFiltersWithFilterIdApiTests : MachineLearningIntegrationTestBase<IGetFiltersResponse, IGetFiltersRequest, GetFiltersDescriptor, GetFiltersRequest>
	{
		public GetFiltersWithFilterIdApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putFilterResponse = client.PutFilter(callUniqueValue.Value, f => f
					.Type("filter_type")
					.Items("item_1", "item_2"));

				if (!putFilterResponse.IsValid)
					throw new Exception("Problem setting up PutFilter for integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetFilters(f),
			fluentAsync: (client, f) => client.GetFiltersAsync(f),
			request: (client, r) => client.GetFilters(r),
			requestAsync: (client, r) => client.GetFiltersAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/filters/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetFiltersDescriptor, IGetFiltersRequest> Fluent => f => f.FilterId(CallIsolatedValue);
		protected override GetFiltersRequest Initializer => new GetFiltersRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetFiltersResponse response)
		{
			response.ShouldBeValid();
			response.Filters.Count.Should().BeGreaterOrEqualTo(1);
			response.Filters.Count.Should().Be(1);
		}
	}
}
