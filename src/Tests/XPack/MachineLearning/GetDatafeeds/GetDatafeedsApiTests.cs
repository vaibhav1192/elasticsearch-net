using System;
using System.Linq;
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
				PutDatafeed(client, callUniqueValue.Value);
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
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);

			response.Datafeeds.First().DatafeedId.Should().NotBeNullOrWhiteSpace();
			response.Datafeeds.First().JobId.Should().NotBeNullOrWhiteSpace();

			response.Datafeeds.First().QueryDelay.Should().NotBeNull("QueryDelay");
			response.Datafeeds.First().QueryDelay.Should().Be(new Time("1m"));

//			Indices are not deserialising...
//			response.Datafeeds.First().Indices.Should().NotBeNull("Indices");
//			response.Datafeeds.First().Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			response.Datafeeds.First().Types.Should().NotBeNull("Types");
			response.Datafeeds.First().Types.Should().Be(Types.Parse("metric"));

			response.Datafeeds.First().ScrollSize.Should().Be(1000);

			response.Datafeeds.First().ChunkingConfig.Should().NotBeNull();
			response.Datafeeds.First().ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			response.Datafeeds.First().Query.Should().NotBeNull();

			response.ShouldBeValid();
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
				PutDatafeed(client, callUniqueValue.Value);
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
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");
		protected override GetDatafeedsRequest Initializer => new GetDatafeedsRequest(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IGetDatafeedsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);

			response.Datafeeds.First().DatafeedId.Should().NotBeNullOrWhiteSpace();
			response.Datafeeds.First().JobId.Should().NotBeNullOrWhiteSpace();

			response.Datafeeds.First().QueryDelay.Should().NotBeNull("QueryDelay");
			response.Datafeeds.First().QueryDelay.Should().Be(new Time("1m"));

//			Indices are not deserialising...
//			response.Datafeeds.First().Indices.Should().NotBeNull("Indices");
//			response.Datafeeds.First().Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			response.Datafeeds.First().Types.Should().NotBeNull("Types");
			response.Datafeeds.First().Types.Should().Be(Types.Parse("metric"));

			response.Datafeeds.First().ScrollSize.Should().Be(1000);

			response.Datafeeds.First().ChunkingConfig.Should().NotBeNull();
			response.Datafeeds.First().ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			response.Datafeeds.First().Query.Should().NotBeNull();

			response.ShouldBeValid();
		}
	}
}
