using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //Sauvegarder les differentes lignes composant la queue
    public Tail queuePrefab;
    private List<Tail> listQueue;
    private Tail currentTail;
    //determine si on est sur un trou ou si on dessine la queue
    private bool isDrawingTail;
    public Transform positionHotSpotEnd;

    //Generer des trous dans la queue du serpent
    public float minTimeTail = 1f;
    public float minTimeBreakInTail = 1f;
    public float maxTimeTail = 1.5f;
    public float maxTimeBreakInTail = 1.5f;

    //Pour les collisions
    public Transform positionHotSpotFront;
    public GameObject dieAnimation;

    void Start()
    {
        isDrawingTail = true;
        listQueue = new List<Tail>();
        createTail();
        StartCoroutine(DrawCurrentTail());
    }


    /*
     * Créer la queue du serpent
     */
    //détermine si on dessine la queue ou si on fait un trou
    private IEnumerator DrawCurrentTail()
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

    void Update()
    {
        //dessine la queue
        if (isDrawingTail)
        {
            currentTail.updateTailVertex(positionHotSpotEnd.position);
        }
    }

    //Crée le bout de queue suivant
    private void createTail()
    {
        currentTail = Instantiate(queuePrefab);
        listQueue.Add(currentTail);
    }


    /**
     * Collisions
     */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Tail"))
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(dieAnimation, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
        Destroy(gameObject);
    }
}
