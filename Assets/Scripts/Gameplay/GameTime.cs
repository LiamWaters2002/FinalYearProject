using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public float secondsPerMonth = 1f;
    public bool isGameTimePaused = false; // Whether game time is paused or not
    public int currentMonth = 1; // The current month
    public int currentYear; // The current year
    public Text monthText; 
    public Text yearText; 
    public float totalElapsedTime = 0f;

    public Image imgPausePlay;
    public Sprite pause;
    public Sprite play;



    void Update()
    {
        if (!isGameTimePaused)
        {
            // Add the current frame's time to the total elapsed time
            totalElapsedTime += Time.deltaTime;

            // Calculate how many months have passed based on the total elapsed time
            int monthsPassed = Mathf.FloorToInt(totalElapsedTime / secondsPerMonth);

            // Increment the month and year based on the number of months passed
            currentMonth += monthsPassed;
            if (currentMonth > 12)
            {
                currentYear += Mathf.FloorToInt(currentMonth / 12);
                currentMonth %= 12;
            }
            monthText.text = currentMonth.ToString();
            yearText.text = currentYear.ToString();

            // Subtract the time linked with elapsed months from the total elapsed time
            totalElapsedTime -= monthsPassed * secondsPerMonth;
        }
    }

    public void IncreaseGameTimeSpeed()
    {
        //Only increase game time speed if it is not greater than 1 second per month
        if (secondsPerMonth != 1)
        {
            secondsPerMonth = secondsPerMonth - 1f;
        }
        
    }

    public void DecreaseGameTimeSpeed()
    {
        if (secondsPerMonth != 15)
        {
            secondsPerMonth = secondsPerMonth + 1f;
        }
    }

    public void ToggleGameTimePaused()
    {
        isGameTimePaused = !isGameTimePaused;

        if (isGameTimePaused)
        {
            imgPausePlay.sprite = play;
        }
        else
        {
            imgPausePlay.sprite = pause;
        }
    }
}
