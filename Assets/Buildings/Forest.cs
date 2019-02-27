using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    /**
     * Container Class for all Tree Resources that are children
     * of this Gameobject
    */

    public class Forest : MonoBehaviour
    {
        public List<GameObject> Trees { get; private set; } = new List<GameObject>();

        void Start()
        {
            Trees = GetTreesInForest();
        }

        private List<GameObject> GetTreesInForest()
        {
            List<GameObject> trees = new List<GameObject>();
            foreach (Transform tree in transform)
            {
                trees.Add(tree.gameObject);
            }
            return trees;
        }

    } 
}
