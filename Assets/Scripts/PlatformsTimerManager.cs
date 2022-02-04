using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformsTimerManager : MonoBehaviour
{



    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnPoint;

    GameObject platformsTimer;

    GameObject oldPlatforms;

    UnityAction listener;

    Vector3 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        platformsTimer = Resources.Load<GameObject>("Prefab/PlatformsTimer");
        listener = CaidaLibre;
        EventManager.StartListening("timerPlatformFallDown", listener);
    }

    // Start is called before the first frame update
    public void CaidaLibre()
    {
        Player.transform.position = respawnPoint.transform.position;
    }

}
