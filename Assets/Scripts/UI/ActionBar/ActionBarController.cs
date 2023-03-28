using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarController : MonoBehaviour
{
    public Canvas ActionBarCanvas;

    public Canvas BuildCanvas;
    public Canvas PlaceCanvas;
    public Canvas LearnCanvas;
    public Canvas PolicyCanvas;
    public Canvas MarketsCanvas;

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
    }
}
