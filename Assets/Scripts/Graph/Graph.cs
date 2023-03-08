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

    public float horizontalOffset = 30f; //Moves all curves left/right
    public float verticalOffset = 30f; //Moves all curves up/down

    private GameObject supplyLine;
    private GameObject demandLine;
    private GameObject xLabel;
    private GameObject yLabel;

    GameObject shiftedDemandCurve;
    public bool demandShift = false;

    void Start()
    {
        // Price (y) = Gradient of Supply (m) x Quantity Supplied (x) + Minimum Price (c)
        // Price (y) = Gradient of Demand (m) x Quantity Demanded (x) + Maximum Price (c)

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

        // Calculate the y-coordinate of the second point of the demand curve
        float demandY = demandSlope * (maxX) + demandIntercept;

        // Adjust the y-coordinate to make the demand curve more or less inelastic
        demandY *= 0f - demandSlope;

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

    public void Update()
    {
        UpdateCurves(horizontalOffset, verticalOffset, supplySlope, demandSlope);
        UpdateXAxis(scaleXAxis);
        UpdateYAxis(scaleYAxis);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!demandShift)
            {
                shiftedDemandCurve = Instantiate(demandLine);
                demandShift = true;
            }
            ShiftDemand(shiftedDemandCurve);
        }

        
    }

    public void UpdateCurves(float horizontalOffset, float verticalOffset, float supplySlope, float demandSlope)
    {
        Vector3[] supplyPositions = new Vector3[2];
        supplyPositions[0] = new Vector3(minX + horizontalOffset, supplyIntercept + verticalOffset, 0f); // first point
        supplyPositions[1] = new Vector3(maxX + horizontalOffset, maxY * supplySlope + supplyIntercept + verticalOffset, 0f); // second point

        supplyLine.GetComponent<LineRenderer>().SetPositions(supplyPositions);


        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(minX + horizontalOffset, demandIntercept + verticalOffset, 0f); // first point
        demandPositions[1] = new Vector3(maxX + horizontalOffset, maxY * demandSlope + demandIntercept + verticalOffset, 0f); // second point

        demandLine.GetComponent<LineRenderer>().SetPositions(demandPositions);
    }

    void UpdateXAxis(float scaleXAxis)
    {
        GameObject xAxis = GameObject.Find("X Axis");
        LineRenderer xAxisLineRenderer = xAxis.GetComponent<LineRenderer>();
        Vector3[] xAxisPositions = new Vector3[2];
        xAxisPositions[0] = new Vector3(minX * scaleXAxis, 0f, 0f);
        xAxisPositions[1] = new Vector3(maxX * scaleXAxis, 0f, 0f);
        xAxisLineRenderer.SetPositions(xAxisPositions);
    }

    void UpdateYAxis(float scaleYAxis)
    {
        GameObject yAxis = GameObject.Find("Y Axis");
        LineRenderer yAxisLineRenderer = yAxis.GetComponent<LineRenderer>();
        Vector3[] yAxisPositions = new Vector3[2];
        yAxisPositions[0] = new Vector3(0f, minY * scaleYAxis, 0f);
        yAxisPositions[1] = new Vector3(0f, maxY * scaleYAxis, 0f);
        yAxisLineRenderer.SetPositions(yAxisPositions);
    }

    void ShiftDemand(GameObject shiftedDemandCurve)
    {
        LineRenderer lineRenderer = shiftedDemandCurve.GetComponent<LineRenderer>();
        MoveLineDiagonally(lineRenderer, demandSlope, 100, 100);
    }

    void ShiftSupply(string direction)
    {
        GameObject shiftedSupplyCurve = Instantiate(supplyLine);
        LineRenderer lineRenderer = shiftedSupplyCurve.GetComponent<LineRenderer>();
        MoveLineDiagonally(lineRenderer, supplySlope, -100, -100);
    }

    public void MoveLineDiagonally(LineRenderer lineRenderer, float gradient, float moveAmountX, float moveAmountY)
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        Vector3 direction = (positions[1] - positions[0]).normalized;
        Vector3 gradientVector = new Vector3(1, gradient, 0).normalized;
        Vector3 movementVector = direction * moveAmountX - gradientVector * moveAmountY;

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] += movementVector;
        }

        lineRenderer.SetPositions(positions);
    }
}
