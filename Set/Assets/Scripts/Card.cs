using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject frame;

    public int type;    // 0 - Cube  | 1 - Circle | 2 - Rhombus
    public int color;   // 0 - Red   | 1 - Green  | 2 - Blue
    public int count;   // 0 - 1     | 1 - 2      | 2 - 3
    public int filling; // 0 - Empty | 1 - Hatch  | 2 - Fill
    public bool framed;
    public int index;
    public Card card;

    private SpriteRenderer spr;

    // Start is called before the first frame update
    void Awake()
    {       
        card = GetComponent<Card>();

        spr = GetComponent<SpriteRenderer>();

        spr.sprite = Resources.Load<Sprite>(card.type.ToString() + card.color.ToString() + card.count.ToString() + card.filling.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Logic.win_or_loose > 0 && framed)
        {
            framed = false;
            if (Logic.win_or_loose == 1)
            {
                Destroy(gameObject, Frame.timeForDestroy);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!framed)
        {
            Logic.set[Logic.count_i, 0] = type;
            Logic.set[Logic.count_i, 1] = color;
            Logic.set[Logic.count_i, 2] = count;
            Logic.set[Logic.count_i, 3] = filling;
            Logic.count_i++;
            
            Instantiate(frame, transform.position, Quaternion.identity);
            framed = true;
            Spawn.cards[index].framed = true;
        }
    }
}
