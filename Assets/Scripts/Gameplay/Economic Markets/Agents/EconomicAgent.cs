using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicAgent
{
    public float price;
    public float quantity;

    // Quantity that the buyer/seller is willing to sell/buy at a given price
    public float QuantityAtPrice(float price)
    {
        if (price >= this.price)
        {
            return quantity;
        }
        else
        {
            return 0f;
        }
    }
}
