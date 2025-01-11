using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private DrinkManager _drinkManager;
    private ArrowChooser _arrowChooser;
    private Spin _spin;
    public static GameManager Instance;

    private int _playerNb = 1;
    private Drink _drink;

    private bool _revealTime;
    private bool _gameEnd = true;


    [Header("GAME INFO")]

    //Player nb
    [SerializeField] private TextMeshProUGUI _TxtPlayerNumber;
    [SerializeField] private Button _butPrevious;
    [SerializeField] private Button _butNext;
    [SerializeField] private GameObject _playerNumberPrefab;

    [Space(15)]
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

        //Btn nb player
        _butPrevious.interactable = false;
    }
    private void Start()
    {
        _butReveal.text = "START";
    }
    public int GetPlayerNumber()
    {
        return _playerNb * 2;
    }

    public void StartNewRound()
    {
        //_alcoolInfoRound.text = "";
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
        UpdateText(_drinkManager.GetAlcoolInRound() + " ALCOOL \n" + _drinkManager.GetWaterInfo() + " WATER", Color.white);
    }

    public void PlayButtonPress()
    {
        if (_revealTime)
        {
            // REVEAL //
            UpdateText(_drink.GetContainer().ToString(), Color.red);
            if (_drink.GetContainer() == Drink.DrinkCategories.Alcool)
            {
                StartCoroutine(FoundAlcool());
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
                _playerNumberPrefab.SetActive(false);

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

    private IEnumerator FoundAlcool()
    {
        //END
        _gameEnd = true;
        _butReveal.transform.parent.gameObject.SetActive(false);

        //RESTART
        yield return StartCoroutine(_drinkManager.DestroyPreviousDrinks());
        Restart();
    }

    private void Restart()
    {
        // CLEAR //
        _alcoolInfoRound.text = "";

        // RESET //
        _playerNumberPrefab.SetActive(true);
        _butReveal.transform.parent.gameObject.SetActive(true);
    }
    
    // Btn Player Number
    public void IncreasePlayerNumber()
    {
        //player number max : 6
        if (_playerNb<6)
        {
            _playerNb += 1;
            _TxtPlayerNumber.text = _playerNb.ToString();
        }

        //Interactable Btn
        if (_playerNb == 6)
        {
            _butNext.interactable = false;
        } else if (_playerNb == 2)
        {
            _butPrevious.interactable = true;
        }
    }

    public void DecrecreasePlayerNumber()
    {
        //player number min : 1
        if (_playerNb > 1)
        {
            _playerNb -= 1;
            _TxtPlayerNumber.text = _playerNb.ToString();
        }

        //Interactable Btn
        if (_playerNb == 1)
        {
            _butPrevious.interactable = false;
        }
        else if (_playerNb == 5)
        {
            _butNext.interactable = true;
        }
    }

    private void UpdateText(string text, Color color)
    {
        _alcoolInfoRound.text = text;
        _alcoolInfoRound.color = color;
    }

}
