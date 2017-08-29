using System;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit.Abstractions;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobApiTests : ApiIntegrationTestBase<XPackMachineLearningCluster, IPutJobResponse, IPutJobRequest, PutJobDescriptor<MachineLearningSeeder.Metric>, PutJobRequest>
	{
		public PutJobApiTests(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutJob(r),
			requestAsync: (client, r) => client.PutJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override PutJobDescriptor<MachineLearningSeeder.Metric> NewDescriptor() =>
			new PutJobDescriptor<MachineLearningSeeder.Metric>(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<PutJobDescriptor<MachineLearningSeeder.Metric>, IPutJobRequest> Fluent => f => f
			.Description("Lab 1 - Simple example - Fluent Syntax")
			.ResultsIndexName("server-metrics")
			.AnalysisConfig(a => a
				.BucketSpan(new Time("30m"))
				.Latency("0s")
				.Detectors(d => d.Sum(c => c.FieldName(r => r.Total))
				)
			)
			.DataDescription(d => d.TimeField(r => r.Timestamp));

		protected override PutJobRequest Initializer =>
			new PutJobRequest(CallIsolatedValue)
			{
				Description = "Lab 1 - Simple example - Object Syntax",
				ResultsIndexName = "server-metrics",
				AnalysisConfig = new AnalysisConfig
				{
					BucketSpan = new Time("30m"),
					Latency = "0s",
					Detectors = new []
					{
						new SumDetector
						{
							FieldName = "total"
						}
					}
				},
				DataDescription = new DataDescription
				{
					TimeField = "timeStamp"
				}
			};

		protected override void ExpectResponse(IPutJobResponse response)
		{
			response.ShouldBeValid();
		}
	}
}
