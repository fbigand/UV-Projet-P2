using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    //Dessine la ligne
    [HideInInspector]
    public LineRenderer line;

    [HideInInspector]
    public Color color;
    //contient les points pour le edgeCollider
    private List<Vector2> listPointsEdgeCollider;

    //largeur de la ligne
    public float widthLine = 1f;
    //le composant collider pour les collisions de la ligne
    private EdgeCollider2D edgeCollider;


    // Start is called before the first frame update
    void Awake()
    {
        listPointsEdgeCollider = new List<Vector2>();
        line = GetComponent<LineRenderer>();
        
        line.positionCount = 0;
        line.startWidth = widthLine;
        line.endWidth = widthLine;
        edgeCollider = GetComponent<EdgeCollider2D>();
        
    }

    //a chaque fois qu'on a un nouveau point à ajouter à la ligne
    public void updateTailVertex(Vector3 position)
    {
        //On ajoute le point pour le dessin de la ligne
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);

        //on ajoute le point à notre liste de point pour le collider de la ligne
        listPointsEdgeCollider.Add(new Vector2(position.x,position.y));

        //et si on a plus d'un seul point
        if (line.positionCount > 1)
        {
            //on met a jour notre collider avec les points constituant la ligne
            edgeCollider.points = listPointsEdgeCollider.ToArray();
        }
    }

    public void SetColor(Color color)
    {
        this.color = color;
        line.material.color = color;
    }
}
