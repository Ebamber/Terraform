using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private PlayerNumber playerID { get; set; }
    private long playerPoints { get; set; }

    public Player(int number) {
        switch (number) {
            case 1: {
                playerID = PlayerNumber.PLAYER_1;
                break;
            }
            case 2:
            {
                playerID = PlayerNumber.PLAYER_2;
                break;
            }
            case 3:
            {
                playerID = PlayerNumber.PLAYER_3;
                break;
            }
            case 4:
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
