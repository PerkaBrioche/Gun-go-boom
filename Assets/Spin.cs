using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float minimumSpeed = 0.0f;
    [SerializeField] float maximumSpeed = 500.0f;
    [SerializeField] float duration = 3.0f;

    bool isSpinning = false;
    bool canSpin = true;

    float startTime;
    float currentSpeed;


    void Update()
    {
        if (isSpinning)
        {
            float t = (Time.time - startTime) / duration;

            // Decrease speed
            currentSpeed = Mathf.SmoothStep(maximumSpeed, minimumSpeed, t); //maximumSpeed > minimunSpeed dc decceleration

            gameObject.transform.Rotate(0, 0, currentSpeed * Time.deltaTime * -1, Space.Self);

            if (t >= 1.0f)
            {
                ResetSpin();
            }
        }
    }


    //Btn
    public void StartSpin()
    {
        if (canSpin)
        {
            startTime = Time.time;

            isSpinning = true;
            canSpin = false;
        }
    }

    private void ResetSpin()
    {
        currentSpeed = 0;

        isSpinning = false;
        canSpin = true;
    }
}
