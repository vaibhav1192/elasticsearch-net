using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetFilters
{
	public class GetFiltersApiTests : ApiIntegrationTestBase<XPackCluster, IGetFiltersResponse, IGetFiltersRequest, GetFiltersDescriptor, GetFiltersRequest>
	{
		public GetFiltersApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a filter
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
			// TODO: Implement
		}
	}

	public class GetFiltersWithFilterIdApiTests : ApiIntegrationTestBase<XPackCluster, IGetFiltersResponse, IGetFiltersRequest, GetFiltersDescriptor, GetFiltersRequest>
	{
		public GetFiltersWithFilterIdApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: create a filter
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

		protected override Func<GetFiltersDescriptor, IGetFiltersRequest> Fluent => f => f
			.FilterId(CallIsolatedValue);

		protected override GetFiltersRequest Initializer => new GetFiltersRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetFiltersResponse response)
		{
			// TODO: Implement
		}
	}
}
