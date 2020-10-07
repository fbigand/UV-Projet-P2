using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private LineRenderer line;
    public Color color;
    public float widthLine = 1f;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startColor = color;
        line.endColor = color;
        line.positionCount = 1;
        line.SetPosition(0, transform.position);
        line.startWidth = widthLine;
        line.endWidth = widthLine;
    }

    // Update is called once per frame
    void Update()
    {
        line.positionCount++;
        line.SetPosition(line.positionCount-1, transform.position);
    }
}
