using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePlatorm : MonoBehaviour
{

    [SerializeField]
    int maxTouches;

    int currentTouches;

    // Start is called before the first frame update
    void Start()
    {
        currentTouches = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTouches > maxTouches)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void addTouch()
    {
        currentTouches += 1;
    }

}
