using UnityEngine;

public class Test1 : MonoBehaviour, InteractionManager.IClickable
{

    public void Clicked()
    {
        print("CLICKED 1");
    }
}
