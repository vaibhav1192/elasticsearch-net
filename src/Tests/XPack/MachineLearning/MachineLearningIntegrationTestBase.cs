using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning
{
	public abstract class MachineLearningIntegrationTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		: ApiIntegrationTestBase<XPackMachineLearningCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		public MachineLearningIntegrationTestBase(XPackMachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected IPutJobResponse PutJob(IElasticClient client, string jobId)
		{
			var putJobResponse = client.PutJob<Metric>(jobId, f => f
				.Description("Lab 1 - Simple example")
				.ResultsIndexName(jobId + "-results")
				.AnalysisConfig(a => a
					.BucketSpan(new Time("30m"))
					.Latency("0s")
					.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
				)
				.DataDescription(d => d.TimeField(r => r.Timestamp))
			);

			if (!putJobResponse.IsValid)
				throw new Exception("Problem setting up PutJob for integration test");

			return putJobResponse;
		}

		protected IOpenJobResponse OpenJob(IElasticClient client, string jobId)
		{
			var openJobResponse = client.OpenJob(jobId, f => f);
			if (!openJobResponse.IsValid)
				throw new Exception("Problem setting up OpenJob for integration test");
			return openJobResponse;
		}

		protected IPutDatafeedResponse PutDatafeed(IElasticClient client, string jobId)
		{
			var putDataFeedResponse = client.PutDatafeed<Metric>(jobId + "-datafeed", f => f
				.JobId(jobId)
				.Indices(Nest.Indices.Parse("server-metrics"))
				.Types(Types.Parse("metric"))
				.Query(q => q.MatchAll(m => m.Boost(1))));

			if (!putDataFeedResponse.IsValid)
				throw new Exception("Problem setting up PutDatafeed for integration test");

			return putDataFeedResponse;
		}

		protected IStartDatafeedResponse StartDatafeed(IElasticClient client, string jobId)
		{
			var startDatafeedResponse = client.StartDatafeed(jobId + "-datafeed", f => f);

			if (!startDatafeedResponse.IsValid)
				throw new Exception("Problem setting up StartDatafeed for integration test");

			return startDatafeedResponse;
		}
	}
}
