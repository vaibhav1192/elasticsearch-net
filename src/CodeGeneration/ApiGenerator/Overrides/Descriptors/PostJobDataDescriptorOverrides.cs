using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class PostJobDataDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new string[]
		{
			// TODO: figure out if needed
			//"reset_start",
			//"reset_end"
		};
	}
}
