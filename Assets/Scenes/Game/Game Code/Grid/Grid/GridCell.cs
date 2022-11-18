using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void DisplayCell()
    {
        meshRenderer.enabled = true;
    }

    public void HideCell()
    {
        meshRenderer.enabled = false;
    }
}
