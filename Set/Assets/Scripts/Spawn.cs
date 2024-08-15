using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject card;

    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(card, spawnPosition, spawnRotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
