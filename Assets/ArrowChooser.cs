using System;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowChooser : MonoBehaviour
{
    private Collider2D actualDrink;
    

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Drink"))
        {
            actualDrink = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Drink"))
        {
            actualDrink = null;
        }    
    }

    private Drink.DrinkCategories GetContenant()
    {
        return actualDrink.GetComponent<Drink>().GetContainer();
    }
}
