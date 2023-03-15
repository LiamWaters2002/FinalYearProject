using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    public float minX = 0f; //Check unity inspector
    public float maxX = 150f; //Check unity inspector
    public float minY = 0f; //Check unity inspector
    public float maxY = 100f; //Check unity inspector

    public float scaleXAxis = 2f;
    public float scaleYAxis = 2f;

    public float supplyIntercept = 0f; //Check unity inspector
    public float supplySlope = 1f; //Check unity inspector
    public float demandIntercept = 50f; //Check unity inspector
    public float demandSlope = -1f; //Check unity inspector

    public float horizontalOffset = 30f; //Moves whole graph left/right
    public float verticalOffset = 30f; //Moves whole graph up/down

    private GameObject supplyLine;
    private GameObject demandLine;
    private GameObject xLabel;
    private GameObject yLabel;

    GameObject shiftedDemandCurve;
    public bool demandShift = false;

    void Start()
    {
        // Price (y) = Gradient of [supply or demand] (m) x Quantity [Supplied or demanded] (x) + [Minimum or maximum] Price (c)

        // Create the supply curve line renderer
        supplyLine = new GameObject("Supply Line");
        supplyLine.transform.parent = transform;
        LineRenderer supplyLineRenderer = supplyLine.AddComponent<LineRenderer>();
        supplyLineRenderer.positionCount = 2;
        Vector3[] supplyPositions = new Vector3[2];
        supplyPositions[0] = new Vector3(minX, supplyIntercept, 0f); // first point
        supplyPositions[1] = new Vector3(maxX, maxY * supplySlope + supplyIntercept, 0f); // second point
        supplyLineRenderer.SetPositions(supplyPositions);
        supplyLineRenderer.material = Resources.Load<Material>("Materials/Supply");
        supplyLineRenderer.startWidth = 1f;
        supplyLineRenderer.endWidth = 1f;

        // Create the demand curve line renderer
        demandLine = new GameObject("Demand Line");
        demandLine.transform.parent = transform;
        LineRenderer demandLineRenderer = demandLine.AddComponent<LineRenderer>();
        demandLineRenderer.positionCount = 2;
        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(minX, demandIntercept, 0f);
        demandPositions[1] = new Vector3(maxX, maxY * demandSlope + demandIntercept, 0f); // second point
        demandLineRenderer.SetPositions(demandPositions);
        demandLineRenderer.material = Resources.Load<Material>("Materials/Demand");
        demandLineRenderer.startWidth = 1f;
        demandLineRenderer.endWidth = 1f;


        //// Create the x-axis label
        //xLabel = new GameObject("X Label");
        //xLabel.transform.parent = transform;
        //Text xLabelText = xLabel.AddComponent<Text>();
        //xLabelText.text = "Quantity";
        //xLabelText.alignment = TextAnchor.LowerCenter;
        //xLabelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        //xLabelText.fontSize = 16;
        //RectTransform xLabelRect = xLabelText.GetComponent<RectTransform>();
        //xLabelRect.pivot = new Vector2(0.5f, 0.05f);
        //xLabelRect.anchoredPosition = new Vector2(maxX / 2, 0f);

        //// Create the y-axis label
        //yLabel = new GameObject("Y Label");
        //yLabel.transform.parent = transform;
        //Text yLabelText = yLabel.AddComponent<Text>();
        //yLabelText.text = "Price";
        //yLabelText.alignment = TextAnchor.UpperCenter;
        //yLabelText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        //yLabelText.fontSize = 16;
        //RectTransform yLabelRect = yLabelText.GetComponent<RectTransform>();
        //yLabelRect.pivot = new Vector2(0.05f, 0.5f);
        //yLabelRect.anchoredPosition = new Vector2(0f, maxY / 2);

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

        horizontalOffset = -1000f;
        verticalOffset = -1000f;

        //// Create a new game object for the x-axis label
        //GameObject xAxisLabel = new GameObject("X Axis Label");
        //xAxisLabel.transform.parent = xAxis.transform; // Make the label a child of the x-axis
        //xAxisLabel.transform.localPosition = new Vector3(maxX * scaleXAxis + horizontalOffset + 0.5f, verticalOffset + 2f, 0f); // Position the label slightly to the right of the end of the x-axis
        //TextMesh textMesh = xAxisLabel.AddComponent<TextMesh>(); // Add a TextMesh component to the label
        //textMesh.text = "Quantity"; // Set the text of the label
        //textMesh.fontSize = 50; // Set the font size of the text

        //// Create a new game object for the y-axis label
        //GameObject yAxisLabel = new GameObject("Y Axis Label");
        //yAxisLabel.transform.parent = yAxis.transform; // Make the label a child of the y-axis
        //yAxisLabel.transform.localPosition = new Vector3(horizontalOffset, maxY * scaleYAxis + verticalOffset + 2f, 0f);
        //TextMesh textMesh2 = yAxisLabel.AddComponent<TextMesh>(); // Add a TextMesh component to the label
        //textMesh2.text = "Y Axis Label"; // Set the text of the label
        //textMesh2.fontSize = 20; // Set the font size of the text
    }

    public void Update()
    {
        UpdateCurves(horizontalOffset, verticalOffset, supplySlope, demandSlope);
        UpdateXAxis(scaleXAxis, horizontalOffset, verticalOffset);
        UpdateYAxis(scaleYAxis, horizontalOffset, verticalOffset);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!demandShift)
            {
                shiftedDemandCurve = Instantiate(demandLine);
                demandShift = true;
            }
            ShiftDemand(shiftedDemandCurve);
        }

        //CreateIntersectionLines(horizontalOffset, verticalOffset);
    }

    public void UpdateCurves(float horizontalOffset, float verticalOffset, float supplyGradient, float demandGradient)
    {
        Vector3 center = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f, 0f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);//centre rotation (offset ignored)

        Vector3[] supplyPositions = new Vector3[2];
        supplyPositions[0] = new Vector3(-center.x + horizontalOffset, center.y * supplyGradient + verticalOffset, 0f); // first point
        supplyPositions[1] = new Vector3(center.x + horizontalOffset, -center.y * supplyGradient + verticalOffset, 0f); // second point
        supplyPositions[0] = rotation * supplyPositions[0] + center;
        supplyPositions[1] = rotation * supplyPositions[1] + center;

        supplyLine.GetComponent<LineRenderer>().SetPositions(supplyPositions);


        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(-center.x + horizontalOffset, center.y * demandGradient + verticalOffset, 0f); // first point
        demandPositions[1] = new Vector3(center.x + horizontalOffset, -center.y * demandGradient + verticalOffset, 0f); // second point
        demandPositions[0] = rotation * demandPositions[0] + center;
        demandPositions[1] = rotation * demandPositions[1] + center;

        demandLine.GetComponent<LineRenderer>().SetPositions(demandPositions);
    }

    void UpdateXAxis(float scaleXAxis, float horizontalOffset, float verticalOffset)
    {
        horizontalOffset = horizontalOffset - 30;
        verticalOffset = verticalOffset - 30;

        GameObject xAxis = GameObject.Find("X Axis");
        LineRenderer xAxisLineRenderer = xAxis.GetComponent<LineRenderer>();
        Vector3[] xAxisPositions = new Vector3[2];
        xAxisPositions[0] = new Vector3(minX * scaleXAxis + horizontalOffset, verticalOffset, 0f);
        xAxisPositions[1] = new Vector3(maxX * scaleXAxis + horizontalOffset, verticalOffset, 0f);
        xAxisLineRenderer.SetPositions(xAxisPositions);
    }

    void UpdateYAxis(float scaleYAxis, float horizontalOffset, float verticalOffset)
    {
        horizontalOffset = horizontalOffset - 30;
        verticalOffset = verticalOffset - 30;

        GameObject yAxis = GameObject.Find("Y Axis");
        LineRenderer yAxisLineRenderer = yAxis.GetComponent<LineRenderer>();
        Vector3[] yAxisPositions = new Vector3[2];
        yAxisPositions[0] = new Vector3(horizontalOffset, minY * scaleYAxis + verticalOffset, 0f);
        yAxisPositions[1] = new Vector3(horizontalOffset, maxY * scaleYAxis + verticalOffset, 0f);
        yAxisLineRenderer.SetPositions(yAxisPositions);
    }

    void ShiftDemand(GameObject shiftedDemandCurve)
    {
        LineRenderer lineRenderer = shiftedDemandCurve.GetComponent<LineRenderer>();
        MoveLineDiagonally(lineRenderer, demandSlope, 30, 30);
    }

    void ShiftSupply(string direction)
    {
        GameObject shiftedSupplyCurve = Instantiate(supplyLine);
        LineRenderer lineRenderer = shiftedSupplyCurve.GetComponent<LineRenderer>();
        MoveLineDiagonally(lineRenderer, supplySlope, -30, -30);
    }

    public void MoveLineDiagonally(LineRenderer lineRenderer, float gradient, float moveAmountX, float moveAmountY)
    {
        // Get the starting and ending points of the line
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        // Calculate the x and y distances to move the line based on the given gradient
        float distanceX = moveAmountX / Mathf.Sqrt(1 + gradient * gradient);
        float distanceY = gradient * distanceX;

        // Calculate the new positions of the line's starting and ending points based on the calculated distances
        Vector3 newStartPoint = new Vector3(startPoint.x + distanceX, startPoint.y + distanceY, startPoint.z);
        Vector3 newEndPoint = new Vector3(endPoint.x + distanceX, endPoint.y + distanceY, endPoint.z);

        // Update the positions of the line's starting and ending points
        lineRenderer.SetPosition(0, newStartPoint);
        lineRenderer.SetPosition(1, newEndPoint);
    }
}
