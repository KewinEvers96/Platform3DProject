using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    const string prefabPath = "Prefab/";

    [SerializeField]
    int numberOfCoins = 3;
    [SerializeField]
    float separationRatio = 1f;

    float spaceBeetweenCoins ;

    GameObject coinPrefab;

    Vector3 currentPosition;
    Vector3 squareCorner;

    // Start is called before the first frame update
    void Start()
    {
        coinPrefab = Resources.Load<GameObject>(prefabPath + "Coin");
        currentPosition = transform.position;
        squareCorner = new Vector3(currentPosition.x - numberOfCoins, 
                                    currentPosition.y + 1, 
                                    currentPosition.z + numberOfCoins);
        spaceBeetweenCoins = separationRatio * numberOfCoins;

        int i, j;

        for(i = 0; i < numberOfCoins; i++)
        {
            for (j = 0; j < numberOfCoins; j++)
            {

                Instantiate<GameObject>(coinPrefab,
                    new Vector3(squareCorner.x + (i * spaceBeetweenCoins), squareCorner.y, squareCorner.z - (j * spaceBeetweenCoins)), 
                    Quaternion.identity);
            }
        }
    }

}
