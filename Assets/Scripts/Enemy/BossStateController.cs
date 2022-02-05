using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateController : MonoBehaviour
{

    int bossLife = 5;
    bool beingHit = false;


    // Update is called once per frame
    void Update()
    {
        if(bossLife < 0)
        {
            EventManager.TriggerEvent("GameWon");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("FireBall") && !beingHit)
        {
            beingHit = true;
            bossLife -= 1;
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        GameObject other = collision.gameObject;

        if (other.CompareTag("FireBall"))
        {
            beingHit = false;
            Destroy(other);
        }
    }

}
