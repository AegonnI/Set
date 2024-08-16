using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject card;

    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    private bool[,,,] cardBoard = new bool[3,3,3,3];

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < 12; i++)
        {
            int xrand = rand.Next(3);
            int yrand = rand.Next(3);
            int zrand = rand.Next(3);
            int wrand = rand.Next(3);
            if (cardBoard[xrand, yrand, zrand, wrand] == true)
            {

            }
            cardBoard[xrand, yrand, zrand, wrand] = true;
            spawnPosition = new Vector3(i % 3, i / 3, 0);
            Card crd = card.GetComponent<Card>();
            crd.type = xrand;
            crd.color = yrand;
            crd.count = zrand;
            crd.filling = wrand;
            Instantiate(card, spawnPosition, spawnRotation);
        }
        //Instantiate(card, spawnPosition, spawnRotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
