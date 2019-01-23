using UnityEngine;

namespace RTS
{
    public class MouseCursor : MonoBehaviour
    {
        public Texture2D cursorTexture;
        public Texture2D attackTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = new Vector2(-96, 96);

        private void Start()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        Layer currentLayerHit;
        public Layer[] layers =
        {
            Layer.Walkable,
            Layer.Attackables
        };

        private void Update()
        {
            RaycastToScreen();
        }

        RaycastHit hit;
        private void RaycastToScreen()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            foreach (Layer layer in layers)
            {
                int mask = 1 << (int)layer;
                if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, mask))
                {
                    if (currentLayerHit != layer)
                    {
                        currentLayerHit = layer;
                        SetMouseCursor(currentLayerHit);
                    }
                }
            }
        }

        private void SetMouseCursor(Layer layer)
        {
            switch (layer)
            {
                case Layer.Attackables:
                    Cursor.SetCursor(attackTexture, hotSpot, cursorMode);
                    break;
                case Layer.Walkable:
                    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                    break;
                case Layer.Units:
                    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                    break;
                default:
                    Debug.LogError("I don't know what I'm raycasting to....");
                    break;
            }
        }
    }
}
