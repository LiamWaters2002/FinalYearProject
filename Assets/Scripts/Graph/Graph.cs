using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    public float supplyGradient = 1f; //Check unity inspector
    public float demandGradient = -1f; //Check unity inspector

    private float curveThickness = 5;

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
    private Transform supplyLabel;
    private Transform demandLabel;
    public LineRenderer[] horizontalGridLine;
    public LineRenderer[] verticalGridLine;

    private Vector3[] horizontalGridLinePositions;
    private Vector3[] verticalGridLinePositions;

    GameObject shiftedDemandContainer;
    GameObject shiftedSupplyContainer;

    public bool demandShifted = false;
    public bool supplyShifted = false;

    public string shiftType = "";

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


        supplyLabel = supplyLineContainer.transform.Find("Supply Label");
        demandLabel = demandLineContainer.transform.Find("Demand Label");

        //Only need to get data for one either supply/demand's GridLines as they start off in same position.
        verticalGridLinePositions = new Vector3[verticalGridLine[0].positionCount];
        verticalGridLine[0].GetPositions(verticalGridLinePositions);

        horizontalGridLinePositions = new Vector3[horizontalGridLine[0].positionCount];
        horizontalGridLine[0].GetPositions(horizontalGridLinePositions);

        verticalGridLine[0].startWidth = 3;
        verticalGridLine[0].endWidth = 3;

        horizontalGridLine[0].startWidth = 3;
        horizontalGridLine[0].endWidth = 3;

        verticalGridLine[1].startWidth = 3;
        verticalGridLine[1].endWidth = 3;

        horizontalGridLine[1].startWidth = 3;
        horizontalGridLine[1].endWidth = 3;
    }

    public void Update()
    {
        UpdateCurves(supplyGradient, demandGradient);
        UpdateLabelPosition(demandLine.GetComponent<LineRenderer>(), demandLabel.GetComponent<TextMesh>(), 0);
        UpdateLabelPosition(supplyLine.GetComponent<LineRenderer>(), supplyLabel.GetComponent<TextMesh>(), 0);
        
        if (shiftType != "")
        {
            CheckShiftType();
        }
    }

    public void CheckShiftType()
    {
        if (shiftType.Equals("RightwardShiftInSupply"))
        {
            shiftType = "";
            ShiftSupply(shiftedSupplyContainer, 10); //negative - left, positive - right
            
        }
        else if (shiftType.Equals("RightwardShiftInDemand"))
        {
            shiftType = "";
            ShiftDemand(shiftedDemandContainer, 10); //negative - left, positive - right
        }
        else if (shiftType.Equals("LeftwardShiftInSupply"))
        {
            shiftType = "";
            ShiftSupply(shiftedSupplyContainer, -10); //negative - left, positive - right
        }
        else if (shiftType.Equals("LeftwardShiftInDemand"))
        {
            shiftType = "";
            ShiftDemand(shiftedDemandContainer, -10); //negative - left, positive - right
        }
    }


    public void ResetGraph()
    {
        Destroy(shiftedDemandContainer);
        Destroy(shiftedSupplyContainer);
        supplyGradient = 1;
        demandGradient = -1;
        supplyShifted = false;
        demandShifted = false;
    }

    public void IncreaseSupplyGradient()
    {
        float rotation = supplyLine.transform.eulerAngles.z;
        rotation = rotation + 5;
        Debug.Log(rotation);
        if (!(rotation > 89))
        {
            supplyGradient = Mathf.Tan(rotation * Mathf.Deg2Rad);
            UpdateLabelPosition(supplyLine.GetComponent<LineRenderer>(), supplyLabel.GetComponent<TextMesh>(), 0);
        }
        else if (rotation > 89)
        {
            supplyGradient = Mathf.Tan(89 * Mathf.Deg2Rad); //90 causes label to be in wrong position...
        }
        ShiftSupply(shiftedSupplyContainer ,1);
        ShiftDemand(shiftedDemandContainer, 1);
    }

    public void DecreaseSupplyGradient()
    {
        float rotation = supplyLine.transform.eulerAngles.z;
        rotation = rotation - 5;
        Debug.Log(rotation);
        //it is not exactly 0
        if (!(rotation < 1))
        {
            supplyGradient = Mathf.Tan(rotation * Mathf.Deg2Rad);
            UpdateLabelPosition(supplyLine.GetComponent<LineRenderer>(), supplyLabel.GetComponent<TextMesh>(), 0);
        }
        else if (rotation < 1)
        {
            supplyGradient = 0;
        }
        ShiftSupply(shiftedSupplyContainer, 1);
        ShiftDemand(shiftedDemandContainer, 1);
    }

    public void IncreaseDemandGradient()
    {
        float rotation = demandLine.transform.eulerAngles.z;
        rotation = rotation - 5;
        Debug.Log(rotation);
        if (!(rotation < 271 && rotation > 6))
        {
            demandGradient = Mathf.Tan(rotation * Mathf.Deg2Rad);
            UpdateLabelPosition(demandLine.GetComponent<LineRenderer>(), demandLabel.GetComponent<TextMesh>(), 0);
        }
        else if (rotation <= 270)
        {
            demandGradient = Mathf.Tan(-89 * Mathf.Deg2Rad);
        }

        MoveGridLine(demandLineContainer.transform.Find("Grid Line Container"), shiftedSupplyContainer.transform.Find("Supply Line"));
        MoveGridLine(shiftedDemandContainer.transform.Find("Grid Line Container"), shiftedSupplyContainer.transform.Find("Supply Line"));
    }

    public void DecreaseDemandGradient()
    {
        float rotation = demandLine.transform.eulerAngles.z;
        rotation = rotation + 5;
        Debug.Log(rotation);
        if (rotation > 359 || demandGradient == 0)
        {
            demandGradient = 0;
        }
        else
        {
            demandGradient = Mathf.Tan(rotation * Mathf.Deg2Rad);
            UpdateLabelPosition(demandLine.GetComponent<LineRenderer>(), demandLabel.GetComponent<TextMesh>(), 0);
        }
        MoveGridLine(demandLineContainer.transform.Find("Grid Line Container"), shiftedSupplyContainer.transform.Find("Supply Line"));
        MoveGridLine(shiftedDemandContainer.transform.Find("Grid Line Container"), shiftedSupplyContainer.transform.Find("Supply Line"));
    }

    public void RightwardShiftInSupply()
    {
        shiftType = "RightwardShiftInSupply";
        if (!supplyShifted)
        {
            shiftedSupplyContainer = Instantiate(supplyLineContainer, this.transform); //Must be at the this.transform position, or it will go to reference...
            shiftedSupplyContainer.transform.parent = this.transform;  //Make curve parent graph object
            supplyShifted = true;
        }
        supplyShifted = true;
    }

    public void LeftwardShiftInSupply()
    {
        shiftType = "LeftwardShiftInSupply";
        if (!supplyShifted)
        {
            shiftedSupplyContainer = Instantiate(supplyLineContainer, this.transform);
            shiftedSupplyContainer.transform.parent = this.transform;  //Make curve parent graph object
            supplyShifted = true;
        }
        supplyShifted = true;
    }

    public void RightwardShiftInDemand()
    {
        shiftType = "RightwardShiftInDemand";

        if (!demandShifted)
        {
            shiftedDemandContainer = Instantiate(demandLineContainer, this.transform);
            shiftedDemandContainer.transform.parent = this.transform;  //Make curve parent graph object
            demandShifted = true;
        }
        demandShifted = true;
    }

    public void LeftwardShiftInDemand()
    {
        shiftType = "LeftwardShiftInDemand";

        if (!demandShifted)
        {
            shiftedDemandContainer = Instantiate(demandLineContainer, this.transform);
            shiftedDemandContainer.transform.parent = this.transform; //Make curve parent graph object
            demandShifted = true;
        }
        demandShifted = true;
    }


    public void UpdateCurves(float supplyGradient, float demandGradient)
    {

        float sRadians = Mathf.Atan(supplyGradient);
        float sAngle = sRadians * Mathf.Rad2Deg;
        Quaternion stargetRotation = Quaternion.Euler(0, 0, sAngle);

        supplyLine.transform.rotation = stargetRotation;
        if (shiftedSupplyContainer)
        {
            Transform supplyLine = shiftedSupplyContainer.transform.Find("Supply Line");
            supplyLine.transform.rotation = stargetRotation;

            TextMesh label = shiftedSupplyContainer.transform.Find("Supply Label").GetComponent<TextMesh>();

            UpdateLabelPosition(supplyLine.GetComponent<LineRenderer>(), label, 0);
        }
        

        float dRadians = Mathf.Atan(demandGradient);
        float dAngle = dRadians * Mathf.Rad2Deg;
        Quaternion dtargetRotation = Quaternion.Euler(0, 0, dAngle);

        demandLine.transform.rotation = dtargetRotation;
        if (shiftedDemandContainer)
        {
            Transform demandLine = shiftedDemandContainer.transform.Find("Demand Line");
            demandLine.transform.rotation = dtargetRotation;

            TextMesh label = shiftedDemandContainer.transform.Find("Demand Label").GetComponent<TextMesh>();

            UpdateLabelPosition(demandLine.GetComponent<LineRenderer>(), label, 0);
        }

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

        Coroutine coroutine = StartCoroutine(ShiftCurve(demandCurve, amount, gridLineContainer));

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

        Coroutine coroutine = StartCoroutine(ShiftCurve(supplyCurve, amount, gridLineContainer));

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
            gridLineRenderer.startWidth = 3f;
            gridLineRenderer.endWidth = 3f;
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

        Vector3 line2start = line2.transform.TransformPoint(line2.GetPosition(0));
        Vector3 line2end = line2.transform.TransformPoint(line2.GetPosition(1));

        float m1;
        float m2;

        if (line1.name.Equals("Supply Line"))
        {
            m1 = supplyGradient;
            m2 = demandGradient;
        }
        else
        {
            m1 = demandGradient;
            m2 = supplyGradient;
        }

        float c1 = line1start.y - m1 * line1start.x;


        float c2 = line2start.y - m2 * line2start.x;

        // y1 = mx1 + c1 
        // y2 = mx2 + c2
        // 
        // mx1 + c1 = mx2 + c2
        // mx1 - mx2 = c1 - c2
        // x(m1 - m2) = c1 - c2
        // x = c1 - c2 / m1 - m2
        float x = (c2 - c1) / (m1 - m2);

        float y = m1 * x + c1;

        Vector3 result = new Vector3(x, y, 0);

        // If the intersection point is outside of the range of the line segments, there is no gridLine
        if (x < Mathf.Min(line1start.x, line1end.x)
           || x > Mathf.Max(line1start.x, line1end.x) 
           || x < Mathf.Min(line2start.x, line2end.x)
           || x > Mathf.Max(line2start.x, line2end.x)
           || y < Mathf.Min(line1start.y, line1end.y)
           || y > Mathf.Max(line1start.y, line1end.y)
           || y < Mathf.Min(line2start.y, line2end.y)
           || y > Mathf.Max(line2start.y, line2end.y))
        {
            result = new Vector3(-999, -999, -999);
        }

        // Return the intersection point
        return result;
    }
}

