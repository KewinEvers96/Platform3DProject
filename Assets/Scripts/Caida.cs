using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caida : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnPoint;
    // Start is called before the first frame update
    public void CaidaLibre ()
    {
        Player.transform.position = respawnPoint.transform.position;
    }
    
}
