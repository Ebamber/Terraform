﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class Coordinate { 
        public int x, y;
    }

    public int mapConstraintX, mapConstraintY;
    public Coordinate coordinates;
    public GameObject playerPrefab;
    public PlayerNumber playerID;
    public int playerPoints;
    public CardType cardType;
    public List<ActiveCard> cards;
    public List<Tile> ownedTiles;
    public Color playerColour;

    private void Start()
    {
        cards = new List<ActiveCard>();
        cards.Add(new ActiveCard(ActiveCardType.BUSHFIRE, this));
        cards.Add(new ActiveCard(ActiveCardType.SEEDBOMB, this));
        cards.Add(new ActiveCard(ActiveCardType.SABOTAGE, this));
    }

    public Player(int number, int mapConstraintX, int mapConstraintY)
    {
       
        this.mapConstraintX = mapConstraintX;
        this.mapConstraintY = mapConstraintY;
        switch (number)
        {
            case 0:
                {
                    playerID = PlayerNumber.PLAYER_1;
                    break;
                }
            case 1:
                {
                    playerID = PlayerNumber.PLAYER_2;
                    break;
                }
            case 2:
                {
                    playerID = PlayerNumber.PLAYER_3;
                    break;
                }
            case 3:
                {
                    playerID = PlayerNumber.PLAYER_4;
                    break;
                }
            default:
                {
                    playerID = PlayerNumber.NONE;
                    break;
                }
        }
        //this.coordinates = GenerateCoordinate();
    }
    /*
    public Coordinate GenerateCoordinate()
    {
        //return new Coordinate(mapConstraintX, mapConstraintY);
    }
    */
}
