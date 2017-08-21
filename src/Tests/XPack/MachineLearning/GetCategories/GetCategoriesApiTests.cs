using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetCategories
{
	public class GetCategoriesApiTests : ApiIntegrationTestBase<XPackCluster, IGetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
	{
		public GetCategoriesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: run a job in order to get results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetCategories(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetCategoriesAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetCategories(r),
			requestAsync: (client, r) => client.GetCategoriesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/categories";

		protected override bool SupportsDeserialization => true;

		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<GetCategoriesDescriptor, IGetCategoriesRequest> Fluent => f => f;

		protected override GetCategoriesRequest Initializer =>
			new GetCategoriesRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetCategoriesResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetCategoriesWithCategoriesApiTests : ApiIntegrationTestBase<XPackCluster, IGetCategoriesResponse, IGetCategoriesRequest, GetCategoriesDescriptor, GetCategoriesRequest>
	{
		public GetCategoriesWithCategoriesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// TODO: run a job in order to get bucket results
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetCategories(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.GetCategoriesAsync(CallIsolatedValue, f),
			request: (client, r) => client.GetCategories(r),
			requestAsync: (client, r) => client.GetCategoriesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/results/categories/1";

		protected override bool SupportsDeserialization => true;

		protected override GetCategoriesDescriptor NewDescriptor() => new GetCategoriesDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<GetCategoriesDescriptor, IGetCategoriesRequest> Fluent => f => f
			.CategoryId(1);

		protected override GetCategoriesRequest Initializer => new GetCategoriesRequest(CallIsolatedValue, 1);

		protected override void ExpectResponse(IGetCategoriesResponse response)
		{
			// TODO: Implement
		}
	}
}
