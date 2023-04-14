using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Market
{
    public List<EconomicAgent> buyers = new List<EconomicAgent>();
    public List<EconomicAgent> sellers = new List<EconomicAgent>();

    //Elasticity/gradient
    public float elasticityDemand = 1;
    public float elasticitySupply = 1;

    public Graph graph;

    private float totalDemand;
    private float totalSupply;

    private string marketName;
    private int quantityOfGood;

    public Market(Good good)
    {
        totalDemand = 0f;
        totalSupply = 0f;
        marketName = good.getName();
    }

    public string getMarketName()
    {
        return marketName;
    }

    public void addBuyer(EconomicAgent buyer)
    {
        buyers.Add(buyer);
        UpdateTotalDemand();
    }

    public void removeBuyer(EconomicAgent buyer)
    {
        buyers.Remove(buyer);
        UpdateTotalDemand();
    }

    public void addSeller(EconomicAgent seller)
    {
        sellers.Add(seller);
        UpdateTotalSupply();
    }

    public void removeSeller(EconomicAgent seller)
    {
        sellers.Remove(seller);
        UpdateTotalSupply();
    }

    public void UpdateTotalDemand()
    {
        foreach (EconomicAgent buyer in buyers)
        {
            totalDemand += buyer.QuantityAtPrice(buyer.price);
        }
    }

    public void UpdateTotalSupply()
    {
        foreach (EconomicAgent seller in sellers)
        {
            totalSupply += seller.QuantityAtPrice(seller.price);
        }
    }

    void update()
    {

    }

    /// <summary>
    /// Formula for Quantity Demanded: Qd = a - bP
    /// </summary>
    /// <param name="price"> P = Price </param>
    /// <param name="constantA"> a = Constant  (influenced by external factors) </param>
    /// <param name="demandGradient">Gradient of the demand curve</param>
    /// <returns></returns>
    public float QuantityDemanded(float price, float constantA, float demandGradient)
    {
        float quantity = constantA - demandGradient * price;
        return quantity;
    }


    // c - level of supply independent of price
    // P - market price of product
    // d - supplyGradient

    /// <summary>
    /// Formula for Quantity Supplied: Qs = c + dP
    /// </summary>
    /// <param name="price"> P = Price </param>
    /// <param name="constantC"> c = Constant (supply given even when price is 0) </param>
    /// <param name="supplyGradient"></param>
    /// <returns></returns>
    public float QuantitySupplied(float price, float constantC, float supplyGradient)
    {
        float quantity = constantC + supplyGradient * price;
        return quantity;
    }


    /// <summary>
    /// Qd = Qs
    /// a - bP = c + dP
    /// a - c = bP + dP
    /// (a - c) / (b + d) = P* (Equilibrium Price)
    /// </summary>
    /// <param name="constantA"></param>
    /// <param name="constantC"></param>
    /// <param name="demandGradient"></param>
    /// <param name="supplyGradient"></param>
    /// <returns></returns>
    public float EquilibriumPrice(float constantA, float constantC, float demandGradient, float supplyGradient)
    {
        float equilibriumPrice = (constantA - constantC) / (demandGradient + supplyGradient);
        return equilibriumPrice;
    }

    /// <summary>
    /// Substitute P* into any of the quantity formulas:
    /// Q* = a - bP*
    /// </summary>
    /// <param name="constantB"></param>
    /// <param name="supplyGradient"></param>
    /// <param name="equilibriumPrice"></param>
    /// <returns></returns>
    public float EquilibriumQuantity(float constantC, float supplyGradient, float equilibriumPrice)
    {
        float equilibriumQuantity = QuantitySupplied(constantC, supplyGradient, equilibriumPrice);
        return equilibriumQuantity;
    }
}
