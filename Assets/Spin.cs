using System;
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
    float endSpeedTimer;

    public bool readyToStop;

    void Update()
    {
        if (isSpinning)
        {
            float t = (Time.time - startTime) / duration;
            // Decrease speed
            if (!readyToStop)
            {
                currentSpeed = Mathf.SmoothStep(maximumSpeed, minimumSpeed, t); //maximumSpeed > minimunSpeed dc decceleration
            }
            else
            {
                // endSpeedTimer += Time.deltaTime;
                // currentSpeed = minimumSpeed - endSpeedTimer;
                // print("END SPEED = " + currentSpeed);
                // if (currentSpeed < 0)
                // {
                //     isSpinning = false;
                // }
            }

            gameObject.transform.Rotate(0, 0, currentSpeed * Time.deltaTime * -1, Space.Self);

            if (t >= 1.0f)
            {
                GameManager.Instance.StartLockingSpin();
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

    public void StopSpin()
    {
        currentSpeed = 0;

        isSpinning = false;
        canSpin = true;
    }
}
