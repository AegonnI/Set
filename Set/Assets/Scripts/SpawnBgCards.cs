using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBgCards : MonoBehaviour
{
    public GameObject spaceCard;

    public int cardCount;

    //public static float moveSpeed = 0.001f;
    //public static float rotateSpeed = 0.011f;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < cardCount; i++) 
        {
            Instantiate(spaceCard, new Vector3(rand.Next(-10, 10), rand.Next(-5, 5), 5), Quaternion.identity);
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
