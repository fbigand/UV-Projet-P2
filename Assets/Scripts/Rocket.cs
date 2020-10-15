using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Rocket"))
        {
            Die();
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShipMovement>().Die();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
