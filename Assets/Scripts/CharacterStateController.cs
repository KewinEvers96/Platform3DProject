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

    #region DamageCotrolVariables
    Timer damageTimer;
    bool recevingDamage;
    #endregion
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
            EventManager.TriggerEvent("coinsAdded");
        }
    }

    public int LifePoints
    {
        get
        {
            return _lifePoints;
        }
    }


    #endregion


    private void Awake()
    {
        _coinsCollected = 0;
        _lifePoints = 100;
        damageTimer = GetComponent<Timer>();
        damageTimer.MaxTime = 3f;
        player = GetComponent<Transform>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)

    {
        GameObject other = hit.collider.gameObject;

        if (other.CompareTag("Collectable"))
        {
            Collectable collectable = other.GetComponent<Collectable>();
            collectable.OnCollected(this);
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
            UpdateLifePoints();
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
                UpdateLifePoints();
                _timerToDescount += 1;
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
                UpdateLifePoints();
                figthing = true;
            }
            else if (distance > 1.5f)
            {
                figthing = false;
                enemyDetected = false;
            }
        }
    }

    void UpdateLifePoints()
    {
        if(!recevingDamage && !damageTimer.Started)
        {
            _lifePoints -= 5;
            EventManager.TriggerEvent("LifePointsUpdated");
            recevingDamage = true;
            damageTimer.RunTimer();
        }
        if(recevingDamage && damageTimer.Started && !damageTimer.Running)
        {
            recevingDamage = false;
            damageTimer.Restart();
        }
    }

}
