using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventGameManager : MonoBehaviour
{

    [SerializeField]
    int level = 1;

    // Start is called before the first frame update
    void Awake()
    {
        EventManager.StartListening("GameOver", GameOver);
        EventManager.StartListening("GameWon", GameWon);
    }

    private void Update()
    {
        if(Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void GameOver()
    {
        switch (level) 
        {
            case 1:
                SceneManager.LoadScene(3);
                break;
            case 2:
                SceneManager.LoadScene(4);
                break;
            default:
                return;
        }

    }
    
    void GameWon()
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene(5);
                break;
            case 2:
                SceneManager.LoadScene(5);
                break;
            default:
                return;
        }
    }

}
