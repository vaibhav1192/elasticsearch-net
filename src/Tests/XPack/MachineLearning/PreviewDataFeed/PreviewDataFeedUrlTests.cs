using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PreviewDatafeed
{
	public class PreviewDatafeedUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/ml/datafeeds/datafeed_id/_preview")
				.Fluent(c => c.PreviewDatafeed("datafeed_id", p => p))
				.Request(c => c.PreviewDatafeed(new PreviewDatafeedRequest("datafeed_id")))
				.FluentAsync(c => c.PreviewDatafeedAsync("datafeed_id", p => p))
				.RequestAsync(c => c.PreviewDatafeedAsync(new PreviewDatafeedRequest("datafeed_id")))
				;
		}
	}
}
