using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas ActionBarCanvas;

    public Canvas BuildCanvas;
    public Canvas PlaceCanvas;
    public Canvas LearnCanvas;
    public Canvas PolicyCanvas;
    public Canvas MarketsCanvas;
    public Canvas ShiftCurvesCanvas;
    public Canvas ElasticityCanvas;
    public Canvas FullyInteractiveCanvas;
    public GameObject RandomEventsContainer;

    public void Start()
    {
        ActionBarCanvas.enabled = true;
        CloseAllBut(null);
    }

    public void OnBuildActionBarClick()
    {
        CloseAllBut(BuildCanvas);

    }

    public void OnLearnActionBarClick()
    {
        CloseAllBut(LearnCanvas);

    }

    public void OnPolicyActionBarClick()
    {
        CloseAllBut(PolicyCanvas);

    }

    public void OnMarketsActionBarClick()
    {
        CloseAllBut(MarketsCanvas);
    }

    public void OnElasticityLearnClick()
    {
        CloseAllBut(ElasticityCanvas);
    }

    public void OnShiftsLearnClick()
    {
        CloseAllBut(ShiftCurvesCanvas);
    }

    public void OnFullyInteractiveClick()
    {
        CloseAllBut(FullyInteractiveCanvas);
    }

    public void CloseAll()
    {
        CloseAllBut(null);
    }

    private void CloseAllBut(Canvas canvasToOpen)
    {
        BuildCanvas.enabled = false;
        PlaceCanvas.enabled = false;
        LearnCanvas.enabled = false;
        MarketsCanvas.enabled = false;
        PolicyCanvas.enabled = false;
        ShiftCurvesCanvas.enabled = false;
        ElasticityCanvas.enabled = false;
        FullyInteractiveCanvas.enabled = false;
        

        foreach (Transform canvas in RandomEventsContainer.transform)
        {
            canvas.gameObject.SetActive(false);
        }

        if (canvasToOpen == null)
        {

        }
        else if (canvasToOpen.Equals(BuildCanvas))
        {
            BuildCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(PlaceCanvas))
        {
            PlaceCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(LearnCanvas))
        {
            LearnCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(MarketsCanvas))
        {
            MarketsCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(PolicyCanvas))
        {
            PolicyCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(ShiftCurvesCanvas))
        {
            ShiftCurvesCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(ElasticityCanvas))
        {
            ElasticityCanvas.enabled = true;
        }
        else if (canvasToOpen.Equals(FullyInteractiveCanvas))
        {
            FullyInteractiveCanvas.enabled = true;
        }
    }

    public bool isLearnOpen()
    {
        return LearnCanvas.enabled;
    }
}
