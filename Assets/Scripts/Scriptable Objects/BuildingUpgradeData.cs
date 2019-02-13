using UnityEngine;
using System;

namespace RTS
{
    [CreateAssetMenu (menuName = "RTS/Producible/Building Upgrades")]
    public class BuildingUpgradeData : ProductionData, IQueueable, IRequireable
    {
        [SerializeField] Mesh _upgradedBuilding;
        [SerializeField] Material[] _buildingMaterials;
        [SerializeField] float _time;
        public float Time { get { return _time; } set { _time = value; } }

        public void OnProductionComplete(Building productionBuilding)
        {
            UpgradeBuilding(productionBuilding);
            OnItemComplete();
            Debug.Log(string.Format("{0} Upgrade Complete!", _title));
        }

        public void OnItemComplete()
        {
            UpgradeManager.CompleteUpgrade(this);
        }

        private void UpgradeBuilding(Building building)
        {
            MeshFilter filter = building.GetComponent<MeshFilter>();
            MeshCollider collider = building.GetComponent<MeshCollider>();
            MeshRenderer renderer = building.GetComponent<MeshRenderer>();
            filter.mesh = _upgradedBuilding;
            Array.Clear(renderer.materials, 0, renderer.materials.Length);
            for (int i = 0; i < _buildingMaterials.Length; i++)
            {
                renderer.materials[i] = _buildingMaterials[i];
            }
        }

    }
}
