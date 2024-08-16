using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int type; // 0 - cube, 1 - circle, 2 - romb
    public int color; // 0 - red, 1 - green, 2 - blue
    public int count; // 0 - 1, 1 - 2, 2 - 3
    public int filling; // 0 - empty, 1 - hatch, 2 - fill

    public Card()
    {
        type = -1;
        color = -1;
        count = -1;
        filling = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
