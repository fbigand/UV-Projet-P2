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
        CreateTail();
    }

    void Update()
    {
        //dessine la queue
        lastDistance += Vector3.Distance(lastPoint, new Vector2(positionHotSpotEnd.position.x, positionHotSpotEnd.position.y));
        if (isDrawingTail)
        {
            currentTail.updateTailVertex(positionHotSpotEnd.position);
            if(lastDistance > maxDistanceTail)
            {
                isDrawingTail = false;
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                anim.SetBool("Free", true);
                ResetLastDistance();
            }
        }
        else
        {
            if (lastDistance > distanceBreakInTail)
            {
                gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
                anim.SetBool("Free", false);
                isDrawingTail = true;
                ResetLastDistance();
                CreateTail();
            }
        }
        lastPoint = positionHotSpotEnd.position;
    }

    public void ResetLastDistance()
    {
        lastDistance = 0f;
    }

    //Crée le bout de queue suivant
    public void CreateTail()
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
        if (collision.CompareTag("Wall")||collision.CompareTag("Player"))
        {
            Die();
        }
        else if (collision.CompareTag("Tail"))
        {
            CollideTail(collision.GetComponent<Tail>());
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(dieAnimation, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
        Destroy(gameObject);
    }

    private void CollideTail(Tail collidedTail)
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

        //s'il n'y avait pas de points avant l'explosion on detruit la queue prevue pour ça
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
