using System;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowChooser : MonoBehaviour
{
    private Collider2D actualDrink;

    private bool _lockAlcool;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Drink"))
        {
            actualDrink = other;
            if (_lockAlcool)
            {
                ReturnDrink();
                _lockAlcool = false;
            }
        }
    }
    private void ReturnDrink()
    {
        GameManager.Instance.StopSpin(actualDrink.GetComponent<Drink>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Drink"))
        {
            actualDrink = null;
        }    
    }

    public void LockDrink()
    {
        _lockAlcool = true;
    }

    private Drink.DrinkCategories GetContenant()
    {
        return actualDrink.GetComponent<Drink>().GetContainer();
    }
}
