using System;
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
    public int maxTurns;

    public List<GameObject> fertile;
    public List<GameObject> crater;
    public List<GameObject> playerBase;
    public List<GameObject> water;
    public List<GameObject> plains;

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
            players[i].playerPoints = 0;
            players[i].cardType = passiveCardEffectManager.AssignRandomCard(i);
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
            if (index < players.Count)
            {
                currentPlayer = players[index];
            }
            else
            {
                currentPlayer = players[0];
                turnCounter++;
            }
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
        if (MoveIsLegal(tile))
        {
            endOfTurn = tile.DevelopTile(currentPlayer);
            currentPlayer.ownedTiles.Add(tile);
        }
        if (endOfTurn) {

            //handle tile stealing (reclaiming)
            if (tile.ClaimEnemyTile(currentPlayer.playerID))
            {
                ///perform changeover
                ///
                Debug.Log("points stolen: " + tile.totalPointValue);
                Player oldPlayer = players[((int)tile.tileOwner)];
                currentPlayer.playerPoints += tile.totalPointValue;
                oldPlayer.playerPoints -= tile.totalPointValue;
                tile.tileOwner = currentPlayer.playerID;
            }
            //handle tile claiming and development
            else
            {
                currentPlayer.playerPoints += (int)tile.tileState;
                tile.totalPointValue += (int)tile.tileState;
                //bonus point calculation
                int bonusPoints = tile.CalculateBonusPoints();
                tile.totalPointValue += bonusPoints;
                currentPlayer.playerPoints += bonusPoints;
            }

            for (int i = 0; i < players.Count; i++)
            {
                Debug.Log("player " + (i+1) + " has points " + players[i].playerPoints);
            }
            PlayerTurn();
            Debug.Log($"We are on turn {turnCounter}");
            if (turnCounter == maxTurns) {
                state = GameState.END;
            }
        }
    }

    public Player GetPlayer(PlayerNumber playerID)
    {
        Player returnable = null;
        foreach (Player player in players) {
            if (player.playerID == playerID)
            {
                returnable = player;
            }
        }
        Debug.Log("CLAIM FROM PLAYER " + playerID);
        return returnable;
    }

    private bool MoveIsLegal(Tile desiredMove) {
        bool legal = false;
        foreach (Tile tile in currentPlayer.ownedTiles) {
            legal = legal || tile.adjacencyList.Contains(desiredMove) && desiredMove.IsAvailable();
            Debug.Log(legal);
        }
        return legal;
    }

    public GameObject ChangePlains(Tile tile)
    {
        switch (tile.tileState) {
            case TileState.IN_DEVELOPMENT: {
                return plains[1];
            }
            case TileState.TERRAFORMED:
            {
                return plains[2];
            }
            default:
            {
                return plains[0];
            }
        }
    }

    public GameObject ChangeWater(Tile tile)
    {
        switch (tile.tileState)
        {
            case TileState.IN_DEVELOPMENT:
            case TileState.TERRAFORMED:
            {
                return water[1];
            }
            default:
            {
                return plains[0];
            }
        }
    }

    public GameObject ChangeFertile(Tile tile)
    {
        switch (tile.tileState)
        {
            case TileState.IN_DEVELOPMENT:
            {
                return fertile[1];
            }
            case TileState.TERRAFORMED:
            {
                return fertile[2];
            }
            default:
            {
                return fertile[0];
            }
        }
    }
}
