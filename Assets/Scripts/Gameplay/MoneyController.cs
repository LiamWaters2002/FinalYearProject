using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController: MonoBehaviour
{
    private float amount;
    private float governmentOwnedAmount;

    public float Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public float GovernmentOwnedAmount
    {
        get { return governmentOwnedAmount; }
        set { governmentOwnedAmount = value; }
    }

    public void Start() 
    {
        amount = 2000000;
        governmentOwnedAmount = 1000000;
    }

    void Update()
    {

    }

    //Printing money
    public void Add(float money)
    {
        amount = amount + money;
        governmentOwnedAmount = governmentOwnedAmount + money;
    }

    //Removing money
    public void Subtract(float money)
    {
        amount = amount - money;
        governmentOwnedAmount = governmentOwnedAmount - money;
    }

    public void CollectTax(float amount)
    {
        governmentOwnedAmount = governmentOwnedAmount + amount;
    }

    public void GovernmentSpend(float amount)
    {
        governmentOwnedAmount = governmentOwnedAmount - amount;
    }

    public void IncreaseGovernmentOwnedAmount(float amount)
    {
        governmentOwnedAmount = governmentOwnedAmount + amount;
    }

    //Gets the value of the total amount of money the government owns
    public float GetGovernmentOwnedAmount()
    {
        return governmentOwnedAmount;
    }
}
