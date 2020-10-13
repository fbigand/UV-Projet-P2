using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

public class ManagePlayers : MonoBehaviour
{
    public GameObject[] usableSpaceships;

    public int numberPlayers = 1;

    private GameObject[] activePlayers;
    // Start is called before the first frame update
    void Start()
    {
        activePlayers = new GameObject[numberPlayers];
        if(usableSpaceships != null && usableSpaceships.Length > 0)
        {
            for(int i = 0; i< usableSpaceships.Length; i++)
            {
                if (i < numberPlayers)
                {
                    activePlayers[i] = usableSpaceships[i];
                }
                else
                {
                    Destroy(usableSpaceships[i].gameObject);
                }
            }

            placePlayers();
        }
    }

    private void placePlayers()
    {
        float rayon = 4;
        Vector2 center= new Vector2(4, 0);
        float incrArc = 2 * Mathf.PI * rayon / numberPlayers;
        float longueurArc = 0;

        float angle;
        float posX;
        float posY;

        for (int i = 0; i < activePlayers.Length; i++)
        {
            
            angle = longueurArc  /  rayon;
            posX = rayon * Mathf.Cos(angle) +center.x;
            posY = rayon * Mathf.Sin(angle) + center.y;

          
            activePlayers[i].transform.position = new Vector3(posX, posY, -0.1f);
            orientateSpaceship(activePlayers[i].transform, center);

            longueurArc += incrArc;
        }
    }

    private void orientateSpaceship(Transform spaceship, Vector2 lookPoint)
    {
        Vector3 diff = lookPoint - new Vector2(spaceship.position.x,spaceship.position.y);
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        spaceship.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
