using UnityEngine;

namespace RTS
{
    [System.Serializable]
    public class PlayerInformation
    {
        public int _id;
        public Color _color;
        public bool _isAi;
        public Transform _startPos;
    } 
}
