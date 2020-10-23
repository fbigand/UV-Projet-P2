using System.Collections.Generic;
using UnityEngine;

public class ManagePickUps : MonoBehaviour
{
    public List<PickUps> usablePickUps;

    public bool activationPickUps = true;
    public bool stopSpawning = false;

    public float spawnDelay = 6f;
    public float spawnTime = 4f;
    private float maplength = 8.7f;

    void Start()
    {
        if (activationPickUps)
        {
            InvokeRepeating("SpawnPickUps", spawnTime, spawnDelay);
        }
    }

    public void SpawnPickUps()
    {
        int randomItem = Random.Range(0, usablePickUps.Count);
        PickUps toSpawn = usablePickUps[randomItem];

        Vector3 pickUpsPosition = new Vector3(Random.Range(-0.4f, -0.4f + maplength), Random.Range(-4.35f, -4.35f + maplength), -1f);
        Instantiate(toSpawn, pickUpsPosition, Quaternion.identity);

        if (stopSpawning)
        {
            CancelInvoke("SpawnPickUps");
        }
    }
}
