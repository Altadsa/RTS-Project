using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    Player1 = 1,
    Player2 = 2
}

public class PlayerInfo : MonoBehaviour
{
    PlayerID _playerID;

    public void SetPlayerInfo(PlayerID iD)
    {
        _playerID = iD;
    }

    public bool IsSamePlayer(PlayerID iD)
    {
        return _playerID == iD;
    }
}
