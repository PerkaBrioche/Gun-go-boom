using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public interface IClickable
    {
        public void Clicked();
    }

    public void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.up);
        if(hit.collider == null){return;}
        print(hit);
        if (hit.collider.TryGetComponent(out IClickable ic))
        {
            ic.Clicked();
        }
    }
}
