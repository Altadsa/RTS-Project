using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class AiSelectionController : SelectionController
    {
        Building _selectedBuilding;

        private void Start()
        {
            StartCoroutine(AiState());
        }

        IEnumerator AiState()
        {
            yield return new WaitForSeconds(5);
            SelectableUnits.ForEach(u => u.GetComponent<Unit>().Select());
        }

    } 
}
