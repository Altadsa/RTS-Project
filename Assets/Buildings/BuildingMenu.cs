using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

namespace RTS
{
    public class BuildingMenu : MonoBehaviour
    {
        private BuildingBuildData _buildData;
        private PlayerInformation _playerToBuildFor;

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
            if (!buildingInstance) return;
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
            SetupBuildingInstance();
            UnitInputController.Instance.enabled = false;
        }

        private void SetupBuildingInstance()
        {
            buildingInstance = Instantiate(_buildData.ConstructionPrefab);
            buildingInstance.GetComponent<ConstructionBuilding>().Setup(_buildData.BuildingPrefab);
            buildingInstance.AddComponent<Player>()._player = _playerToBuildFor;
            Transform parent = GameObject.FindGameObjectWithTag("Active Buildings").transform;
            buildingInstance.transform.SetParent(parent, true);
        }

        public void ConstructBuilding(PlayerInformation player, BuildingBuildData buildData)
        {
            if (_buildData) return;
            _buildData = buildData;
            _playerToBuildFor = player;
            isBuildingMoving = true;
        }

        private void DeselectBuilding()
        {
            if (!buildingInstance) return;
            if (!CanPlaceBuilding()) return;
            if (Input.GetMouseButtonDown(0))
            {
                isBuildingMoving = false;
                _buildData = null;
                _playerToBuildFor = null;
                buildingInstance = null;
                UnitInputController.Instance.enabled = true;
            }
        }

        private void UndoBuildingConstruction()
        {
            if (Input.GetMouseButtonDown(1))
            {
                isBuildingMoving = false;
                Destroy(buildingInstance);
                _buildData = null;
                UnitInputController.Instance.enabled = true;
            }
        }

        private Vector3 SnapToPosition(Vector3 pos)
        {
            float pX = Mathf.Floor(pos.x);
            //float pY = Mathf.Floor(pos.y);
            float pZ = Mathf.Floor(pos.z);
            return new Vector3(pX, pos.y, pZ);
        }

        private bool CanPlaceBuilding()
        { 
            var verts = buildingInstance.GetComponent<MeshFilter>().mesh.vertices;
            var obstactles = FindObjectsOfType<NavMeshObstacle>();
            var cols = new List<Collider>();
            foreach (var o in obstactles)
            {
                if (o.gameObject != buildingInstance.gameObject)
                    cols.Add(o.GetComponent<Collider>());
            }

            foreach (var vert in verts)
            {
                NavMeshHit hit;
                Vector3 worldPos = buildingInstance.transform.TransformPoint(vert);
                NavMesh.SamplePosition(worldPos, out hit, 20, NavMesh.AllAreas);

                bool onXAxis = Mathf.Abs(hit.position.x - worldPos.x) < 0.5f;
                bool onZAxis = Mathf.Abs(hit.position.z - worldPos.z) < 0.5f;

                bool hitCollider = cols.Any(c => c.bounds.Contains(worldPos));

                if (hitCollider)
                {
                    Debug.Log("Can't place that there...");
                    return false;
                }
            }
            return true;
        }

    } 
}
