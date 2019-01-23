using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class BuildingMenu : MonoBehaviour
    {

        public GameObject buildingPrefab;

        bool isBuildingMoving = false;

        GameObject buildingInstance;

        private void LateUpdate()
        {
            SetupBuilding();
        }

        private void SetupBuilding()
        {
            if (isBuildingMoving)
            {
                InstantiateBuildingIfExists();
                MoveBuildingToMousePosition();
                DeselectBuilding();
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
            if (!buildingInstance)
            {
                buildingInstance = Instantiate(buildingPrefab);
                buildingInstance.transform.parent = GameObject.FindGameObjectWithTag("World Objects").transform;
            }
        }

        public void ConstructBuilding()
        {
            isBuildingMoving = true;
        }

        private void DeselectBuilding()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isBuildingMoving = false;
                buildingInstance = null;
                return;
            }
        }

    } 
}
