using UnityEngine;

namespace RTS
{
    [CreateAssetMenu (menuName = "RTS/Setup/Start Data")]
    public class PlayerStartData : ScriptableObject
    {
        [SerializeField] GameObject _headquarters;
        [SerializeField] GameObject _workerUnit;

        public void SetupPlayerStart(PlayerInformation info)
        {
            GameObject hq = Instantiate(_headquarters);
            hq.transform.position = info.StartPos.position;
            hq.GetComponent<Building>().SetPlayer(info);
            Vector3 centreSpawn = info.StartPos.position + (20 * Vector3.forward);
            for (int i = 0; i < 5; i++)
            {
                GameObject nUnit = Instantiate(_workerUnit);
                nUnit.GetComponent<Unit>().SetPlayerOwner(info);
                nUnit.transform.position = centreSpawn + (Vector3)Random.insideUnitCircle;
            }
        }
    } 
}
