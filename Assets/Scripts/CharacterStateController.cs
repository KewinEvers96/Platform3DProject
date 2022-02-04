using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{

    #region Character_state_attributes
    int _coinsCollected;
    int _lifePoints;

    public int Coins
    {
        get
        {
            return _coinsCollected;
        }
        set
        {
            _coinsCollected = value;
        }
    }

    #endregion


    private void Start()
    {
        _coinsCollected = 0;
        _lifePoints = 0;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)

    {
        GameObject other = hit.collider.gameObject;
        if (other.CompareTag("Collectable"))
        {
            Collectable collectable = other.GetComponent<Collectable>();
            collectable.OnCollected(this);
            Debug.Log("coins: " + _coinsCollected);
        }

        if (other.CompareTag("CoinSpawner"))
        {
            CoinSpawner coinSpawner = other.GetComponent<CoinSpawner>();
            coinSpawner.SpawnCoins();
        }

        if (other.CompareTag ("Caida"))
        {
            Caida caida = other.GetComponent<Caida>();
            caida.CaidaLibre();
        }

    }

}
