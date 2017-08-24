namespace Nest
{
	/// <summary>
	/// A request to retrieve filters for Machine Learning jobs.
	/// </summary>
	public partial interface IGetFiltersRequest {}

	/// <inheritdoc />
	public partial class GetFiltersRequest {}

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetFilters")]
	public partial class GetFiltersDescriptor {}
}
