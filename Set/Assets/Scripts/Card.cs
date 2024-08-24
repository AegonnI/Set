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
    public bool framed; 

    private SpriteRenderer spr;
    public Quaternion spawnRotation = Quaternion.identity;

    public Card()
    {
        type = -1;
        color = -1;
        count = -1;
        filling = -1;
        framed = false;
    }

    // Start is called before the first frame update
    void Awake()
    {       
        Card card = GetComponent<Card>();

        spr = GetComponent<SpriteRenderer>();

        spr.sprite = Resources.Load<Sprite>(card.type.ToString() + card.color.ToString() + card.count.ToString() + card.filling.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        //Frame frm = frame.GetComponent<Frame>();
        if (!framed)
        {
            //Logic.set[Logic.count_i] = new Vector4(type, color, count, filling);
            Logic.set[Logic.count_i, 0] = type;
            Logic.set[Logic.count_i, 1] = color;
            Logic.set[Logic.count_i, 2] = count;
            Logic.set[Logic.count_i, 3] = filling;
            //Debug.Log(Logic.set[Logic.count_i].x + ", " + Logic.set[Logic.count_i].y + ", " + Logic.set[Logic.count_i].z + ", " + Logic.set[Logic.count_i].w);
            Logic.count_i++;
            
            Instantiate(frame, transform.position, spawnRotation);
            framed = true;
        }
    }
}
