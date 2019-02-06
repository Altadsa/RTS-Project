using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace RTS
{
    public class BuildingMenu : MonoBehaviour
    {
        private BuildingBuildData _buildData;

        bool isBuildingMoving = false;

        GameObject buildingInstance;

        private void LateUpdate()
        {
            SetupBuilding();
        }

        private void Update()
        {
         
            if (isBuildingMoving)
            {
                DeselectBuilding();                   
                InstantiateBuildingIfExists();
                UndoBuildingConstruction();
            }
        }

        private void SetupBuilding()
        {
            if (isBuildingMoving)
            {
                MoveBuildingToMousePosition();
            }
        }

        private void MoveBuildingToMousePosition()
        {
            RaycastHit hit;
            int layerMask = 1 << (int)Layer.Walkable;
            bool hasHitTerrain = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask);
            if (hasHitTerrain)
            {
                buildingInstance.transform.position = SnapToPosition(hit.point);
            }
        }

        private void InstantiateBuildingIfExists()
        {
            if (buildingInstance || !_buildData) return;
            buildingInstance = Instantiate(_buildData.ConstructionPrefab);
            buildingInstance.GetComponent<ConstructionBuilding>().Setup(_buildData.BuildingPrefab);
            Transform parent = GameObject.FindGameObjectWithTag("Active Buildings").transform;
            buildingInstance.transform.SetParent(parent, false);
        }

        public void ConstructBuilding(BuildingBuildData buildData)
        {
            if (_buildData) return;
            _buildData = buildData;
            isBuildingMoving = true;
        }

        private void DeselectBuilding()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isBuildingMoving = false;
                _buildData = null;
                buildingInstance = null;
            }
        }

        private void UndoBuildingConstruction()
        {
            if (Input.GetMouseButtonDown(1))
            {
                isBuildingMoving = false;
                Destroy(buildingInstance);
                _buildData = null;
            }
        }

        private Vector3 SnapToPosition(Vector3 pos)
        {
            float pX = Mathf.Floor(pos.x);
            //float pY = Mathf.Floor(pos.y);
            float pZ = Mathf.Floor(pos.z);
            return new Vector3(pX, pos.y, pZ);
        }

    } 
}
