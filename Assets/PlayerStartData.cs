using UnityEngine;

namespace RTS
{
    [CreateAssetMenu (menuName = "RTS/Setup/Start Data")]
    public class PlayerStartData : ScriptableObject
    {
        [SerializeField] GameObject _headquarters;
        [SerializeField] GameObject _workerUnit;

        public void SetupPlayerStart(PlayerStart player, PlayerID iD)
        {
            GameObject hq = Instantiate(_headquarters);
            hq.transform.position = player.transform.position;
            hq.AddComponent<PlayerInfo>().SetPlayerInfo(iD);
            GameObject unit = Instantiate(_workerUnit);
            unit.transform.position = new Vector3(player.transform.position.x + 20, player.transform.position.y, player.transform.position.z);
            unit.AddComponent<PlayerInfo>().SetPlayerInfo(iD);
            //FindObjectOfType<PlayerManager>().transform.position = player.transform.position;
        }
    } 
}
