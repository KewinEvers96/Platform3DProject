using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    private Transform player;
    private Transform enemy;
    float distance;
    bool figthing = false;
    bool enemyDetected = false;
    #region Character_state_attributes
    int _coinsCollected;
    float _timerToDescount = 0;
    bool touchingLava = false;

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
        if (other.CompareTag ("Caida"))
        {
            Caida caida = other.GetComponent<Caida>();
            caida.CaidaLibre();
        }

        if (other.CompareTag("CaidaLibrePlatforms"))
        {
            EventManager.TriggerEvent("timerPlatformFallDown");
        }

        if (other.CompareTag("Enemy"))
        {
            enemy = other.gameObject.transform;
            enemyDetected = true;
        }
        if (other.CompareTag("Lava"))
        {
            if (_timerToDescount == 0){
                _lifePoints -= 5;
                _timerToDescount += 1;
                Debug.Log("Descounting Life"+ _lifePoints);
            }
            touchingLava = true;
        }

    }

    private void Update() {

        if (touchingLava){
            _timerToDescount = _timerToDescount + Time.deltaTime;
            if (_timerToDescount >= 5) _timerToDescount = 0;
            touchingLava = false;
        }
        else {
            _timerToDescount = 0;
        } 


        if (enemyDetected)
        {
            distance = Vector3.Distance(enemy.position, player.position);
            if (distance < 1.5f && figthing == false)
            {
                _lifePoints -= 5;
                figthing = true;
            }
            else if (distance > 1.5f)
            {
                figthing = false;
                enemyDetected = false;
            }
        }
    }

}
