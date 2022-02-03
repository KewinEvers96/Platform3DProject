using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform enemy;
    float distance;
    bool figthing = false;

    #region Character_state_attributes
    int _coinsCollected;

    [SerializeField]
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
        _lifePoints = 100;

        player = GetComponent<Transform>();
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
    }

    private void Update() {
        distance = Vector3.Distance(enemy.position, player.position);
        if (distance < 1.5f && figthing == false){
            _lifePoints -= 5;
            figthing = true;
        }
        else if (distance > 1.5f) figthing = false;
    }

}
