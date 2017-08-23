using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	// TODO! Consider IPostJobDataRequest<T>
	public partial interface IPostJobDataRequest
	{
		/// <summary>
		/// The job must have a state of open to receive and process the data.
		/// </summary>
		[JsonIgnore]
		IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc />
	public partial class PostJobDataRequest
	{
		/// <inheritdoc />
		IEnumerable<object> Data { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlPostData")]
	public partial class PostJobDataDescriptor
	{
		/// <inheritdoc />
		IEnumerable<object> IPostJobDataRequest.Data { get; set; }

		/// <inheritdoc />
		public PostJobDataDescriptor Data(IEnumerable<object> data) => Assign(a => a.Data = data);
	}
}
