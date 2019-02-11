using UnityEngine;
using System;

namespace RTS
{
    [CreateAssetMenu (menuName = "RTS/Upgrades/Building Upgrades")]
    public class BuildingUpgradeData : UpgradeData
    {

        [SerializeField] Mesh _upgradedBuilding;
        [SerializeField] Material[] _buildingMaterials;

        public override void OnComplete(Building productionBuilding)
        {
            UpgradeBuilding(productionBuilding);
            Debug.Log(string.Format("{0} Upgrade Complete!", _upgradeTitle));
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
