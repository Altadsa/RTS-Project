using UnityEngine;

namespace RTS
{
    public class UnitSelectionBox : MonoBehaviour
    {
        GameObject selectionBox;
        RectTransform sbRectTransform;

        Vector3 boxStart, boxEnd;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            selectionBox = GameObject.FindGameObjectWithTag("Selection Box");
            if (selectionBox)
            {
                sbRectTransform = selectionBox.GetComponent<RectTransform>();
                selectionBox.SetActive(false);
            }
        }

        RaycastHit hit;
        void Update()
        {
            DrawSelectionBox();
        }

        private void DrawSelectionBox()
        {
            if (UiRaycast.RaycastToUi) return;
            if (Input.GetMouseButtonDown(0))
            {
                bool hasHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
                if (hasHit)
                {
                    boxStart = hit.point;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                selectionBox.SetActive(false);
            }

            if (Input.GetMouseButton(0))
            {
                if (!selectionBox.activeInHierarchy)
                {
                    selectionBox.SetActive(true);
                }
                boxEnd = Input.mousePosition;

                Vector3 rectStart = Camera.main.WorldToScreenPoint(boxStart);
                Vector3 centre = (rectStart + boxEnd) / 2;
                rectStart.z = 0;
                float sizeX = Mathf.Abs(rectStart.x - boxEnd.x);
                float sizeY = Mathf.Abs(rectStart.y - boxEnd.y);

                sbRectTransform.sizeDelta = new Vector2(sizeX, sizeY);
                sbRectTransform.position = centre;
            }
        }

    }

}