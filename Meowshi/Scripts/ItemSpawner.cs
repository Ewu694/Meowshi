using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    //maxTime basically correlates with timer controlling spawn intervals.
    public float maxTime = 1f;
    private float timer = 0;

    //GameObject array to list all the items being used, and public transform to spawn within the Pipe range by linking them.
    public GameObject[] itemRandomSpawn;
    public Transform pipePos;

    private void OnEnable()
    {
        InvokeRepeating(nameof(ItemSpawn), maxTime, maxTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ItemSpawn));
    }

    private void ItemSpawn()
    {
        if (timer > maxTime)
        {
            //This chooses a random item to spawn on the interval.
            int n = Random.Range(0, itemRandomSpawn.Length);
            GameObject item = Instantiate(itemRandomSpawn[n]);

            //Spans the item in correlation to the pipe position to avoid clipping.
            item.transform.position = pipePos.position + new Vector3(2, 1, 0);

            Destroy(item, 15);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

    }
}
