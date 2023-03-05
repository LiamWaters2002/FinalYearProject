using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods
{
    protected int inferiorQuantity; // The amount of inferior quality produced items
    protected int normalQuantity; // The amount of normal quality produced items
    protected int luxuryQuantity; // The amount of luxury quality produced items

    public Goods(int startingInferiorQuantity, int startingNormalQuantity, int startingLuxuryQuantity)
    {
        inferiorQuantity = startingInferiorQuantity;
        normalQuantity = startingNormalQuantity;
        luxuryQuantity = startingLuxuryQuantity;
    }

    public virtual void Produce(int inferiorAmount, int normalAmount, int luxuryAmount)
    {
        inferiorQuantity += inferiorAmount;
        normalQuantity += normalAmount;
        luxuryQuantity += luxuryAmount;
    }

    public virtual bool Consume(int inferiorAmount, int normalAmount, int luxuryAmount)
    {
        if (inferiorAmount > inferiorQuantity || normalAmount > normalQuantity || luxuryAmount > luxuryQuantity)
        {
            Debug.LogError("Not enough " + GetType().Name + " to consume!");
            return false;
        }

        inferiorQuantity -= inferiorAmount;
        normalQuantity -= normalAmount;
        luxuryQuantity -= luxuryAmount;

        return true;
    }

    public int GetInferiorQuantity()
    {
        return inferiorQuantity;
    }

    public int GetNormalQuantity()
    {
        return normalQuantity;
    }

    public int GetLuxuryQuantity()
    {
        return luxuryQuantity;
    }
}