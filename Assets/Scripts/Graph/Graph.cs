using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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

    public float horizontalOffset = 0f; //Moves whole graph left/right
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
    public LineRenderer[] horizontalGridLine;
    public LineRenderer[] verticalGridLine;

    private Vector3[] horizontalGridLinePositions;
    private Vector3[] verticalGridLinePositions;

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

        //Initialise supply
        LineRenderer supplyLineRenderer = supplyLine.GetComponent<LineRenderer>();
        supplyLineRenderer.startWidth = curveThickness;
        supplyLineRenderer.endWidth = curveThickness;

        //Initalise demand
        LineRenderer demandLineRenderer = demandLine.GetComponent<LineRenderer>();
        demandLineRenderer.startWidth = curveThickness;
        demandLineRenderer.endWidth = curveThickness;

        //Set X-Axis line
        LineRenderer xAxisLineRenderer = xAxis.GetComponent<LineRenderer>();
        xAxisLineRenderer.startWidth = curveThickness;
        xAxisLineRenderer.endWidth = curveThickness;

        //Set Y-Axis line
        LineRenderer yAxisLineRenderer = yAxis.GetComponent<LineRenderer>();
        yAxisLineRenderer.startWidth = curveThickness;
        yAxisLineRenderer.endWidth = curveThickness;


        supplyLabel = GameObject.Find("Supply Label");
        demandLabel = GameObject.Find("Demand Label");

        //Only need to get data for one either supply/demand's GridLines as they start off in same position.
        verticalGridLinePositions = new Vector3[verticalGridLine[0].positionCount];
        verticalGridLine[0].GetPositions(verticalGridLinePositions);

        horizontalGridLinePositions = new Vector3[horizontalGridLine[0].positionCount];
        horizontalGridLine[0].GetPositions(horizontalGridLinePositions);

    }

    public void Update()
    {
        UpdateCurves(horizontalOffset, verticalOffset, supplyGradient, demandGradient);

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



        if (shiftType.Equals("RightwardShiftInSupply"))
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
        else if (shiftType.Equals("RightwardShiftInDemand"))
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
            supplyShifted = true;
            supplyShifting = true;
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
            supplyShifted = true;
            supplyShifting = true;
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

        //Center of the curve
        Vector3 centerOfGraph = demandLine.transform.position;

        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);//centre rotation

        Vector3[] supplyPositions = new Vector3[2];
        Vector3 localPosition = new Vector3(centerOfGraph.x + horizontalOffset, centerOfGraph.y + verticalOffset, 0f);

        float sangle = Mathf.Atan(supplyGradient) * Mathf.Rad2Deg;
        Quaternion stargetRotation = Quaternion.Euler(0, 0, sangle);

        Vector3 spivot = supplyLine.transform.position + new Vector3(horizontalOffset, verticalOffset, 0f);

        supplyLine.transform.RotateAround(spivot, Vector3.forward, sangle - supplyLine.transform.rotation.eulerAngles.z);
        supplyLine.transform.rotation = stargetRotation;


        Vector3[] demandPositions = new Vector3[2];
        Vector3 localPosition2 = new Vector3(-centerOfGraph.x + horizontalOffset, centerOfGraph.y + verticalOffset, 0f);


        float dangle = Mathf.Atan(demandGradient) * Mathf.Rad2Deg;
        Quaternion dtargetRotation = Quaternion.Euler(0, 0, dangle);
        if (supplyGradient < 0)
        {
            sangle += 180f;
        }

        Vector3 dpivot = demandLine.transform.position + new Vector3(horizontalOffset, verticalOffset, 0f);

        demandLine.transform.RotateAround(dpivot, Vector3.forward, dangle - demandLine.transform.rotation.eulerAngles.z);
        demandLine.transform.rotation = dtargetRotation;
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


        Transform intersectionContainer = demandLineContainer.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();

        LineRenderer demandLineRenderer = demandCurve.GetComponent<LineRenderer>();

        MoveLineDiagonally(demandLineRenderer, demandGradient, amount, amount, "demand", demandLabel);
        //Moveline1Diagonally(intersectionList[0], demandLineRenderer, "demandPrice");
        //Moveline1Diagonally(intersectionList[1], demandLineRenderer, "demandQuantity");

        //Put this into its own method later on...
        TextMesh[] GridLineLabels = intersectionContainer.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh label in GridLineLabels)
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

        Transform intersectionContainer = supplyLineContainer.transform.Find("Intersection Line Container");
        LineRenderer[] intersectionList = intersectionContainer.GetComponentsInChildren<LineRenderer>();
        LineRenderer supplyLineRenderer = supplyCurve.GetComponent<LineRenderer>();

        MoveLineDiagonally(supplyLineRenderer, supplyGradient, amount, amount, "supply", supplyLabel);
        Moveline1Diagonally(intersectionList[0], supplyLineRenderer, "supplyPrice");
        Moveline1Diagonally(intersectionList[1], supplyLineRenderer, "supplyQuantity");

        //Put this into its own method later on...
        TextMesh[] GridLineLabels = intersectionContainer.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh label in GridLineLabels)
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

    public void ShiftCurve(GameObject curve, float moveAmount)
    {
        Vector3 position = curve.transform.position; //move the y axis to shift diagonally.
        if (curve.transform.parent.tag.Equals("Supply"))
        {
            position.y -= moveAmount;
        }
        else
        {
            position.y += moveAmount;
        }
        
        position.x += moveAmount;
        curve.transform.position = position;
    }


    public void MoveLineDiagonally(LineRenderer lineRenderer, float gradient, float moveAmountX, float moveAmountY, string curveType, GameObject label)
    {
        ShiftCurve(lineRenderer.gameObject, moveAmountX);
        //Vector3 startPoint = lineRenderer.GetPosition(0);
        //Vector3 endPoint = lineRenderer.GetPosition(1);

        //// Calculate the x and y distances to move the line based on the given gradient
        //float distanceX = moveAmountX / Mathf.Sqrt(1 + gradient * gradient);
        //float distanceY = moveAmountY / Mathf.Sqrt(1 + gradient * gradient);

        //Vector3 newStartPoint = new Vector3(0,0,0);
        //Vector3 newEndPoint = new Vector3(0, 0, 0); ;
        //// Calculate the new positions of the line's starting and ending points based on the calculated distances
        //if (curveType.Equals("demand"))
        //{
        //    newStartPoint = new Vector3(startPoint.x + distanceX, startPoint.y + distanceY, startPoint.z);
        //    newEndPoint = new Vector3(endPoint.x + distanceX, endPoint.y + distanceY, endPoint.z);
        //}
        //else if (curveType.Equals("supply"))
        //{
        //    newStartPoint = new Vector3(startPoint.x + distanceX, startPoint.y - distanceY, startPoint.z);
        //    newEndPoint = new Vector3(endPoint.x + distanceX, endPoint.y - distanceY, endPoint.z);
        //}


        //// Update the positions of the line's starting and ending points
        //lineRenderer.SetPosition(0, newStartPoint);
        //lineRenderer.SetPosition(1, newEndPoint);

        //label.transform.position = newEndPoint;
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
