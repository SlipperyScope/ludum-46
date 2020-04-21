using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropStopper : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            if (collision.tag == "SmallBot" || collision.tag == "MediumBot" || collision.tag == "LargeBot")
            {
                var y = this.gameObject.GetComponent<BoxCollider2D>().bounds.min.y;
                if (collision.bounds.min.y > y)
                {
                    Destroy(collision.gameObject);
                } else
                {
                    collision.gameObject.GetComponent<Bot>().ExplodeAndDie();
                }
            }
        }
    }
}
