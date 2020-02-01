using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int turnCounter { get; set; }
    private  int numberOfPlayers { get; set; }

    void Awake() {
        turnCounter = 0;
        numberOfPlayers = 0;
    }

    void Update()
    {
        
    }
}
