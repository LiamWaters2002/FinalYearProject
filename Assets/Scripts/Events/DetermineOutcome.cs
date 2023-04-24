using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineOutcome : MonoBehaviour
{

    public Graph steelGraph;
    public Graph wheatGraph;
    public Graph ironGraph;
    public Graph carGraph;

    int steelMillCount;
    int farmCount;
    int quarryCount;
    int carFactoryCount;



    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewBuilding(PlaceableObject placeable)
    {
        string good;
        if (placeable.name.Equals("Steel Mill"))
        {
            steelMillCount++;
            steelGraph.RightwardShiftInSupply();
        }
        else if (placeable.name.Equals("Farm"))
        {
            farmCount++;
            wheatGraph.RightwardShiftInSupply();
        }
        else if (placeable.name.Equals("Quarry"))
        {
            quarryCount++;
            ironGraph.RightwardShiftInSupply();
        }
        else if (placeable.name.Equals("Cars Factory"))
        {
            carFactoryCount++;
            carGraph.RightwardShiftInSupply();
        }

        
    }

    private void ShiftSupply(int amount)
    {

    }

    private void ShiftDemand(int amount)
    {

    }
}
