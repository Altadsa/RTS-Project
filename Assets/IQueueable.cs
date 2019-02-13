using UnityEngine;

namespace RTS
{
    public interface IQueueable
    {
        float Time { get; }
        void OnProductionComplete(Building productionBuilding);
    }
}
