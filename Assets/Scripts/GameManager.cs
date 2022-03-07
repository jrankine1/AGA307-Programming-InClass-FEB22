using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Title, Playing, Paused, GameOver};
public enum Difficulty { Easy, Medium, Hard};
public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public Difficulty diffculty;
    public int score;
    int scoreMultiplier = 1;
    
    void Start()
    {
        gameState = GameState.Title;

        switch(diffculty)
        {
            case Difficulty.Easy:
                scoreMultiplier = 1;
                break;
            case Difficulty.Medium:
                scoreMultiplier = 2;
                break;
            case Difficulty.Hard:
                scoreMultiplier = 3;
                break;
            default:
                scoreMultiplier = 1;
                break;
                   
        }
    }

   
}
