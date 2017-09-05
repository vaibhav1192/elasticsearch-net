using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning.UpdateDatafeed
{
	public class UpdateDatafeedApiTests : MachineLearningIntegrationTestBase<IUpdateDatafeedResponse,
		IUpdateDatafeedRequest, UpdateDatafeedDescriptor<Project>, UpdateDatafeedRequest>
	{
		public UpdateDatafeedApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateDatafeed(CallIsolatedValue + "-datafeed", f),
			fluentAsync: (client, f) => client.UpdateDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			request: (client, r) => client.UpdateDatafeed(r),
			requestAsync: (client, r) => client.UpdateDatafeedAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed/_update";
		protected override bool SupportsDeserialization => true;
		protected override UpdateDatafeedDescriptor<Project> NewDescriptor() => new UpdateDatafeedDescriptor<Project>(CallIsolatedValue);

		protected override object ExpectJson => new
		{
			indices = "server-metrics",
			job_id = CallIsolatedValue,
			query = new
			{
				match_all = new
				{
					boost = 2.0
				}
			},
			types = new [] { "metric" }
		};

		protected override Func<UpdateDatafeedDescriptor<Project>, IUpdateDatafeedRequest> Fluent => f => f
			.JobId(CallIsolatedValue)
			.Indices(Nest.Indices.Parse("server-metrics"))
			.Types(Types.Parse("metric"))
			.Query(q => q.MatchAll(m => m.Boost(1)))
			;

		protected override UpdateDatafeedRequest Initializer =>
			new UpdateDatafeedRequest(CallIsolatedValue + "-datafeed")
			{
				JobId = CallIsolatedValue,
				Indices = Nest.Indices.Parse("server-metrics"),
				Types = Types.Parse("metric"),
				Query = new QueryContainer(new MatchAllQuery
				{
					Boost = 2
				})
			};

		protected override void ExpectResponse(IUpdateDatafeedResponse response)
		{
			response.ShouldBeValid();

			response.DatafeedId.Should().NotBeNullOrWhiteSpace();
			response.JobId.Should().Be(CallIsolatedValue);

			response.QueryDelay.Should().NotBeNull("QueryDelay");
			response.QueryDelay.Should().Be(new Time("1m"));

//			Indices are not deserialising...
//			response.Indices.Should().NotBeNull("Indices");
//			response.Indices.Should().Be(Nest.Indices.Parse("server-metrics"));

			response.Types.Should().NotBeNull("Types");
			response.Types.Should().Be(Types.Parse("metric"));

			response.ScrollSize.Should().Be(1000);

			response.ChunkingConfig.Should().NotBeNull();
			response.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);

			response.Query.Should().NotBeNull();

			response.ShouldBeValid();
		}
	}
}
