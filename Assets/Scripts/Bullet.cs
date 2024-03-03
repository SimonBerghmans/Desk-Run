using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Zombie")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            GameManager.activeZombies--;
            Debug.Log("hit zombie");
        }
        else
        {
            //Destroy(this.gameObject);
        }

    }
}
