using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace RTS
{
    public class BuildingMenu : MonoBehaviour
    {
        private GameObject _buildingPrefab;

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
                buildingInstance.transform.position = hit.point;
            }
        }

        private void InstantiateBuildingIfExists()
        {
            if (buildingInstance || !_buildingPrefab) return;
            buildingInstance = Instantiate(_buildingPrefab);
            buildingInstance.transform.parent = GameObject.FindGameObjectWithTag("Active Buildings").transform;
        }

        public void ConstructBuilding(GameObject buildingPrefab)
        {
            if (_buildingPrefab) return;
            _buildingPrefab = buildingPrefab;
            isBuildingMoving = true;
        }

        private void DeselectBuilding()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isBuildingMoving = false;
                _buildingPrefab = null;
                buildingInstance = null;
            }
        }

    } 
}
