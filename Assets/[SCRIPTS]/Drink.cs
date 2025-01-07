using UnityEngine;

public class Drink : MonoBehaviour
{
    public enum DrinkCategories
    {
        Water,
        Alcool,
    }

    [SerializeField] private bool _isSpecial;
    [SerializeField] private bool _IsSam;

    [Header("The drink contain ; ")]
    [SerializeField] private DrinkCategories Contain;

    public void GetNewContainer(DrinkCategories contain, bool isSpecial)
    {
        Contain = contain;
        _isSpecial = isSpecial;
    }

    public void SetSamStatus()
    {
        _IsSam = true;
        _isSpecial = false;
    }
}
