using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Frame : MonoBehaviour
{
    public sbyte state; // -1 - empty, 0 - goldenFrame, 1 - greenFrame, 2 - redFrame

    public static float timeForDestroy = 0.3f;

    private SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();

        spr.sprite = Resources.Load<Sprite>("GoldenFrame");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Logic.win_or_loose == 1)
        {
            Debug.Log("Win");
            spr.sprite = Resources.Load<Sprite>("GreenFrame");
        }
        else if (Logic.win_or_loose == 2)
        {
            Debug.Log("Loose");
            spr.sprite = Resources.Load<Sprite>("RedFrame");
        }
    }

    private void LateUpdate()
    {
        if (Logic.win_or_loose == 1 || Logic.win_or_loose == 2)
        {
            Destroy(gameObject, timeForDestroy);
        }
    }

    private void FixedUpdate()
    {
        if (Logic.win_or_loose == 1 || Logic.win_or_loose == 2)
        {
            //Logic.win_or_loose = 0;
        }

    }
}
