using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PassiveCardManager))]
[RequireComponent(typeof(ActiveCardManager))]
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

    private PassiveCardManager passiveCardEffectManager;
    private ActiveCardManager activeCardManager;

    void Start() {

        passiveCardEffectManager = GetComponent<PassiveCardManager>();
        activeCardManager = GetComponent<ActiveCardManager>();
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
                activeCardManager.ShowActiveCardUI(currentPlayer.cards);
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
        PlayerNumber oldPlayerID = tile.tileOwner;

        if (activeCardManager.bushfire)
        {
            if (TileIsTaken(tile))
            {

            }
        }
        
        if (MoveIsLegal(tile))
        {
            endOfTurn = tile.DevelopTile(currentPlayer);
            currentPlayer.ownedTiles.Add(tile); //should overwrite the same tile if already added
        }
        if (endOfTurn) {
            //handle tile stealing (reclaiming)
            if (IsStolen(oldPlayerID))
            {        
                Player oldPlayer = players[((int)oldPlayerID)];

                ///perform changeover
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
            //Debug.Log($"We are on turn {turnCounter}");
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
        return returnable;
    }

    private bool MoveIsLegal(Tile desiredMove) {
        bool legal = false;
        foreach (Tile tile in currentPlayer.ownedTiles) {
            legal = legal || tile.adjacencyList.Contains(desiredMove) && desiredMove.IsAvailable();
            //Debug.Log(legal);
        }
        return legal;
    }

    private bool TileIsTaken(Tile desiredMove)
    {
        bool taken = false; 
        return !desiredMove.tileOwner.Equals(currentPlayer);
    }

    public bool TrySabotage()
    {

        return false;
    }

    private bool IsStolen(PlayerNumber oldPlayerID) {
        Debug.Log("old id" + oldPlayerID);
        Debug.Log("my id" + currentPlayer.playerID);
        bool newClaim = oldPlayerID.Equals(PlayerNumber.NONE);
        if (!newClaim)
        {
            Debug.Log("not new claim!");
        }
        bool upgrade = currentPlayer.playerID.Equals(oldPlayerID);
        if (!upgrade)
        {
            Debug.Log("Not an upgrade! What?");
        }
        if (!upgrade && !newClaim)
        {
            Debug.Log("stolen!");
        }
            return (!upgrade && !newClaim);
    }
}
