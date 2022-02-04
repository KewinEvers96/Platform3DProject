using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DestructablePlatorm : MonoBehaviour
{
    [SerializeField]
    float maxTime = 3f;

    Renderer renderer;
    bool colorRed;

    Timer timer;
    bool destructionStarted;

    UnityAction listener;

    public bool DestructionStarted 
    {
        get
        {
            return destructionStarted;
        }      
    }
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color",Color.black);
        colorRed = true;
        destructionStarted = false;
        timer = gameObject.AddComponent<Timer>();
        timer.MaxTime = maxTime;

        listener = ActivatePlatform;

        EventManager.StartListening("timerPlatformFallDown", listener);

    }

    // Update is called once per frame
    void Update()
    {
        if(!timer.Running && timer.Started)
        {
            gameObject.SetActive(false);
        }
        if(timer.Started && timer.Running && timer.TimeRemaning <= 1f)
        {
            if (colorRed)
            {
                colorRed = false;
                renderer.material.SetColor("_Color", Color.red);
            }
            else
            {
                colorRed = true;
                renderer.material.SetColor("_Color", Color.black);
            }
        }
    }

    public void ActivatePlatform()
    {
        gameObject.SetActive(true);
        destructionStarted = false;
        timer.Restart();
        renderer.material.SetColor("_Color", Color.black);
    }

    public void startTimer()
    {
        if(!timer.Running && !timer.Started)
        {
            timer.RunTimer();
            destructionStarted = true;
        }
    }

}
