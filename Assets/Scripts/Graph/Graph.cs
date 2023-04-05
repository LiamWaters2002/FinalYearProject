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

    public bool demandShifted = false;
    public bool supplyShifted = false;
    public bool demandShifting = false;
    public bool supplyShifting = false;

    public float timer = 0;
    public float duration = 0.01f;

    public string shiftType = "";

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
            if (!demandShifted)
            {
                shiftedDemandCurve = Instantiate(demandLineContainer);
                shiftedDemandCurve.transform.parent = this.transform; //Make curve parent of the original
                demandShifted = true;
                demandShifting = true;
            }
            demandShifted = true;
            demandShifting = true;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!supplyShifted && !supplyShifting)
            {
                shiftedSupplyCurve = Instantiate(supplyLineContainer);
                shiftedSupplyCurve.transform.parent = this.transform;
            }
            supplyShifted = true;
            supplyShifting = true;
        }

        //if (!shiftType.Equals(""))
        //{
        //    if (!supplyShifted && !supplyShifting)
        //    {
        //        shiftedSupplyCurve = Instantiate(supplyLineContainer);
        //        shiftedSupplyCurve.transform.parent = this.transform;
        //    }
        //    supplyShifted = true;
        //    supplyShifting = true;
        //}



        if (supplyShifting || shiftType.Equals("RightwardShiftInSupply"))
        {
            timer += Time.deltaTime * 10;
            if(timer < duration)
            {
                ShiftSupply(shiftedSupplyCurve, 1); //negative - left, positive - right
            }
            else
            {
                timer = 0;
                supplyShifting = false;
                shiftType = "";
            }
        }
        else if (demandShifting || shiftType.Equals("RightwardShiftInDemand"))
        {
            timer += Time.deltaTime * 10;
            if (timer < duration)
            {
                ShiftDemand(shiftedDemandCurve, 1); //negative - left, positive - right
            }
            else
            {
                timer = 0;
                demandShifting = false;
                shiftType = "";
            }
        }
        else if (supplyShifting || shiftType.Equals("LeftwardShiftInSupply"))
        {
            timer += Time.deltaTime * 10;
            if (timer < duration)
            {
                ShiftSupply(shiftedSupplyCurve, -1); //negative - left, positive - right
            }
            else
            {
                timer = 0;
                supplyShifting = false;
                shiftType = "";
            }
        }
        else if (demandShifting || shiftType.Equals("LeftwardShiftInDemand"))
        {
            timer += Time.deltaTime * 10;
            if (timer < duration)
            {
                ShiftDemand(shiftedDemandCurve, -1); //negative - left, positive - right
            }
            else
            {
                timer = 0;
                demandShifting = false;
                shiftType = "";
            }
        }

        //Createline1s(horizontalOffset, verticalOffset);
    }

    public void RightwardShiftInSupply()
    {
        shiftType = "RightwardShiftInSupply";
        if (!supplyShifted && !supplyShifting)
        {
            shiftedSupplyCurve = Instantiate(supplyLineContainer);
            shiftedSupplyCurve.transform.parent = this.transform;
        }
        supplyShifted = true;
    }

    public void LeftwardShiftInSupply()
    {
        shiftType = "LeftwardShiftInSupply";
        if (!supplyShifted && !supplyShifting)
        {
            shiftedSupplyCurve = Instantiate(supplyLineContainer);
            shiftedSupplyCurve.transform.parent = this.transform;
        }
        supplyShifted = true;
    }

    public void RightwardShiftInDemand()
    {
        shiftType = "RightwardShiftInDemand";

        if (!demandShifted)
        {
            shiftedDemandCurve = Instantiate(demandLineContainer);
            shiftedDemandCurve.transform.parent = this.transform; //Make curve parent of the original
            demandShifted = true;
            demandShifting = true;
        }
        demandShifted = true;
    }

    public void LeftwardShiftInDemand()
    {
        shiftType = "LeftwardShiftInDemand";

        if (!demandShifted)
        {
            shiftedDemandCurve = Instantiate(demandLineContainer);
            shiftedDemandCurve.transform.parent = this.transform; //Make curve parent of the original
            demandShifted = true;
            demandShifting = true;
        }
        demandShifted = true;
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

    /// <summary>
    /// Shifts a particular curve, and its labels
    /// </summary>
    /// <param name="demandLineContainer"></param>
    /// <param name="amount"></param>
    void ShiftDemand(GameObject demandLineContainer, int amount)
    {
        Transform demandCurve = demandLineContainer.transform.Find("Demand Line");

        GameObject demandLabel = demandLineContainer.transform.Find("Demand Label").gameObject;

        TextMesh demandLabelText = demandLabel.GetComponent<TextMesh>();

        int demandLineCount = -1; //We exclude the initial demand line 

        //for each child in graph, count all demand lines...
        foreach (Transform child in this.transform)
        {
            if (child.CompareTag("Demand"))
            {
                demandLineCount++;
            }
        }
        demandLabelText.text = "D" + demandLineCount;


        Transform intersectionContainer = demandCurve.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();

        LineRenderer demandLineRenderer = demandCurve.GetComponent<LineRenderer>();

        MoveLineDiagonally(demandLineRenderer, demandGradient, amount, amount, "demand", demandLabel);
        Moveline1Diagonally(intersectionList[0], demandLineRenderer, "demandPrice");
        Moveline1Diagonally(intersectionList[1], demandLineRenderer, "demandQuantity");

        //Put this into its own method later on...
        TextMesh[] readLineLabels = intersectionContainer.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh label in readLineLabels)
        {
            //Labels with no numbers for learning
            if (char.IsLetter(label.text[0]) && label.text.Length == 1)
            {
                int supplyLineCount = -1;
                foreach (Transform child in this.transform)
                {
                    if (child.CompareTag("Supply"))
                    {
                        supplyLineCount++;
                    }
                }
                label.text = label.text + (demandLineCount + supplyLineCount).ToString();
            }
            
        }
    }

    void ShiftSupply(GameObject supplyLineContainer, int amount)
    {
        Transform supplyCurve = supplyLineContainer.transform.Find("Supply Line");

        GameObject supplyLabel = supplyLineContainer.transform.Find("Supply Label").gameObject;

        TextMesh supplyLabelText = supplyLabel.GetComponent<TextMesh>();

        int supplyLineCount = -1;

        //for each child in graph, count all supply lines...
        foreach (Transform child in this.transform)
        {
            if (child.CompareTag("Supply"))
            {
                supplyLineCount++;
            }
        }
        supplyLabelText.text = "S" + supplyLineCount;

        Transform intersectionContainer = supplyCurve.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();

        LineRenderer supplyLineRenderer = supplyCurve.GetComponent<LineRenderer>();

        MoveLineDiagonally(supplyLineRenderer, supplyGradient, amount, amount, "supply", supplyLabel);
        Moveline1Diagonally(intersectionList[0], supplyLineRenderer, "supplyPrice");
        Moveline1Diagonally(intersectionList[1], supplyLineRenderer, "supplyQuantity");

        //Put this into its own method later on...
        TextMesh[] readLineLabels = intersectionContainer.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh label in readLineLabels)
        {
            //Labels with no numbers for learning
            if (char.IsLetter(label.text[0]) && label.text.Length == 1)
            {
                int demandLineCount = -1;
                foreach (Transform child in this.transform)
                {
                    if (child.CompareTag("Demand"))
                    {
                        demandLineCount++;
                    }
                }
                label.text = label.text + (demandLineCount + supplyLineCount).ToString();
            }

        }
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

        TextMesh label = line1.GetComponentInChildren<TextMesh>(); //Get label of line1

        if (curveType.Equals("demandQuantity"))//demand1
        {
            LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();

            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, supplyLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, supplyLineRenderer));
            line1End.x = intersectionPoint.x;
            line1.SetPosition(1, line1End);

            label.transform.position = line1End + new Vector3(0, -7 ,0);  //alignment


        }
        if (curveType.Equals("demandPrice"))//demand2
        {
            LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();

            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, supplyLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, supplyLineRenderer));
            line1End.y = intersectionPoint.y;
            line1.SetPosition(1, line1End);

            label.transform.position = line1End + new Vector3(-2, 0, 0);
        }
        if (curveType.Equals("supplyQuantity"))
        {
            LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, demandLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, demandLineRenderer));
            line1End.x = intersectionPoint.x;
            line1.SetPosition(1, line1End);

            label.transform.position = line1End + new Vector3(0, -7, 0);  //alignment

        }
        if (curveType.Equals("supplyPrice"))
        {
            LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
            Vector3 intersectionPoint = GetIntersectionPoint(newCurve, demandLineRenderer);
            line1.SetPosition(0, GetIntersectionPoint(newCurve, demandLineRenderer));
            //line1Start.y = intersectionPoint.y;
            line1End.y = intersectionPoint.y;
            line1.SetPosition(1, line1End);
            label.transform.position = line1End + new Vector3(-2, 0, 0);
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
