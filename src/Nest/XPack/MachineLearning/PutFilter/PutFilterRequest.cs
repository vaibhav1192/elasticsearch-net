using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Creates a filter for a Machine Learning Job
	/// </summary>
	public partial interface IPutFilterRequest
	{
		[JsonProperty("type")]
		string Type { get; set; }

		[JsonProperty("items")]
		IEnumerable<string> Items { get; set; }
	}

	/// <inheritdoc />
	public partial class PutFilterRequest
	{
		/// <inheritdoc />
		public string Type { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Items { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlPutFilter")]
	public partial class PutFilterDescriptor
	{
		string IPutFilterRequest.Type { get; set; }
		IEnumerable<string> IPutFilterRequest.Items { get; set; }

		/// <inheritdoc />
		public PutFilterDescriptor Type(string type) => Assign(a => a.Type = type);

		/// <inheritdoc />
		public PutFilterDescriptor Items(IEnumerable<string> items) => Assign(a => a.Items = items);

		/// <inheritdoc />
		public PutFilterDescriptor Items(params string[] items) => Assign(a => a.Items = items);
	}
}
