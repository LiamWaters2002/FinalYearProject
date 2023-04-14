using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Good
{
    string goodName;
    int quantityMade;

    public Good(string name)
    {
        goodName = name;
    }

    public int getQuantity()
    {
        return quantityMade;
    }

    public void produceGood(int amount)
    {
        quantityMade = quantityMade - amount;
    }

    public void consumeGood(int amount)
    {
        quantityMade = quantityMade - amount;
    }

    public string getName()
    {
        return goodName;
    }
}