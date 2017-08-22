using Newtonsoft.Json;

namespace Nest
{
	public interface IPostJobDataResponse : IResponse
	{
	}

	public class PostJobDataResponse : ResponseBase, IPostJobDataResponse
	{
	}
}
