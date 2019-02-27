using UnityEngine;

namespace RTS
{
    [System.Serializable]
    public class PlayerInformation
    {
        [SerializeField] int _id;
        [SerializeField] Color _color;
        [SerializeField] bool _isAi;
        [SerializeField] Transform _startPos;
        [SerializeField] ResourceData _resourceData = new ResourceData(500, 500);

        public int Id { get { return _id; } }
        public Color Color { get { return _color; } }
        public bool IsAi { get { return _isAi; } }
        public Transform StartPos { get { return _startPos; } }
        public ResourceData ResourceData { get { return _resourceData; } }

    } 
}
