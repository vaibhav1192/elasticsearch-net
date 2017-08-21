using System;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetFilters
{
	public class GetFiltersUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/ml/filters")
				.Fluent(c => c.GetFilters())
				.Request(c => c.GetFilters(new GetFiltersRequest()))
				.FluentAsync(c => c.GetFiltersAsync())
				.RequestAsync(c => c.GetFiltersAsync(new GetFiltersRequest()))
				;

			await GET("/_xpack/ml/filters/filter_id")
				.Fluent(c => c.GetFilters(r => r.FilterId("filter_id")))
				.Request(c => c.GetFilters(new GetFiltersRequest("filter_id")))
				.FluentAsync(c => c.GetFiltersAsync(r => r.FilterId("filter_id")))
				.RequestAsync(c => c.GetFiltersAsync(new GetFiltersRequest("filter_id")))
				;
		}
	}
}
