using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning
{
	[SkipVersion("<5.4.0", "Machine Learning does not exist in previous versions")]
	public abstract class MachineLearningIntegrationTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		: ApiIntegrationTestBase<XPackMachineLearningCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected MachineLearningIntegrationTestBase(ClusterBase cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected IPutJobResponse PutJob(IElasticClient client, string jobId)
		{
			var putJobResponse = client.PutJob<Metric>(jobId, f => f
				.Description("Lab 1 - Simple example")
				.AnalysisConfig(a => a
					.BucketSpan("30m")
					.Latency("0s")
					.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
				)
				.DataDescription(d => d.TimeField(r => r.Timestamp))
			);

			if (!putJobResponse.IsValid)
				throw new Exception($"Problem putting job {jobId} for integration test");

			return putJobResponse;
		}

		protected IOpenJobResponse OpenJob(IElasticClient client, string jobId)
		{
			var openJobResponse = client.OpenJob(jobId);
			if (!openJobResponse.IsValid || openJobResponse.Opened == false)
				throw new Exception($"Problem opening job {jobId} for integration test");
			return openJobResponse;
		}

		protected ICloseJobResponse CloseJob(IElasticClient client, string jobId)
		{
			var closeJobResponse = client.CloseJob(jobId);
			if (!closeJobResponse.IsValid || closeJobResponse.Closed == false)
				throw new Exception($"Problem closing job {jobId} for integration test");
			return closeJobResponse;
		}

		protected IDeleteJobResponse DeleteJob(IElasticClient client, string jobId)
		{
			var deleteJobResponse = client.DeleteJob(jobId);
			if (!deleteJobResponse.IsValid || !deleteJobResponse.Acknowledged)
				throw new Exception($"Problem deleting job {jobId} for integration test");
			return deleteJobResponse;
		}

		protected IPutDatafeedResponse PutDatafeed(IElasticClient client, string jobId)
		{
			var putDataFeedResponse = client.PutDatafeed<Metric>(jobId + "-datafeed", f => f
				.Query(q => q.MatchAll()));

			if (!putDataFeedResponse.IsValid)
				throw new Exception($"Problem putting datafeed for job {jobId} for integration test");

			return putDataFeedResponse;
		}

		protected IStartDatafeedResponse StartDatafeed(IElasticClient client, string jobId)
		{
			var startDatafeedResponse = client.StartDatafeed(jobId + "-datafeed", f => f);
			if (!startDatafeedResponse.IsValid || startDatafeedResponse.Started == false)
				throw new Exception($"Problem starting datafeed for job {jobId} for integration test");
			return startDatafeedResponse;
		}

		protected void IndexSnapshot(IElasticClient client, string jobId, string snapshotId)
		{
			var index = ".ml-anomalies-" + jobId;
			client.LowLevel.Index<object>(index, "doc", (object)new
			{
				job_id = jobId,
				snapshot_id = snapshotId,
				timestamp = "2016-06-02T00:00:00Z",
				snapshot_doc_count = 1
			});

			client.Refresh(index);
		}

		protected void IndexAnomalyRecord(IElasticClient client, string jobId, DateTimeOffset timestamp)
		{
			var index = ".ml-anomalies-" + jobId;
			client.LowLevel.Index<object>(index, "doc", (object)new
			{
				job_id = jobId,
				result_type = "record",
				timestamp = timestamp.ToString("o"),
				record_score = 80.0,
				bucket_span = 1,
				is_interim = true
			});

			client.Refresh(index);
		}

		protected void IndexBucket(IElasticClient client, string jobId, DateTimeOffset timestamp)
		{
			var index = ".ml-anomalies-" + jobId;
			client.LowLevel.Index<object>(index, "doc", (object)new
			{
				job_id = jobId,
				result_type = "bucket",
				timestamp = timestamp.ToString("o"),
				anomaly_score = 90.0,
				bucket_span = 1,
				is_interim = true
			});

			client.Refresh(index);
		}

		protected void IndexCategory(IElasticClient client, string jobId)
		{
			var index = ".ml-anomalies-" + jobId;
			client.LowLevel.Index<object>(index, "doc", (object)new
			{
				job_id = jobId,
				category_id = "1"
			});

			client.Refresh(index);
		}

		protected void IndexInfluencer(IElasticClient client, string jobId, DateTimeOffset timestamp)
		{
			var index = ".ml-anomalies-" + jobId;
			client.LowLevel.Index<object>(index, "doc", (object)new
			{
				job_id = jobId,
				timestamp = timestamp.ToString("o"),
				influencer_field_name = "foo",
				influencer_field_value = "bar",
				influencer_score = 50,
				result_type = "influencer",
				bucket_span = 1
			});

			client.Refresh(index);
		}
	}
}
