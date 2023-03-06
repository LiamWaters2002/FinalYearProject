using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    public float minX = 0f;
    public float maxX = 100f;
    public float minY = 0f;
    public float maxY = 100f;

    public float supplyIntercept = 5f;
    public float supplySlope = 0.5f;
    public float demandIntercept = 50f;
    public float demandSlope = 0.5f;

    private GameObject supplyLine;
    private GameObject demandLine;
    private GameObject xLabel;
    private GameObject yLabel;

    void Start()
    {
        // Create the supply curve line renderer
        supplyLine = new GameObject("Supply Line");
        supplyLine.transform.parent = transform;
        LineRenderer supplyLineRenderer = supplyLine.AddComponent<LineRenderer>();
        supplyLineRenderer.positionCount = 2;
        Vector3[] supplyPositions = new Vector3[2];
        supplyPositions[0] = new Vector3(minX + 30, supplyIntercept + 30, 0f);
        supplyPositions[1] = new Vector3(maxX - 30, maxY * supplySlope + supplyIntercept, 0f);
        supplyLineRenderer.SetPositions(supplyPositions);
        supplyLineRenderer.material = Resources.Load<Material>("Materials/Supply");
        supplyLineRenderer.startWidth = 1f;
        supplyLineRenderer.endWidth = 1f;

        // Calculate the y-coordinate of the second point of the demand curve
        float demandY = demandSlope * (maxX - 30) + demandIntercept;

        // Adjust the y-coordinate to make the demand curve more or less inelastic
        demandY *= 0f - demandSlope;

        // Create the demand curve line renderer
        demandLine = new GameObject("Demand Line");
        demandLine.transform.parent = transform;
        LineRenderer demandLineRenderer = demandLine.AddComponent<LineRenderer>();
        demandLineRenderer.positionCount = 2;
        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(minX + 30, demandIntercept, 0f);
        Vector3 maxPosition = new Vector3(maxX - 30, demandY + 30, 0f);

        //Calcualte point of intersection (no need for line to go pass x axis)

        // Find the slope of the line
        float slope = (maxPosition.y - demandPositions[0].y) / (maxPosition.x - demandPositions[0].x);

        // Find the y-intercept of the line
        float yIntercept = demandPositions[0].y - slope * demandPositions[0].x;
        Debug.Log(yIntercept);
        Debug.Log(demandIntercept);

        float xIntersect = -yIntercept / slope;
        demandPositions[1] = new Vector3(xIntersect, 0f, 0f);

        demandLineRenderer.SetPositions(demandPositions);
        demandLineRenderer.material = Resources.Load<Material>("Materials/Demand");
        demandLineRenderer.startWidth = 1f;
        demandLineRenderer.endWidth = 1f;


        // Create the x-axis label
        xLabel = new GameObject("X Label");
        xLabel.transform.parent = transform;
        Text xLabelText = xLabel.AddComponent<Text>();
        xLabelText.text = "Quantity";
        xLabelText.alignment = TextAnchor.LowerCenter;
        xLabelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        xLabelText.fontSize = 16;
        RectTransform xLabelRect = xLabelText.GetComponent<RectTransform>();
        xLabelRect.pivot = new Vector2(0.5f, 0.05f);
        xLabelRect.anchoredPosition = new Vector2(maxX / 2, 0f);

        // Create the y-axis label
        yLabel = new GameObject("Y Label");
        yLabel.transform.parent = transform;
        Text yLabelText = yLabel.AddComponent<Text>();
        yLabelText.text = "Price";
        yLabelText.alignment = TextAnchor.UpperCenter;
        yLabelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        yLabelText.fontSize = 16;
        RectTransform yLabelRect = yLabelText.GetComponent<RectTransform>();
        yLabelRect.pivot = new Vector2(0.05f, 0.5f);
        yLabelRect.anchoredPosition = new Vector2(0f, maxY / 2);

        // Create the x-axis line renderer
        GameObject xAxis = new GameObject("X Axis");
        xAxis.transform.parent = transform;
        LineRenderer xAxisLineRenderer = xAxis.AddComponent<LineRenderer>();
        xAxisLineRenderer.positionCount = 2;
        Vector3[] xAxisPositions = new Vector3[2];
        xAxisPositions[0] = new Vector3(minX, 0f, 0f);
        xAxisPositions[1] = new Vector3(maxX, 0f, 0f);
        xAxisLineRenderer.SetPositions(xAxisPositions);
        xAxisLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        xAxisLineRenderer.startWidth = 1f;
        xAxisLineRenderer.endWidth = 1f;

        // Create the y-axis line renderer
        GameObject yAxis = new GameObject("Y Axis");
        yAxis.transform.parent = transform;
        LineRenderer yAxisLineRenderer = yAxis.AddComponent<LineRenderer>();
        yAxisLineRenderer.positionCount = 2;
        Vector3[] yAxisPositions = new Vector3[2];
        yAxisPositions[0] = new Vector3(0f, minY, 0f);
        yAxisPositions[1] = new Vector3(0f, maxY, 0f);
        yAxisLineRenderer.SetPositions(yAxisPositions);
        yAxisLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        yAxisLineRenderer.startWidth = 1f;
        yAxisLineRenderer.endWidth = 1f;
    }
}