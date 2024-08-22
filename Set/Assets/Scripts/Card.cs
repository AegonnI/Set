using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject frame;
    public int type; // 0 - cube, 1 - circle, 2 - romb
    public int color; // 0 - red, 1 - green, 2 - blue
    public int count; // 0 - 1, 1 - 2, 2 - 3
    public int filling; // 0 - empty, 1 - hatch, 2 - fill
    public float height = 0.32f;
    public float width = 0.20f;
    private SpriteRenderer spr, spr1;
    private Transform trans;
    //public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    public Card()
    {
        type = -1;
        color = -1;
        count = -1;
        filling = -1;
    }

    // Start is called before the first frame update
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();

        Card card = GetComponent<Card>();
        
        spr.sprite = Resources.Load<Sprite>(card.type.ToString() + card.color.ToString() + card.count.ToString() + card.filling.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InsideCard())
        {
            Frame frm = frame.GetComponent<Frame>();
            Instantiate(frame, trans.position, spawnRotation);
        }
    }

    bool InsideCard()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return diference.x >= trans.position.x - width && diference.x <= trans.position.x + width &&
            diference.y >= trans.position.y - height && diference.y <= trans.position.y + height;
    }
}
