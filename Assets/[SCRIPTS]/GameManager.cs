using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private DrinkManager _drinkManager;
    private ArrowChooser _arrowChooser;
    private Spin _spin;
    public static GameManager Instance;
    



    [Header("GAME INFO")] 
    [SerializeField] private int playerNumber;
    [SerializeField] private TextMeshProUGUI _alcoolInfoRound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _drinkManager = FindFirstObjectByType<DrinkManager>();
        _spin = FindFirstObjectByType<Spin>();
        _arrowChooser = FindFirstObjectByType<ArrowChooser>();
    }
    private void Start()
    {
        StartNewRound();
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void StartNewRound()
    {
        _drinkManager.PrepareDrink();
    }
    public void StartSpin()
    {
        _spin.StartSpin();
        ShowDrinksInfoInRound();
    }

    public void StartLockingSpin()
    {
        _arrowChooser.LockDrink();
    }

    public void StopSpin(Drink.DrinkCategories drinkCategory)
    {
        _spin.StopSpin();
        print(drinkCategory);
    }

    public void ShowDrinksInfoInRound()
    {
        _alcoolInfoRound.text = _drinkManager.GetAlcoolInRound() + " ALCOOLS \n" + _drinkManager.GetWaterInfo() + " WATER";
    }
    
    
}
