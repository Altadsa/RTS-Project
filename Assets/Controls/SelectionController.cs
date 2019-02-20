using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public abstract class SelectionController : MonoBehaviour
    {
        public PlayerInformation Player { get; private set; }

        public List<GameObject> SelectedUnits { get; private set; } = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> SelectableUnits = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> SelectableBuildings = new List<GameObject>();

        public delegate void OnUpdateSelectedUnits(List<GameObject> selectedUnits);
        public event OnUpdateSelectedUnits updateSelectedUnits;

        public void SetPlayer(PlayerInformation player)
        {
            if (Player == null)
                Player = player;
        }

        protected void UpdateSelectedUnits()
        {
            updateSelectedUnits?.Invoke(SelectedUnits);
        }
    }
}
