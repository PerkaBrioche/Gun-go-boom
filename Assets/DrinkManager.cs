using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrinkManager : MonoBehaviour
{
    [SerializeField] private GameObject _drinkPrefabs;
    [SerializeField] private Transform _drinkParent;

    private bool CanBeSpecial;
    
    [Header("ROUND INFO")]
    
    private int _drinkLeftToSpawn; 
    private int _playerNumberThisRound;
    private int _alcoolLeftToSpawn;

    private List<Drink> listDrinksThisRound = new List<Drink>();

    private void Start()
    {
        NewRound();
    }

    private void NewRound()
    {
        CanBeSpecial = true;
        _playerNumberThisRound = GameManager.Instance.GetPlayerNumber();
        _drinkLeftToSpawn = _playerNumberThisRound;
        _alcoolLeftToSpawn = GetAlcoolNumber();
        SpawnDrink(_playerNumberThisRound);
    }
    
    private void SpawnDrink(int DrinkToSpawn)
    {
        for (int i = 0; i < DrinkToSpawn; i++)
        {
            var drink = Instantiate(_drinkPrefabs, _drinkParent);
            if(drink.TryGetComponent(out Drink drinkComponent))
            {
                drinkComponent.GetNewContainer(GetRandomContainer(), SetSpecial());
                listDrinksThisRound.Add(drinkComponent);
            }
            else
            {
                Debug.LogError("N'A PAS LE COMPONENT DRINK");
            }

            _drinkLeftToSpawn--;
        }
        GiveSam();

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
        int playerNumber = _playerNumberThisRound;
        int PlayerRange = _playerNumberThisRound / 3;
        print("PlayerRange = " + PlayerRange);
        int alcoolLeft = Random.Range(PlayerRange - 1, PlayerRange + 1);
        
        if (alcoolLeft < 1)
        {
            return 1;
        }

        return alcoolLeft;
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
}
