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
    }
}
