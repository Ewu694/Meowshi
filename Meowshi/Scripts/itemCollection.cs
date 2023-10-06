using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollection : MonoBehaviour
{
    //Used to link to game manager script.
    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to actually destroy the item and give it the counter found in the game manager script.
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("item"))
        {

            GM.itemCollection();
            Destroy(other.gameObject);
        }
    }
}
