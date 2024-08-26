using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Frame : MonoBehaviour
{
    public sbyte state; // -1 - empty, 0 - goldenFrame, 1 - greenFrame, 2 - redFrame

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
        //Debug.Log(Logic.frame_i);
        if (Logic.frame_i >= 1 && Logic.frame_i <= 3)
        {
            if (Logic.IsWin())
            {
                Debug.Log("Win");
                spr.sprite = Resources.Load<Sprite>("GreenFrame");
            }
            else
            {
                Debug.Log("Loose");
                spr.sprite = Resources.Load<Sprite>("RedFrame");
            }
            Logic.frame_i++;
        }
        if (Logic.win_or_loose == 1 || Logic.win_or_loose == 2)
        {
            Destroy(gameObject, 1.0f);
            Logic.win_or_loose = 3;
        }
    }
}
