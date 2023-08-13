using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class ItemCollector : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.position = PlayerController.Instance.transform.position;
        }

        private void OnTriggerEnter2D(Collider2D p_collision)
        {
            Experience expCollectible = p_collision.GetComponent<Experience>();
            if(expCollectible == null) { return; }
            expCollectible.StartAttracting();
        }
    }
}