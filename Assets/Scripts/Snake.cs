using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //Sauvegarder les differentes lignes composant la queue
    public Tail queuePrefab;
    private List<Tail> listQueue;
    private Tail currentTail;
    public Color colorTail;
    //determine si on est sur un trou ou si on dessine la queue
    public bool isDrawingTail;
    public Transform positionHotSpotEnd;

    //Generer des trous dans la queue du serpent
    public float minDistanceTail = 4.5f;
    public float maxDistanceTail = 5f;
    public float distanceBreakInTail = 0.25f;
    private Vector2 lastPoint;
    private float lastDistance;

    //Pour les collisions
    public Transform positionHotSpotFront;
    public GameObject dieAnimation;
    public float radiusExplosionDeath = 1f;

    //Autres
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isDrawingTail = true;
        listQueue = new List<Tail>();
        lastDistance = 0f;
        lastPoint = positionHotSpotEnd.position;
        createTail();
        //StartCoroutine(DrawCurrentTail());
    }


    /*
     * Créer la queue du serpent
     */
    //détermine si on dessine la queue ou si on fait un trou
    /*private IEnumerator DrawCurrentTail()
    {
        while (true)
        {
            createTail();
            float distanceTail = Random.Range(minDistanceTail, maxDistanceTail);
            yield return new WaitForSeconds(distanceTail);
            isDrawingTail = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetBool("Free", true);
            yield return new WaitForSeconds(distanceBreakInTail);
            isDrawingTail = true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            anim.SetBool("Free", false);
        }
    }*/

    void Update()
    {
        //dessine la queue
        if (isDrawingTail)
        {
            currentTail.updateTailVertex(positionHotSpotEnd.position);
            lastDistance+= Vector3.Distance(lastPoint,positionHotSpotEnd.position);
            if(lastDistance > maxDistanceTail)
            {
                isDrawingTail = false;
                lastDistance = 0f;
            }
        }
        else
        {
            lastDistance += Vector3.Distance(lastPoint, positionHotSpotEnd.position);
            if (lastDistance > distanceBreakInTail)
            {
                lastDistance = 0f;
                isDrawingTail = true;
                createTail();
            }
        }
        lastPoint = positionHotSpotEnd.position;
    }

    //Crée le bout de queue suivant
    public void createTail()
    {
        currentTail = Instantiate(queuePrefab) as Tail;
        currentTail.SetColor(colorTail);
        listQueue.Add(currentTail);
    }



    /**
     * Collisions
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Die();
        }
        else if (collision.CompareTag("Tail"))
        {
            collideTail(collision.GetComponent<Tail>());
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(dieAnimation, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
        Destroy(gameObject);
    }

    private void collideTail(Tail collidedTail)
    {
        //en premier on enregistre tous les points de la queue percutée
        Vector2[] listPointsCollidedTail = new Vector2[collidedTail.listPointsEdgeCollider.Count];
        collidedTail.listPointsEdgeCollider.CopyTo(listPointsCollidedTail);

        //on crée une nouvelle queue pour dessiner le bout avant le trou
        Tail partBeforeHole = Instantiate(queuePrefab) as Tail;
        partBeforeHole.SetColor(collidedTail.color);
        int i = 0;
        while(i < listPointsCollidedTail.Length)
        {
            Vector2 currentPoint = listPointsCollidedTail[i];
            // on parcours les points et on les ajoute à la ligne tant qu'on arrive pas au trou
            if (Vector2.Distance(currentPoint, positionHotSpotFront.position) > radiusExplosionDeath)
            {
                partBeforeHole.updateTailVertex(new Vector3(currentPoint.x, currentPoint.y, -0.1f));
                i++;
            }
            else
            {
                break;
            }
        }

        //si il n'y avait pas de points avant l'explosion on detruit la queue prevue pour ça
        if(i == 0)
        {
            partBeforeHole.gameObject.SetActive(false);
        }

        collidedTail.Reset();
        bool isTherePointAfterHole = false;
        while (i < listPointsCollidedTail.Length)
        {
            //on ajoute a la ligne percutee les points a partir de l'explosion
            Vector2 currentPoint = listPointsCollidedTail[i];
            if (Vector2.Distance(currentPoint, positionHotSpotFront.position) > radiusExplosionDeath)
            {
                collidedTail.updateTailVertex(new Vector3(currentPoint.x, currentPoint.y, -0.1f));
                isTherePointAfterHole = true;
            }
            i++;
        }

        if (!isTherePointAfterHole)
        {
            collidedTail.gameObject.SetActive(false);
        }
    }
}
