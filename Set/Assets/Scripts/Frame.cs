using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public sbyte state; // -1 - empty, 0 - goldenFrame, 1 - greenFrame, 2 - redFrame

    public static float timeForDestroy = 0.3f;

    void Awake()
    {
        state = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GoldenFrame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Logic.win_or_loose == 1)
        {
            state = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GreenFrame");
        }
        else if (Logic.win_or_loose == 2)
        {
            state = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("RedFrame");
        }
    }

    private void LateUpdate()
    {
        if (Logic.win_or_loose == 1 || Logic.win_or_loose == 2)
        {
            Destroy(gameObject, timeForDestroy);
        }
    }
}
