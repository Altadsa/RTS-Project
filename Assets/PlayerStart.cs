using UnityEngine;

namespace RTS
{
    public class PlayerStart : MonoBehaviour
    {
        PlayerStartData _startData;
        PlayerID _playerID;

        public void SetupPlayerStart(PlayerStartData data, PlayerID iD)
        {
            _startData = data;
            _playerID = iD;
            _startData.SetupPlayerStart(this, _playerID);
            Debug.Log(string.Format("Setup Player: {0}", _playerID));
        }

    } 
}
