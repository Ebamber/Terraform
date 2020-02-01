using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int turnCounter;
    public List<Player> players;

    public PlayerNumber currentPlayer;
    public GameState state;

    void Awake() {
        turnCounter = 0;
        state = GameState.START;
        SetupBattle();
    }

    private void SetupBattle() {
        //gameObject.AddComponent< new CardFactory().factory(players[0].cardType);
    }
}
