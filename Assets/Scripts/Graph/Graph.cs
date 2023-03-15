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

    public GameObject supplyLine;
    public GameObject demandLine;
    public GameObject xAxis;
    public GameObject yAxis;
    private GameObject xLabel;
    private GameObject yLabel;

    GameObject shiftedDemandCurve;
    public bool demandShift = false;

    void Start()
    {
        //Set supply
        LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();
        supplyLineRenderer.positionCount = 2;
        Vector3[] supplyPositions = new Vector3[2];
        supplyPositions[0] = new Vector3(minX, supplyIntercept, 0f); // first point
        supplyPositions[1] = new Vector3(maxX, maxY * supplySlope + supplyIntercept, 0f); // second point
        supplyLineRenderer.SetPositions(supplyPositions);

        //Set demand
        LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
        demandLineRenderer.positionCount = 2;
        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(minX, demandIntercept, 0f);
        demandPositions[1] = new Vector3(maxX, maxY * demandSlope + demandIntercept, 0f); // second point
        demandLineRenderer.SetPositions(demandPositions);

        //Set X-Axis line
        LineRenderer xAxisLineRenderer = xAxis.GetComponent<LineRenderer>();
        xAxisLineRenderer.positionCount = 2;
        Vector3[] xAxisPositions = new Vector3[2];
        xAxisPositions[0] = new Vector3(minX, 0f, 0f);
        xAxisPositions[1] = new Vector3(maxX, 0f, 0f);
        xAxisLineRenderer.SetPositions(xAxisPositions);
        xAxisLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        xAxisLineRenderer.startWidth = 1f;
        xAxisLineRenderer.endWidth = 1f;

        //Set Y-Axis line
        LineRenderer yAxisLineRenderer = yAxis.GetComponent<LineRenderer>();
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
