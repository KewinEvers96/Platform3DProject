using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        GameObject other = collision.gameObject;

        if (other.CompareTag("FireBall"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
