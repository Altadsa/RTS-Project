using UnityEngine;

namespace RTS
{
    public interface IQueueable
    {
        string Name();
        Sprite Icon();
        float TimeNeeded();
        void OnComplete(Building productionBuilding);
        Vector3Int Cost();
        bool RequirementsMet();
    }
}
