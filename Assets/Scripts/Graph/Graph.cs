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

    public GameObject demandLineContainer;
    public GameObject supplyLineContainer;
    public GameObject supplyLine;
    public GameObject demandLine;
    public GameObject xAxis;
    public GameObject yAxis;
    private GameObject supplyLabel;
    private GameObject demandLabel;
    public LineRenderer[] horizontalReadLine;
    public LineRenderer[] verticalReadLine;

    private Vector3[] horizontalReadLinePositions;
    private Vector3[] verticalReadLinePositions;

    GameObject shiftedDemandCurve;
    GameObject shiftedSupplyCurve;
    public bool demandShift = false;
    public bool supplyShift = false;

    void Start()
    {

        supplyLine = supplyLineContainer.transform.Find("Supply Line").gameObject;
        demandLine = demandLineContainer.transform.Find("Demand Line").gameObject;
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

        supplyLabel = GameObject.Find("Supply Label");
        demandLabel = GameObject.Find("Demand Label");

        //Only need to get data for one either supply/demand's readlines as they start off in same position.
        verticalReadLinePositions = new Vector3[verticalReadLine[0].positionCount];
        verticalReadLine[0].GetPositions(verticalReadLinePositions);
        horizontalReadLinePositions = new Vector3[horizontalReadLine[0].positionCount];
        horizontalReadLine[0].GetPositions(horizontalReadLinePositions);

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
                shiftedDemandCurve = Instantiate(demandLineContainer);
                shiftedDemandCurve.transform.parent = this.transform;
                demandShift = true;
            }
            ShiftDemand(shiftedDemandCurve, 30); //negative - left, positive - right
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!supplyShift)
            {
                shiftedSupplyCurve = Instantiate(supplyLineContainer);
                supplyShift = true;
            }
            ShiftSupply(shiftedSupplyCurve, -30); //negative - left, positive - right
        }

        //Createline1s(horizontalOffset, verticalOffset);
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

        supplyLabel.transform.position = supplyLine.GetComponent<LineRenderer>().GetPosition(1);


        Vector3[] demandPositions = new Vector3[2];
        demandPositions[0] = new Vector3(-center.x + horizontalOffset, center.y * demandGradient + verticalOffset, 0f); // first point
        demandPositions[1] = new Vector3(center.x + horizontalOffset, -center.y * demandGradient + verticalOffset, 0f); // second point
        demandPositions[0] = rotation * demandPositions[0] + center;
        demandPositions[1] = rotation * demandPositions[1] + center;

        demandLine.GetComponent<LineRenderer>().SetPositions(demandPositions);

        //demandLabel = demandLine.transform.Find("Demand Label");

        Vector3 supplyLabelPosition = supplyPositions[1] + new Vector3(5, 10, 0);
        Vector3 demandLabelPosition = demandPositions[1] + new Vector3(5, 2, 0);

        demandLabel.transform.position = demandLabelPosition;
        supplyLabel.transform.position = supplyLabelPosition;

        Vector3[] updatedHorizontalReadLinePositions = new Vector3[horizontalReadLine[0].positionCount];
        Vector3[] updatedVerticalReadLinePositions = new Vector3[verticalReadLine[0].positionCount];

        // Update the positions using the horizontal and vertical offsets for the read lines...
        updatedHorizontalReadLinePositions[0] = new Vector3(horizontalReadLinePositions[0].x + horizontalOffset, horizontalReadLinePositions[0].y + verticalOffset, 0f);
        updatedHorizontalReadLinePositions[1] = new Vector3(horizontalReadLinePositions[1].x + horizontalOffset, horizontalReadLinePositions[1].y + verticalOffset, 0f);

        updatedVerticalReadLinePositions[0] = new Vector3(verticalReadLinePositions[0].x + horizontalOffset, verticalReadLinePositions[0].y + verticalOffset, 0f);
        updatedVerticalReadLinePositions[1] = new Vector3(verticalReadLinePositions[1].x + horizontalOffset, verticalReadLinePositions[1].y + verticalOffset, 0f);

        // Set the updated positions back to the line renderer
        horizontalReadLine[0].SetPositions(updatedHorizontalReadLinePositions);
        verticalReadLine[0].SetPositions(updatedVerticalReadLinePositions);
        horizontalReadLine[1].SetPositions(updatedHorizontalReadLinePositions);
        verticalReadLine[1].SetPositions(updatedVerticalReadLinePositions);
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

    void ShiftDemand(GameObject demandLineContainer, int amount)
    {
        Transform demandCurve = demandLineContainer.transform.Find("Demand Line");

        GameObject demandLabel = demandLineContainer.transform.Find("Demand Label").gameObject;
        TextMesh demandLabelText = demandLabel.GetComponent<TextMesh>();

        int demandLineCount = -1; //We exclude the initial demand line 

        //for each child in graph
        foreach (Transform child in this.transform)
        {
            if (child.CompareTag("Demand"))
            {
                demandLineCount++;
            }
        }
        demandLabelText.text = demandLabelText.text + demandLineCount;


        Transform intersectionContainer = demandCurve.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();

        LineRenderer demandLineRenderer = demandCurve.GetComponent<LineRenderer>();

        MoveLineDiagonally(demandLineRenderer, demandGradient, amount, amount, "demand", demandLabel);
        Moveline1Diagonally(intersectionList[0], demandLineRenderer, "demand2");
        Moveline1Diagonally(intersectionList[1], demandLineRenderer, "demand1");
    }

    void ShiftSupply(GameObject shiftedSupplyCurve, int amount)
    {
        Transform intersectionContainer = shiftedSupplyCurve.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();

        LineRenderer supplyCurve = shiftedSupplyCurve.GetComponent<LineRenderer>();

        MoveLineDiagonally(supplyCurve, supplyGradient, amount, amount, "supply", supplyLabel);
        Moveline1Diagonally(intersectionList[0], supplyCurve, "supply2");
        Moveline1Diagonally(intersectionList[1], supplyCurve, "supply1");
    }

    public void MoveLineDiagonally(LineRenderer lineRenderer, float gradient, float moveAmountX, float moveAmountY, string curveType, GameObject label)
    {
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        // Calculate the x and y distances to move the line based on the given gradient
        float distanceX = moveAmountX / Mathf.Sqrt(1 + gradient * gradient);
        float distanceY = moveAmountY / Mathf.Sqrt(1 + gradient * gradient);

        Vector3 newStartPoint = new Vector3(0,0,0);
        Vector3 newEndPoint = new Vector3(0, 0, 0); ;
        // Calculate the new positions of the line's starting and ending points based on the calculated distances
        if (curveType.Equals("demand"))
        {
            newStartPoint = new Vector3(startPoint.x + distanceX, startPoint.y + distanceY, startPoint.z);
            newEndPoint = new Vector3(endPoint.x + distanceX, endPoint.y + distanceY, endPoint.z);
        }
        else if (curveType.Equals("supply"))
        {
            newStartPoint = new Vector3(startPoint.x + distanceX, startPoint.y - distanceY, startPoint.z);
            newEndPoint = new Vector3(endPoint.x + distanceX, endPoint.y - distanceY, endPoint.z);
        }


        // Update the positions of the line's starting and ending points
        lineRenderer.SetPosition(0, newStartPoint);
        lineRenderer.SetPosition(1, newEndPoint);

        label.transform.position = newEndPoint;
    }

    public void Moveline1Diagonally(LineRenderer line1, LineRenderer newCurve, string curveType)
    {
        Vector3 line1Start = line1.GetPosition(0);
        Vector3 line1End = line1.GetPosition(1);

        Vector3 newCurveStart = line1.GetPosition(0);
        Vector3 NewCurveLineEnd = line1.GetPosition(1);

        if (curveType.Equals("demand1"))
        {
            LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, supplyLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, supplyLineRenderer));
            line1End.x = intersectionPoint.x;
            line1.SetPosition(1, line1End);


        }
        if (curveType.Equals("demand2"))
        {
            LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, supplyLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, supplyLineRenderer));
            line1End.y = intersectionPoint.y;
            line1.SetPosition(1, line1End);


        }
        if (curveType.Equals("supply1"))
        {
            LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, demandLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, demandLineRenderer));
            line1End.x = intersectionPoint.x;
            line1.SetPosition(1, line1End);


        }
        if (curveType.Equals("supply2"))
        {
            LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, demandLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, demandLineRenderer));
            //line1Start.y = intersectionPoint.y;
            line1End.y = intersectionPoint.y;
            line1.SetPosition(1, line1End);


        }
    }

    public Vector3 GetIntersectionPoint(LineRenderer line1, LineRenderer line2)
    {
        Vector3 intersection = new Vector3(0, 0, 0);

        Vector3 line1Start = line1.GetPosition(0);
        Vector3 line1End = line1.GetPosition(1);

        Vector3 line2Start = line2.GetPosition(0);
        Vector3 line2End = line2.GetPosition(1);

        float m1 = (line1End.y - line1Start.y) / (line1End.x - line1Start.x); //(y2 - y1) / (x2 - x1)
        float c1 = line1Start.y - m1 * line1Start.x; // y = mx + c -> y - mx = c

        float m2 = (line2End.y - line2Start.y) / (line2End.x - line2Start.x);
        float c2 = line2Start.y - m2 * line2Start.x;

        float x = (c2 - c1) / (m1 - m2); // mx = c -> x = c/m

        float y = (m1 * x) + c1; //plug in x

        intersection = new Vector3(x, y, 0);

        return intersection;
    }
}
