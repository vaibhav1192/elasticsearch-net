using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// </summary>
	public partial interface IValidateJobRequest
	{

	}

	/// <inheritdoc />
	public partial class ValidateJobRequest
	{
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlValidate")]
	public partial class ValidateJobDescriptor<T>
	{
	}
}
