using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
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
        line.positionCount = 0;
        line.startWidth = widthLine;
        line.endWidth = widthLine;
    }

    public void updateTailVertex(Vector3 position)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);
    }
}
