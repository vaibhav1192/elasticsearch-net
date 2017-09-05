using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.GetDatafeeds
{
	public class GetDatafeedsApiTests : MachineLearningIntegrationTestBase<IGetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeeds(f),
			fluentAsync: (client, f) => client.GetDatafeedsAsync(f),
			request: (client, r) => client.GetDatafeeds(r),
			requestAsync: (client, r) => client.GetDatafeedsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/datafeeds";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f;

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			// TODO: Implement
		}
	}

	public class GetDatafeedsWithDatafeedIdApiTests : MachineLearningIntegrationTestBase<IGetDatafeedsResponse, IGetDatafeedsRequest, GetDatafeedsDescriptor, GetDatafeedsRequest>
	{
		public GetDatafeedsWithDatafeedIdApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeeds(f),
			fluentAsync: (client, f) => client.GetDatafeedsAsync(f),
			request: (client, r) => client.GetDatafeeds(r),
			requestAsync: (client, r) => client.GetDatafeedsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue);
		protected override GetDatafeedsRequest Initializer => new GetDatafeedsRequest(CallIsolatedValue);

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			// TODO: Implement
		}
	}
}
