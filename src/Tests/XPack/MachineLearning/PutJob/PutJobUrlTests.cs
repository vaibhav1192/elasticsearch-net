using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Tests.XPack.MachineLearning.PutJob
{
	public class PutJobUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.PUT("/_xpack/ml/anomaly_detectors/job_id")
				.Fluent(c => c.PutJob<object>("job_id", p => p))
				.Request(c => c.PutJob(new PutJobRequest("job_id")))
				.FluentAsync(c => c.PutJobAsync<object>("job_id"))
				.RequestAsync(c => c.PutJobAsync(new PutJobRequest("job_id")))
				;
		}
	}
}
