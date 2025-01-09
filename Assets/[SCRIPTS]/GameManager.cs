using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private DrinkManager _drinkManager;
    private ArrowChooser _arrowChooser;
    private Spin _spin;
    public static GameManager Instance;

    private Drink _drink;


    private bool _revealTime;
    private bool _gameEnd = true;
    


    [Header("GAME INFO")] 
    [SerializeField] private int playerNumber;
    [SerializeField] private TextMeshProUGUI _alcoolInfoRound;
    [SerializeField] private TextMeshProUGUI _butReveal;
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
        _butReveal.text = "START";
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void StartNewRound()
    {
        _alcoolInfoRound.text = "";
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

    public void StopSpin(Drink drinkValue)
    {
        _spin.StopSpin();
        _drink = drinkValue;
        _revealTime = true;
        _butReveal.transform.parent.gameObject.SetActive(true);
    }

    public void ShowDrinksInfoInRound()
    {
        _alcoolInfoRound.text = _drinkManager.GetAlcoolInRound() + " ALCOOL \n" + _drinkManager.GetWaterInfo() + " WATER";
    }

    public void PlayButtonPress()
    {
        if (_revealTime)
        {
            // REVEAL //
            _alcoolInfoRound.text = _drink.GetContainer().ToString();
            if (_drink.GetContainer() == Drink.DrinkCategories.Alcool)
            {
                FoundAlcool();
            }
            else
            {
                FoundWater();
            }
            _butReveal.text = "START";
            _revealTime = false;
        }
        else
        {
            // START //
            if (_gameEnd)
            {
                _gameEnd = false;
                StartNewRound();
            }
            else
            {
                StartSpin();
            }
            _butReveal.transform.parent.gameObject.SetActive(false);
            _butReveal.text = "REVEAL";
        }
                    


    }
    
    

    private void FoundWater()
    {
        _drink.SetNewContainer( Drink.DrinkCategories.Alcool, false);
    }

    private void FoundAlcool()
    {
        _drinkManager.DestroyPreviousDrinks();
        _gameEnd = true;
    }


    
    
    
    
}
