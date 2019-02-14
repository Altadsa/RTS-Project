using UnityEngine;

namespace RTS
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] PlayerStartData _startData;
        [SerializeField] PlayerStart[] _playerStarts;

        PlayerID[] playerIDs = new PlayerID[]
        {
            PlayerID.Player1,
            PlayerID.Player2
        };

        private void Start()
        {
            for (int i = 0; i < playerIDs.Length; i++)
            {
                if (_playerStarts[i] != null)
                {
                    _playerStarts[i].SetupPlayerStart(_startData, playerIDs[i]);
                }
                else return;
            }
        }
    } 
}
