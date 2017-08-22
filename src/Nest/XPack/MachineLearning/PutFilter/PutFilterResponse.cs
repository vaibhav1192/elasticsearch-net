namespace Nest
{
	public interface IPutFilterResponse : IAcknowledgedResponse {}

	public class PutFilterResponse : AcknowledgedResponseBase, IPutFilterResponse {}
}
