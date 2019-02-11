using UnityEngine;

namespace RTS
{
    public interface IQueueable
    {
        void OnComplete(Building productionBuilding);
        bool RequirementsMet();
    }
}
