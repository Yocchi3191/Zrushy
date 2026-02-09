using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
	public interface IFiredEventLog
	{
		bool HasFired(EventID eventID);
		void Record(EventID eventID);
	}
}
