using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        rb.angularDrag = 0;

        rb.velocity = new Vector3(
            Random.Range (-1f, 1f),
            Random.Range (-1f, 1f),
            Random.Range (-1f, 1f)
        ).normalized*speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
