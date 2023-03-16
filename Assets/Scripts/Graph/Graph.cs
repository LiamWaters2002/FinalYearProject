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
    public float supplyGradient = 1f; //Check unity inspector
    public float demandIntercept = 50f; //Check unity inspector
    public float demandGradient = -1f; //Check unity inspector

    public float horizontalOffset = 30f; //Moves whole graph left/right
    public float verticalOffset = 30f; //Moves whole graph up/down
    public float axisOffset = 30f; //Moves just the axis

    public float curveThickness;

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
        supplyLineRenderer.startWidth = curveThickness;
        supplyLineRenderer.endWidth = curveThickness;
        supplyLineRenderer.positionCount = 2;
        Vector3[] supplyPositions = new Vector3[2];
        supplyPositions[0] = new Vector3(minX, supplyIntercept, 0f); // first point
        supplyPositions[1] = new Vector3(maxX, maxY * supplyGradient + supplyIntercept, 0f); // second point
        supplyLineRenderer.SetPositions(supplyPositions);

        //Set demand
        LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
        demandLineRenderer.startWidth = curveThickness;
        demandLineRenderer.endWidth = curveThickness;
        demandLineRenderer.positionCount = 2;
        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(minX, demandIntercept, 0f);
        demandPositions[1] = new Vector3(maxX, maxY * demandGradient + demandIntercept, 0f); // second point
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
        UpdateCurves(horizontalOffset, verticalOffset, supplyGradient, demandGradient);
        UpdateXAxis(scaleXAxis, horizontalOffset, verticalOffset);
        UpdateYAxis(scaleYAxis, horizontalOffset, verticalOffset);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!demandShift)
            {
                shiftedDemandCurve = Instantiate(demandLine);
                demandShift = true;
            }
            ShiftDemand(shiftedDemandCurve, -30);
        }

        //CreateIntersectionLines(horizontalOffset, verticalOffset);
    }

    public void UpdateCurves(float horizontalOffset, float verticalOffset, float supplyGradient, float demandGradient)
    {
        Vector3 center = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f, 0f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);//centre rotation

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
        horizontalOffset = horizontalOffset - axisOffset;
        verticalOffset = verticalOffset - axisOffset;

        LineRenderer xAxisLineRenderer = xAxis.GetComponent<LineRenderer>();
        Vector3[] xAxisPositions = new Vector3[2];
        xAxisPositions[0] = new Vector3(minX * scaleXAxis + horizontalOffset, verticalOffset, 0f);
        xAxisPositions[1] = new Vector3(maxX * scaleXAxis + horizontalOffset, verticalOffset, 0f);
        xAxisLineRenderer.SetPositions(xAxisPositions);
    }

    void UpdateYAxis(float scaleYAxis, float horizontalOffset, float verticalOffset)
    {
        horizontalOffset = horizontalOffset - axisOffset;
        verticalOffset = verticalOffset - axisOffset;

        LineRenderer yAxisLineRenderer = yAxis.GetComponent<LineRenderer>();
        Vector3[] yAxisPositions = new Vector3[2];
        yAxisPositions[0] = new Vector3(horizontalOffset, minY * scaleYAxis + verticalOffset, 0f);
        yAxisPositions[1] = new Vector3(horizontalOffset, maxY * scaleYAxis + verticalOffset, 0f);
        yAxisLineRenderer.SetPositions(yAxisPositions);
    }

    void ShiftDemand(GameObject shiftedDemandCurve, int amount)
    {
        Transform intersectionContainer = shiftedDemandCurve.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();




        LineRenderer demandCurve = shiftedDemandCurve.GetComponent<LineRenderer>();
        MoveLineDiagonally(demandCurve, demandGradient, amount, amount);
        MoveIntersectionLineDiagonally(intersectionList[0], demandCurve, "demand2");
        MoveIntersectionLineDiagonally(intersectionList[1], demandCurve, "demand1");
    }

    void ShiftSupply(string direction)
    {
        GameObject shiftedSupplyCurve = Instantiate(supplyLine);
        LineRenderer lineRenderer = shiftedSupplyCurve.GetComponent<LineRenderer>();
        MoveLineDiagonally(lineRenderer, supplyGradient, -30, -30);
    }

    public void MoveLineDiagonally(LineRenderer lineRenderer, float gradient, float moveAmountX, float moveAmountY)
    {
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        // Calculate the x and y distances to move the line based on the given gradient
        float distanceX = moveAmountX / Mathf.Sqrt(1 + gradient * gradient);
        float distanceY = moveAmountY / Mathf.Sqrt(1 + gradient * gradient);

        // Calculate the new positions of the line's starting and ending points based on the calculated distances
        Vector3 newStartPoint = new Vector3(startPoint.x + distanceX, startPoint.y + distanceY, startPoint.z);
        Vector3 newEndPoint = new Vector3(endPoint.x + distanceX, endPoint.y + distanceY, endPoint.z);

        // Update the positions of the line's starting and ending points
        lineRenderer.SetPosition(0, newStartPoint);
        lineRenderer.SetPosition(1, newEndPoint);
    }

    public void MoveIntersectionLineDiagonally(LineRenderer intersectionLine, LineRenderer newCurve, string curveType)
    {
        Vector3 intersectionLineStart = intersectionLine.GetPosition(0);
        Vector3 intersectionLineEnd = intersectionLine.GetPosition(1);

        Vector3 newCurveStart = intersectionLine.GetPosition(0);
        Vector3 NewCurveLineEnd = intersectionLine.GetPosition(1);

        if (curveType.Equals("demand1"))
        {
            LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();
            Vector3 supplyLineStart = supplyLineRenderer.GetPosition(0);
            Vector3 supplyLineEnd = supplyLineRenderer.GetPosition(1);
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, supplyLineRenderer);
            intersectionLine.SetPosition(0, GetIntersectionPoint(newCurve, supplyLineRenderer));
            intersectionLineEnd.x = intersectionPoint.x;
            intersectionLine.SetPosition(1, intersectionLineEnd);


        }
        if (curveType.Equals("demand2"))
        {
            LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();
            Vector3 supplyLineStart = supplyLineRenderer.GetPosition(0);
            Vector3 supplyLineEnd = supplyLineRenderer.GetPosition(1);
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, supplyLineRenderer);
            intersectionLine.SetPosition(0, GetIntersectionPoint(newCurve, supplyLineRenderer));
            intersectionLineEnd.y = intersectionPoint.y;
            intersectionLine.SetPosition(1, intersectionLineEnd);


        }
        else if (curveType.Equals("supply"))
        {
            LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
            Vector3 demandLineStart = demandLineRenderer.GetPosition(0);
            Vector3 demandLineEnd = demandLineRenderer.GetPosition(1);
            intersectionLine.SetPosition(0, GetIntersectionPoint(newCurve, demandLineRenderer));
        }
    }

    public Vector3 GetIntersectionPoint(LineRenderer intersectionLine, LineRenderer otherLine)
    {
        Vector3 intersection = Vector3.zero;
        Vector3 intersectionLineStart = intersectionLine.GetPosition(0);
        Vector3 intersectionLineEnd = intersectionLine.GetPosition(1);
        Vector3 otherLineStart = otherLine.GetPosition(0);
        Vector3 otherLineEnd = otherLine.GetPosition(1);
        float a1 = intersectionLineEnd.y - intersectionLineStart.y;
        float b1 = intersectionLineStart.x - intersectionLineEnd.x;
        float c1 = a1 * intersectionLineStart.x + b1 * intersectionLineStart.y;
        float a2 = otherLineEnd.y - otherLineStart.y;
        float b2 = otherLineStart.x - otherLineEnd.x;
        float c2 = a2 * otherLineStart.x + b2 * otherLineStart.y;
        float delta = a1 * b2 - a2 * b1;
        if (delta == 0)
        {
            Debug.Log("Lines are parallel");
        }
        else
        {
            intersection.x = (b2 * c1 - b1 * c2) / delta;
            intersection.y = (a1 * c2 - a2 * c1) / delta;
        }
        return intersection;
    }
}
