using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class PostJobDataDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			method.Url.Params["reset_start"] = new DateTimeOffsetQueryParameters();
			method.Url.Params["reset_end"] = new DateTimeOffsetQueryParameters();
			return method;
		}

		private class DateTimeOffsetQueryParameters : ApiQueryParameters
		{
			public DateTimeOffsetQueryParameters()
			{
				this.Type = "DateTimeOffset";

				this.FluentGenerator = (queryStringParamName, mm, original, setter) =>
					$"public {queryStringParamName} {mm.ToPascalCase()}({CsharpType(mm)} {mm}) => this.AddQueryString(\"{original}\", {setter}.ToString(\"o\"));";


				this.Generator = (fieldType, mm, original, setter) =>
					$"public {fieldType} {mm} {{ get {{ return DateTimeOffset.Parse(Q<string>(\"{original}\")); }} set {{ Q(\"{original}\", {setter}.ToString(\"o\")); }} }}";
			}
		}
	}
}
