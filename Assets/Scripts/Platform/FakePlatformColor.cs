using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatformColor : MonoBehaviour
{

    [SerializeField]
    Color platformColor = Color.black;

    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color",platformColor);
    }
}
