using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int turnCounter;
    public int numberOfPlayers;

    void Awake() {
        turnCounter = 0;
        numberOfPlayers = 0;
    }

    void Update()
    {
        
    }
}
