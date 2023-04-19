using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    public float supplyGradient = 1f; //Check unity inspector
    public float demandGradient = -1f; //Check unity inspector

    public float horizontalOffset = 0f; //Moves whole graph left/right
    public float verticalOffset = 30f; //Moves whole graph up/down
    public float axisOffset = 30f; //Moves just the axis

    public float curveThickness;

    public TextMesh quantityLabel;
    public TextMesh priceLabel;
    public float quantity;
    public float price;

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
    private bool thisShiftHappeneed;
    Coroutine coroutine;

    public GameObject graphContainer;

    void Start()
    {

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
        UpdateLabelPosition(demandLine.GetComponent<LineRenderer>(), demandLabel.GetComponent<TextMesh>(), 0);
        UpdateLabelPosition(supplyLine.GetComponent<LineRenderer>(), supplyLabel.GetComponent<TextMesh>(), 0);
       
        //if ()
        //{

        //}
        

        if (shiftType != "")
        {
            CheckShiftType();
        }

        ////Test if individual market shift works...
        //if(this.transform.name.Equals("Orange Market") && !thisShiftHappeneed)
        //{
        //    LeftwardShiftInDemand();
        //    thisShiftHappeneed = true;
        //}
    }

    public void CheckShiftType()
    {
        if (shiftType.Equals("RightwardShiftInSupply"))
        {
            shiftType = "";
            ShiftSupply(shiftedSupplyCurve, 10); //negative - left, positive - right
            
        }
        else if (shiftType.Equals("RightwardShiftInDemand"))
        {
            shiftType = "";
            ShiftDemand(shiftedDemandCurve, 10); //negative - left, positive - right
        }
        else if (shiftType.Equals("LeftwardShiftInSupply"))
        {
            shiftType = "";
            ShiftSupply(shiftedSupplyCurve, -10); //negative - left, positive - right
        }
        else if (shiftType.Equals("LeftwardShiftInDemand"))
        {
            shiftType = "";
            ShiftDemand(shiftedDemandCurve, -10); //negative - left, positive - right
        }
    }


    public void RightwardShiftInSupply()
    {
        shiftType = "RightwardShiftInSupply";
        if (!supplyShifted && !supplyShifting)
        {
            shiftedSupplyCurve = Instantiate(supplyLineContainer, this.transform); //Must be at the this.transform position, or it will go to reference...
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
            shiftedSupplyCurve = Instantiate(supplyLineContainer, this.transform);
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
            shiftedDemandCurve = Instantiate(demandLineContainer, this.transform);
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
            shiftedDemandCurve = Instantiate(demandLineContainer, this.transform);
            shiftedDemandCurve.transform.parent = this.transform; //Make curve parent of the original
            demandShifted = true;
            demandShifting = true;
        }
        demandShifted = true;
    }


    public void UpdateCurves(float horizontalOffset, float verticalOffset, float supplyGradient, float demandGradient)
    {
        float sangle = Mathf.Atan(supplyGradient) * Mathf.Rad2Deg;
        Quaternion stargetRotation = Quaternion.Euler(0, 0, sangle);

        Vector3 spivot = supplyLine.transform.position + new Vector3(horizontalOffset, verticalOffset, 0f);

        supplyLine.transform.RotateAround(spivot, Vector3.forward, sangle - supplyLine.transform.rotation.eulerAngles.z);
        supplyLine.transform.rotation = stargetRotation;

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

    public void UpdateLabelPosition(LineRenderer line, TextMesh label, int position)
    {
        Vector3 offset = new Vector3(10, 10, 0);
        label.transform.position = offset + line.transform.TransformPoint(line.GetPosition(position));
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


        Transform gridLineContainer = demandLineContainer.transform.Find("Grid Line Container");
        LineRenderer[] gridLineList = gridLineContainer.GetComponentsInChildren<LineRenderer>();

        LineRenderer demandLineRenderer = demandCurve.GetComponent<LineRenderer>();

        coroutine = StartCoroutine(ShiftCurve(demandCurve, amount, gridLineContainer));

        //Put this into its own method later on...
        TextMesh[] GridLineLabels = gridLineContainer.transform.GetComponentsInChildren<TextMesh>();
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
        Debug.Log("This was executed");
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

        Transform gridLineContainer = supplyLineContainer.transform.Find("Grid Line Container");
        LineRenderer[] gridLineList = gridLineContainer.GetComponentsInChildren<LineRenderer>();
        LineRenderer supplyLineRenderer = supplyCurve.GetComponent<LineRenderer>();

        coroutine = StartCoroutine(ShiftCurve(supplyCurve, amount, gridLineContainer));

        //Put this into its own method later on...
        TextMesh[] GridLineLabels = gridLineContainer.transform.GetComponentsInChildren<TextMesh>();
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

    IEnumerator ShiftCurve(Transform curve, int moveAmount, Transform gridLineContainer)
    {

        int totalMovement = Math.Abs(moveAmount);
        int unit = 1;
        if(moveAmount < 0)
        {
            unit = -1;
        }

        for (int i = 0; i < totalMovement; i++)
        {
            TextMesh label = new TextMesh();
            Vector3 position = curve.transform.position; //move the y axis to shift diagonally.
            if (curve.transform.parent.tag.Equals("Supply"))
            {
                position.x += unit;
                position.y -= unit;
                label = curve.transform.parent.Find("Supply Label").GetComponent<TextMesh>();
            }
            else if (curve.parent.tag.Equals("Demand"))
            {
                position.x += unit;
                position.y += unit;
                label = curve.transform.parent.Find("Demand Label").GetComponent<TextMesh>();
            }
            curve.transform.position = position;

            MoveGridLine(gridLineContainer, curve);

            UpdateLabelPosition(curve.GetComponent<LineRenderer>(), label, 0);

            yield return new WaitForSeconds(0.01f); // wait for 1 second
        }
        StopCoroutine(coroutine);
    }

    public void MoveGridLine(Transform gridLineContainer, Transform curve)
    {
        Vector3 gridLinePoint = new Vector3(0,0,0);

        if (curve.parent.tag.Equals("Supply"))
        {
            gridLinePoint = GetgridLinePoint(demandLine.GetComponent<LineRenderer>(), curve.GetComponent<LineRenderer>());
            Debug.Log("This curve will intersect the Demand Curve");


        }
        else if (curve.parent.tag.Equals("Demand"))
        {
            gridLinePoint = GetgridLinePoint(supplyLine.GetComponent<LineRenderer>(), curve.GetComponent<LineRenderer>());
            Debug.Log("This curve will intersect the Supply Curve");
        }

        LineRenderer[] gridLineRendererContainer = gridLineContainer.GetComponentsInChildren<LineRenderer>();

        foreach (LineRenderer gridLineRenderer in gridLineRendererContainer)
        {
            gridLineRenderer.transform.position = gridLinePoint;
            Vector3 position = new Vector3(0, 0, 0);
            if (gridLineRenderer.name.Equals("Grid Line Horizontal")){

                float difference =  xAxis.transform.position.x - gridLinePoint.x;
                position = new Vector3(difference,0,0);
                gridLineRenderer.SetPosition(1, position);

                //Label
                TextMesh label = gridLineRenderer.GetComponentInChildren<TextMesh>();
                Vector3 offset = new Vector3(-5, 0, 0);
                label.transform.position = gridLineRenderer.transform.TransformPoint(gridLineRenderer.GetPosition(1)) + offset;

            }
            else if(gridLineRenderer.name.Equals("Grid Line Vertical"))
            {
                float difference = yAxis.transform.position.y - gridLinePoint.y;
                position = new Vector3(0, difference, 0);
                gridLineRenderer.SetPosition(1, position);
                TextMesh label = gridLineRenderer.GetComponentInChildren<TextMesh>();
                Vector3 offset = new Vector3(0, -10, 0);
                label.transform.position = gridLineRenderer.transform.TransformPoint(gridLineRenderer.GetPosition(1)) + offset;
            }
        }
        
    }


    public Vector3 GetgridLinePoint(LineRenderer line1, LineRenderer line2)
    {
        Vector3 line1start = line1.transform.TransformPoint(line1.GetPosition(0));
        Vector3 line1end = line1.transform.TransformPoint(line1.GetPosition(1));


        Vector3 p3 = line2.transform.TransformPoint(line2.GetPosition(0));
        Vector3 p4 = line2.transform.TransformPoint(line2.GetPosition(1));

        // Calculate the direction vectors of the two lines
        Vector3 dir1 = line1end - line1start;
        Vector3 dir2 = p4 - p3;

        // Calculate the denominator of the equation for t1 and t2
        float den = (dir1.x * dir2.y) - (dir1.y * dir2.x);

        // If the denominator is zero, the lines are parallel
        if (Mathf.Approximately(den, 0))
        {
            return Vector3.zero;
        }

        // Calculate the values of t1 and t2
        float t1 = ((p3.x - line1start.x) * dir2.y - (p3.y - line1start.y) * dir2.x) / den;
        float t2 = ((p3.x - line1start.x) * dir1.y - (p3.y - line1start.y) * dir1.x) / den;

        // If either t value is outside of the range [0, 1], there is no gridLine
        if (t1 < 0 || t1 > 1 || t2 < 0 || t2 > 1)
        {
            return Vector3.zero;
        }

        // Calculate the gridLine point and return it
        return line1start + dir1 * t1;
    }
}

