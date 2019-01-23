using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class CameraControls : MonoBehaviour
    {
        float xThrow, zThrow, yZoom;
        Vector3 scroll = new Vector3(0,1,-1);
        Vector3 middleClickPos;
        float minZoom = 5, maxZoom = 20;

        const float ROTATION = 90;

        void Update()
        {
            ControlCamera();
        }

        private void ControlCamera()
        {
            MoveCamera();
            RotateCamera();
            ZoomCamera();
        }

        private void MoveCamera()
        {
            transform.position += GetMovementFromInput();
        }

        private void RotateCamera()
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, ROTATION * Time.deltaTime, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -ROTATION * Time.deltaTime, 0, Space.World);
            }
            if (Input.GetMouseButtonDown(2)) { middleClickPos = Input.mousePosition; }
            if (Input.GetMouseButton(2))
            {
                float positionDifference = Input.mousePosition.x - middleClickPos.x;
                float yRotation = Mathf.Clamp(positionDifference, -ROTATION, ROTATION) * Time.deltaTime;
                transform.Rotate(0, yRotation, 0);

            }
        }

        Vector3 GetMovementFromInput()
        {
            xThrow = Input.GetAxis("Horizontal");
            zThrow = Input.GetAxis("Vertical");
            Vector3 forwardMove = transform.forward * zThrow;
            Vector3 sideMove = transform.right * xThrow;
            return (forwardMove + sideMove);
        }

        private void ZoomCamera()
        {
            yZoom = Input.mouseScrollDelta.y;
            float yPos = transform.position.y;
            bool isZoomingIn = yZoom > Mathf.Epsilon;
            if (isZoomingIn && !(yPos <= minZoom))
            {
                AddZoomVectorToCamTransform();
            }
            if (!isZoomingIn && !(yPos >= maxZoom))
            {
                AddZoomVectorToCamTransform();
            }
        }

        private void AddZoomVectorToCamTransform()
        {
            Vector3 scaledZoom = yZoom * (Vector3.Scale(Camera.main.transform.forward, Vector3.one).normalized);
            transform.position += scaledZoom;
        }
    } 
}
