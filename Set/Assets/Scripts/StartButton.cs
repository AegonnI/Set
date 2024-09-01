using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class StartButton : MonoBehaviour
{   
    public GameObject frame;

    private static float scale;

    public float delta;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        scale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {        
        SceneManager.LoadScene("SampleScene");
    }

    private void OnMouseEnter()
    {
        gameObject.transform.localScale = new Vector3(scale + delta, scale + delta, scale + delta);
        frame.transform.localScale = new Vector3(scale + delta, scale + delta, scale + delta);
        frame.active = true;
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        frame.transform.localScale = new Vector3(scale, scale, scale);
        frame.active = false;
    }
}
