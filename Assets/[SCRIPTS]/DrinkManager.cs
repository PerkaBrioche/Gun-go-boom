using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrinkManager : MonoBehaviour
{
    [SerializeField] private GameObject _drinkPrefabs;
    [SerializeField] private Transform _drinkParent;

    private bool CanBeSpecial;
    [Header("GAME INFO")] 
    [SerializeField] private float timeBeetweenSpawn;
    [SerializeField] private float shakeIntensitySpawn;
    [SerializeField] private float durationShakeSpawn;
    [Header("RULES INFO")]
    
    private int _drinkLeftToSpawn; 
    private int _alcoolLeftToSpawn;
    
    [Header("ROUND INFO")]

    private int _playerNumberThisRound;
    


    private List<Drink> listDrinksThisRound = new List<Drink>();

    public bool drinksReady;

    private void Start()
    {
    }

    public void PrepareDrink()
    {
        CanBeSpecial = true;
        _playerNumberThisRound = GameManager.Instance.GetPlayerNumber();
        _drinkLeftToSpawn = _playerNumberThisRound;
        _alcoolLeftToSpawn = GetAlcoolNumber();

        StartCoroutine(WaitForSpawn());
    }
    
    
    
    private void SpawnDrink()
    {
        var drink = Instantiate(_drinkPrefabs, _drinkParent);
        if(drink.TryGetComponent(out Drink drinkComponent))
        {
            drinkComponent.SetNewContainer(GetRandomContainer(), SetSpecial());
            listDrinksThisRound.Add(drinkComponent);
        }
        else
        {
            Debug.LogError("N'A PAS LE COMPONENT DRINK");
        }
        _drinkLeftToSpawn--;
    }

    private void GiveSam()
    {
        int randomValue = Random.Range(0, listDrinksThisRound.Count);
        listDrinksThisRound[randomValue].SetSamStatus();
    }
    
    private bool SetSpecial()
    {
        if (!CanBeSpecial)
        {
            return false;
        }

        int randomValue = Random.Range(0, _drinkLeftToSpawn);
        if (randomValue == 0)
        {
            CanBeSpecial = false;
            return true;
        }
        
        return false;
    }
    
    private int GetAlcoolNumber()
    {
        int PlayerRange = (int)(_playerNumberThisRound / 3);
        int alcoolLeft = Random.Range(PlayerRange - 1, PlayerRange + 1);
        
        if (alcoolLeft < 1)
        {
            return 1;
        }

        return alcoolLeft;
    }

    public int GetAlcoolInRound()
    {
        return (int)(_playerNumberThisRound / 3) ;
    }

    private Drink.DrinkCategories GetRandomContainer()
    {
        if (_alcoolLeftToSpawn < 0)
        {
            return Drink.DrinkCategories.Water;
        }

        if (Random.Range(0, _drinkLeftToSpawn) == 0)
        {
            return Drink.DrinkCategories.Alcool;
        }
        return Drink.DrinkCategories.Water;

    }

    private IEnumerator WaitForSpawn()
    {
        while (_drinkLeftToSpawn > 0 )
        {
            SpawnDrink();
            ShakeManager.instance.ShakeCamera(shakeIntensitySpawn,durationShakeSpawn);
            yield return new WaitForSeconds(timeBeetweenSpawn);
        }
        
        GiveSam();
        GameManager.Instance.StartSpin();
    }

    public int GetWaterInfo()
    {
        return (_playerNumberThisRound - GetAlcoolInRound());
    }
    public (int, int) GetDrinksInfo()
    {
        return (GetAlcoolInRound(), _playerNumberThisRound - GetAlcoolInRound());
    }
    
}
