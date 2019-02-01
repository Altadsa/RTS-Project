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
            FindObjectOfType<UnitRaycaster>().UpdateActiveLayer += SetMouseCursor;
        }

        private void SetMouseCursor(Layer layer, RaycastHit hit)
        {
            switch (layer)
            {
                case Layer.Attackables:
                    Cursor.SetCursor(attackTexture, hotSpot, cursorMode);
                    break;
                default:
                    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                    return;
            }
        }
    }
}
