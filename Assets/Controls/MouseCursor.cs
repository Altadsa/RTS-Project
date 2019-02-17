using UnityEngine;

namespace RTS
{
    public class MouseCursor : MonoBehaviour
    {
        public Texture2D cursorTexture;
        public Texture2D attackTexture;
        public Texture2D buildTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = new Vector2(-96, 96);

        SelectionController _selectionController;

        private void Start()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            FindObjectOfType<UnitRaycaster>().UpdateActiveLayer += SetMouseCursor;
            _selectionController = GetComponentInChildren<SelectionController>();
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
