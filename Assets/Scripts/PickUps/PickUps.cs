using UnityEngine;

public abstract class PickUps : MonoBehaviour
{
    public float speedModif = 0.005f;

    public GameObject[] activePlayers;

    private void Awake()
    {
        activePlayers = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagePlayers>().activePlayers;
    }

    public abstract void ActivatePickUp(GameObject ship);
}
