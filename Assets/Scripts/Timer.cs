using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    float currentTime;
    float maxTime;
    bool running;
    bool started;

    public float MaxTime {
        set {
            maxTime = value;
        }   
    }

    public bool Running
    {
        get
        {
            return running;
        }
    }

    public bool Started
    {
        get
        {
            return started;
        }
    }

    public float TimeRemaning
    {
        get
        {
            return maxTime - currentTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(started && running)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= maxTime)
            {
                running = false;
            }
        }
    }

    public void RunTimer()
    {
        started = true;
        running = true;
        currentTime = 0;
    }
}
