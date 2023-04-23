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
    public float gameDuration = 0f;

    public Image imgPausePlay;
    public Sprite pause;
    public Sprite play;
    public Text GameMoney;

    public RandomEvents randomEvents;



    void Update()
    {
        if (!isGameTimePaused)
        {
            gameDuration = gameDuration +  Time.deltaTime;

            if (gameDuration > secondsPerMonth)
            {
                string moneyString = GameMoney.text.Replace(",", "");
                int money = int.Parse(moneyString);
                money = money + 10000;
                GameMoney.text = money.ToString("N0");
            }

            // Calculate months passed based on gameDuration / gameSpeed(seconds per month)
            int monthsPassed = Mathf.FloorToInt(gameDuration / secondsPerMonth);

            // Increment the month and year based on the number of months passed
            currentMonth = currentMonth + monthsPassed;
            if (currentMonth > 12)
            {
                currentYear += Mathf.FloorToInt(currentMonth / 12);
                currentMonth %= 12;

                RandomEvent();
            }
            monthText.text = currentMonth.ToString();
            yearText.text = currentYear.ToString();

            // Subtract the time linked with elapsed months from the game duration
            gameDuration = gameDuration - (monthsPassed * secondsPerMonth);
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

    public void RandomEvent()
    {
        ToggleGameTimePaused(); // Pause timer
         randomEvents.DisplayRandomEvent();
    }
}
