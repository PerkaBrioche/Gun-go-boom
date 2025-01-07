using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    [Header("GAME INFO")] 
    private int playerNumber;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerNumber = 5;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    
    
}
