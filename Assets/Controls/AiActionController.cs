using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class AiActionController : ActionRelay
    {

        private void Start()
        {
            StartCoroutine(AiActions());
        }

        IEnumerator AiActions()
        {
            while(Foo())
            {
                yield return new WaitForSeconds(5);
            }
            GameObject res = FindObjectOfType<Resource>().gameObject;
            RaycastHit hit;
            bool t = Physics.Raycast(transform.position,res.transform.position -transform.position, out hit);
            if(t)
            {
                _layerHit = hit;
                AssignAction();
            }


        }

    }

}