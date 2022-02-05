using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.StartListening("GameOver", GameOver);
        EventManager.StartListening("GameWon", GameWon);
    }
    

    void GameOver()
    {
        Debug.Log("Death");
    }
    
    void GameWon()
    {
        Debug.Log("GameWON");
    }

}
