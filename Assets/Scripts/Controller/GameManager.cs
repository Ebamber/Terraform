using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PassiveCardManager))]
[RequireComponent(typeof(ActiveCardManager))]
[RequireComponent(typeof(LandSelector))]
public class GameManager : MonoBehaviour
{
    public List<Player> players;
    public List<Text> playerScores;

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
    private ActiveCardManager activeCardManager;

    public AudioManager audioManager;

    void Start() {

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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

    public void PlayerTurn()
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
            audioManager.PlaySound(Sounds.GAME_END);
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
                playerScores[i].text = players[i].playerPoints.ToString();
            }
            PlayerTurn();
            //Debug.Log($"We are on turn {turnCounter}");
            if (turnCounter == maxTurns || AllTilesClaimed()) {
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

    private bool TileIsTaken(Tile desiredMove)
    {
        bool taken = false;
        return !desiredMove.tileOwner.Equals(currentPlayer);
    }

    public bool TrySabotage()
    {
        List<Tile> allEnemyTiles = new List<Tile>();
        foreach (Player player in players)
        {
            
            if (player.playerID != currentPlayer.playerID)
            {
                foreach (Tile tile in player.ownedTiles)
                {
                    if (!tile.terrainType.Equals(TerrainTypes.BASE)){
                        allEnemyTiles.Add(tile);
                    }                           
                }
                  
            }
        }
        Debug.Log("allEnemyTiles.Count is " + allEnemyTiles.Count);
            
        bool sabotage = false;
        PlayerNumber oldPlayerID = currentPlayer.playerID;
        int index = 0;
        if (allEnemyTiles.Count > 0)
        {
            System.Random random = new System.Random();
            index = random.Next(allEnemyTiles.Count);
            oldPlayerID = allEnemyTiles[index].tileOwner;
            sabotage = allEnemyTiles[index].StealAnyEnemyTile(currentPlayer.playerID);
        }
        
        if (sabotage)
        {
            Debug.Log("sabotage worked");

            Player oldPlayer = players[((int)oldPlayerID)];
            Tile tile = allEnemyTiles[index];
            ///perform changeover
            currentPlayer.playerPoints += tile.totalPointValue;
            oldPlayer.playerPoints -= tile.totalPointValue;
            tile.tileOwner = currentPlayer.playerID;
            tile.GetComponent<MeshRenderer>().material.SetColor("PlayerColour", currentPlayer.playerColour);
            audioManager.PlaySound(Sounds.SABOTAGE);
        }

        return sabotage;
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
       
        return (!upgrade && !newClaim);
    }

    private bool AllTilesClaimed() {
        bool isGameOver = true;
        foreach (Tile tile in hexGrid.grid) {
            isGameOver = isGameOver && tile.tileState == TileState.CLAIMED;
        }
        return isGameOver;
    }
}
