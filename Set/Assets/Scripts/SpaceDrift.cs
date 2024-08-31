using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDrift : MonoBehaviour
{
    public float moveSpeed = 0.001f;
    public float rotateSpeed = 0.011f;

    //private float moveSpeed = SpawnBgCards.moveSpeed;
    //private float rotateSpeed = SpawnBgCards.rotateSpeed;

    private float xx;
    private float yy;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.sprite = Resources.Load<Sprite>(rand.Next(2).ToString() + rand.Next(2).ToString() + rand.Next(2).ToString() + rand.Next(2).ToString());

        GenVec();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xx, yy, 5), moveSpeed);
        if (transform.position.x == xx && transform.position.y == yy)
        {
            GenVec();
        }
        //transform.rotation = Quaternion.EulerRotation(xx, yy, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.EulerAngles(xx, yy, 50), rotateSpeed);
    }

    void GenVec()
    {
        System.Random rand = new System.Random();
        xx = rand.Next(-10, 10);
        yy = rand.Next(-5, 5);
    }
}
