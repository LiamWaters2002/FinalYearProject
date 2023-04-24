using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovActionScript : MonoBehaviour
{
    public Graph graph;
    public CanvasController canvasController;

    public void LeftwardShiftDemand()
    {
        graph.LearnLeftDemandShift();
        canvasController.CloseAll();
    }

    public void RightwardShiftDemand()
    {
        graph.LearnRightDemandShift();
        canvasController.CloseAll();
    }

    public void LeftwardShiftSupply()
    {
        graph.LearnLeftSupplyShift();
        canvasController.CloseAll();
    }

    public void RightwardShiftSupply()
    {
        graph.LearnRightSupplyShift();
        canvasController.CloseAll();
    }



}
