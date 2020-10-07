using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Tail queuePrefab;
    private List<Tail> listQueue;
    private Tail currentTail;
    private bool isDrawingTail;

    public float minTimeTail = 1f;
    public float minTimeBreakInTail = 1f;
    public float maxTimeTail = 1.5f;
    public float maxTimeBreakInTail = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        isDrawingTail = true;
        listQueue = new List<Tail>();
        createTail();
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeTail,maxTimeTail));
            isDrawingTail = !isDrawingTail;
            yield return new WaitForSeconds(Random.Range(minTimeBreakInTail, maxTimeBreakInTail));
            createTail();
            isDrawingTail = !isDrawingTail;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isDrawingTail)
        {
            currentTail.updateTailVertex(transform.position);
        }
    }

    private void createTail()
    {
        currentTail = Instantiate(queuePrefab);
        listQueue.Add(currentTail);
    }
}
