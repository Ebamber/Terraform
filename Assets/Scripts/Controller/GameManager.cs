using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PassiveCardManager))]
[RequireComponent(typeof(LandSelector))]
public class GameManager : MonoBehaviour
{
    public List<Player> players;

    public int turnCounter;

    public Player currentPlayer;
    public GameState state;
    public HexGrid hexGrid;
    public bool endOfTurn;

    private PassiveCardManager passiveCardEffectManager;

    void Start() {

        passiveCardEffectManager = GetComponent<PassiveCardManager>();
        turnCounter = 0;
        state = GameState.START;
        if ((players.Count < 2) || (players.Count > 4)) {
            Debug.LogWarning("Number of players is not within the correct range!");
            //numberOfPlayers = 2;
        }
        SetupBattle();
    }
    private void SetupBattle() {
        for (int i = 0; i  < players.Count; i++)
        {

            Player player = players[i];
            player.playerID = (PlayerNumber) i;
            player.playerPoints = 0;
            player.cardType = passiveCardEffectManager.AssignRandomCard(i);
        }
        turnCounter++;
        Tile[,] grid = hexGrid.grid;
        foreach (Player player in players)
        {
            Tile tile = grid[player.coordinates.x, player.coordinates.y];
        }
        state = GameState.PLAYER_STATE;
        currentPlayer = players[0];

    }

    private void PlayerTurn()
    {
        if (state != GameState.END)
        {
            int index = (int)currentPlayer.playerID + 1;
            Debug.Log("current player index is " + index);
            Debug.Log("we have this many players " +  players.Count);
            if (index < players.Count)
            {
                currentPlayer = players[index];
            }
            else
            {
                currentPlayer = players[0];
            }
            Debug.Log("Current player is " + currentPlayer);
            endOfTurn = false;
        }
        else {
            //end the game
        }
    }

    public void EndTurn()
    {
        endOfTurn = false;
    }

    public void TryClaimTile(Tile tile) {
        endOfTurn = tile.DevelopTile(currentPlayer);
        if (endOfTurn) {
            currentPlayer.playerPoints += (int) tile.terrainType;
            PlayerTurn();
        }
    }
}
