using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int xCoord, yCoord;
    public GameObject playerPrefab;
    public PlayerNumber playerID;
    public long playerPoints;

    public CardType cardType;

    public Player(int number) {
        switch (number) {
            case 0: {
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
    }
    
}
