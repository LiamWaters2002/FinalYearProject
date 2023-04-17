using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnContents : MonoBehaviour
{

    public CanvasController canvasController;

    Graph shiftingGraph;
    Graph elasticityGraph;

    public void Update()
    {
        if (canvasController.isLearnOpen() && shiftingGraph == null && elasticityGraph == null)
        {
            shiftingGraph = new Graph();
            elasticityGraph = new Graph();
        }
    }

}
