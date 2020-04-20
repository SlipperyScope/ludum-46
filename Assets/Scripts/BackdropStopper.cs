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
                //play explosion if michael makes it in time
                collision.gameObject.GetComponent<Bot>().ExplodeAndDie();
                //Destroy(collision.gameObject);
            }
        }
    }
}
