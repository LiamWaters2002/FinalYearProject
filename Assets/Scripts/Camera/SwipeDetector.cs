using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{

    public float minSwipeDist = 50.0f;

    public float maxTapTime = 0.5f;

    private Vector2[] startPos;

    private float[] startTime;

    private bool[] touchMoved;

    void Start()
    {
        // Initialize the arrays with the maximum number of touches
        int maxTouches = Input.touchCount > 0 ? Input.touchCount : 1;
        startPos = new Vector2[maxTouches];
        startTime = new float[maxTouches];
        touchMoved = new bool[maxTouches];
    }

    void Update()
    {
    }
}