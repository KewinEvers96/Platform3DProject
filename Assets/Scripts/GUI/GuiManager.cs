using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuiManager : MonoBehaviour
{

    const string coinsTextFormat = "Coins: ";
    const string lifeTextFormat = "Life Points: ";

    Text lifePointsText;
    Text coinsText;

    CharacterStateController playerState;


    private void Awake()
    {

        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStateController>();
        coinsText = GameObject.FindGameObjectWithTag("CoinsText").GetComponent<Text>();
        lifePointsText = GameObject.FindGameObjectWithTag("LifeText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {

        AddCoinsToGUI();
        UpdateLifePoints();

        EventManager.StartListening("coinsAdded", AddCoinsToGUI);
        EventManager.StartListening("LifePointsUpdated", UpdateLifePoints);
    }
    
    public void AddCoinsToGUI()
    {
        coinsText.text = coinsTextFormat + playerState.Coins;
    }

    public void UpdateLifePoints()
    {
        lifePointsText.text = lifeTextFormat + playerState.LifePoints;
    }

}
