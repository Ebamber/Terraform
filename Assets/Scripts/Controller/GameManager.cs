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
            Debug.Log($"Player Coordinates are {player.coordinates.x},{player.coordinates.y}");
            Debug.Log($"Tile at {player.coordinates.x},{player.coordinates.y} is " + grid[player.coordinates.x, player.coordinates.y]);
            
            Tile tile = grid[player.coordinates.x, player.coordinates.y];
        }
        state = GameState.PLAYER_STATE;
        currentPlayer = players[0];

        PlayerTurn();
    }

    private void PlayerTurn()
    {
        if (state != GameState.END)
        {
            if (!endOfTurn)
            {
                //do stuff and end turn
            }
            else
            {
                int index = players.FindIndex(p => currentPlayer);
                if (index < players.Count)
                {
                    currentPlayer = players[index + 1];
                }
                else
                {
                    currentPlayer = players[0];
                }
                PlayerTurn();
            }
        }
    }

    public void EndTurn()
    {
        endOfTurn = false;
    }

    public void TryClaimTile() {

    }
}
