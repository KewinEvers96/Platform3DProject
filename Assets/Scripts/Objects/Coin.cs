using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Collectable
{

    private int _value = 1;
    Vector3 rotatationDirection = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Collectable.OnCollected(CharacterStateController character)
    {
        character.Coins += _value;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
