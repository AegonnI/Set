using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBgCards : MonoBehaviour
{
    public GameObject spaceCard;

    public int cardCount;

    void Start()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < cardCount; i++) 
        {
            Instantiate(spaceCard, new Vector3(rand.Next(-10, 10), rand.Next(-5, 5), 5), Quaternion.identity);
        }
   
    }
}
