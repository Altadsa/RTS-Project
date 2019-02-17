using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class GameManager : MonoBehaviour
    {
        public List<PlayerInformation> _players = new List<PlayerInformation>();
        public PlayerStartData _defaultStartData;
        public static PlayerInformation Default = null;

        private void Awake()
        {
            foreach (var player in _players)
            {
                if (!player._isAi && Default == null) { Default = player; FindObjectOfType<SelectionController>()._player = player; }
                _defaultStartData.SetupPlayerStart(player);
            }
        }

    } 
}
