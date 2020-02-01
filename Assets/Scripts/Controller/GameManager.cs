using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PassiveCardManager))]
public class GameManager : MonoBehaviour
{
    [HideInInspector] public List<Player> players;

    public int numberOfPlayers;

    public PlayerNumber currentPlayer;
    public GameState state;


    private PassiveCardManager passiveCardEffectManager;

    void Start() {

        passiveCardEffectManager = GetComponent<PassiveCardManager>();
        state = GameState.START;
        SetupBattle();

        if ((numberOfPlayers < 2) || (numberOfPlayers > 4)) {
            Debug.LogWarning("Number of players is not within the correct range!");
            numberOfPlayers = 2;
        }
    }
    private void SetupBattle() {
        for (int i = 0; i  < numberOfPlayers; i++)
        {   
            Player player = new Player(i);
            players.Add(player);
            player.playerID = (PlayerNumber) i;
            player.playerPoints = 0;
            player.cardType = passiveCardEffectManager.AssignRandomCard(i);
        }
    }
}
