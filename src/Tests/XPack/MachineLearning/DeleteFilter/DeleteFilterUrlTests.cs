using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	public class DeleteFilterUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_xpack/ml/filters/filter_id")
					.Fluent(c => c.DeleteFilter("filter_id"))
					.Request(c => c.DeleteFilter(new DeleteFilterRequest("filter_id")))
					.FluentAsync(c => c.DeleteFilterAsync("filter_id"))
					.RequestAsync(c => c.DeleteFilterAsync(new DeleteFilterRequest("filter_id")))
				;
		}
	}
}
