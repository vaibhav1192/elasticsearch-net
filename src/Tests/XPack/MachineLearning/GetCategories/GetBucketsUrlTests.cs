using System;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetBuckets
{
	public class GetBucketsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/anomaly_detectors/job_id/results/buckets")
				.Fluent(c => c.GetBuckets("job_id"))
				.Request(c => c.GetBuckets(new GetBucketsRequest("job_id")))
				.FluentAsync(c => c.GetBucketsAsync("job_id"))
				.RequestAsync(c => c.GetBucketsAsync(new GetBucketsRequest("job_id")))
				;

			await POST("/_xpack/ml/anomaly_detectors/job_id/results/buckets/1502200800")
				.Fluent(c => c.GetBuckets("job_id", r => r.Timestamp(1502200800)))
				.Request(c => c.GetBuckets(new GetBucketsRequest("job_id", 1502200800)))
				.FluentAsync(c => c.GetBucketsAsync("job_id", r => r.Timestamp(1502200800)))
				.RequestAsync(c => c.GetBucketsAsync(new GetBucketsRequest("job_id", 1502200800)))
				;

			await POST("/_xpack/ml/anomaly_detectors/job_id/results/buckets/1502200800")
				.Fluent(c => c.GetBuckets("job_id", r => r.Timestamp(new DateTime(2017, 08, 09))))
				.Request(c => c.GetBuckets(new GetBucketsRequest("job_id", new DateTime(2017, 08, 09))))
				.FluentAsync(c => c.GetBucketsAsync("job_id", r => r.Timestamp(new DateTime(2017, 08, 09))))
				.RequestAsync(c => c.GetBucketsAsync(new GetBucketsRequest("job_id", new DateTime(2017, 08, 09))))
				;

			await POST("/_xpack/ml/anomaly_detectors/job_id/results/buckets/1502200800")
				.Fluent(c => c.GetBuckets("job_id", r => r.Timestamp(new DateTimeOffset(2017, 08, 09, 0, 0, 0, TimeSpan.Zero))))
				.Request(c => c.GetBuckets(new GetBucketsRequest("job_id", new DateTimeOffset(2017, 08, 09, 0, 0, 0, TimeSpan.Zero))))
				.FluentAsync(c => c.GetBucketsAsync("job_id", r => r.Timestamp(new DateTimeOffset(2017, 08, 09, 0, 0, 0, TimeSpan.Zero))))
				.RequestAsync(c => c.GetBucketsAsync(new GetBucketsRequest("job_id", new DateTimeOffset(2017, 08, 09, 0, 0, 0, TimeSpan.Zero))))
				;
		}
	}
}
