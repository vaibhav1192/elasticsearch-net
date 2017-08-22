using Newtonsoft.Json;

namespace Nest
{
	public interface IPreviewDatafeedResponse : IResponse
	{
	}

	public class PreviewDatafeedResponse : ResponseBase, IPreviewDatafeedResponse
	{
	}
}
