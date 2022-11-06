using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest: MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;


    private Vector3 targetPosition;
    private GridPosition gridPosition;
    private WorldGrid worldGrid;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        gridPosition = worldGrid.GetInstance().GetGridPosition(transform.position);
        worldGrid.GetInstance().AddObjectAtGridPosition(gridPosition, this);
    }

}
