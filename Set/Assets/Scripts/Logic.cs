using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{

    public static int[,] set = new int[3,4];
    public static byte count_i = 0, frame_i = 0, win_or_loose = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count_i == 3)
        {
            frame_i = 1;
            //set = new int[3, 4];
            if (IsWin())
                win_or_loose = 1;
            else 
                win_or_loose = 2;
            //count_i = 0;
        }
    }

    public static bool IsWin()
    {
        return Spawn.IsEqualOrUnqual(set[0, 0], set[1, 0], set[2, 0]) &&
            Spawn.IsEqualOrUnqual(set[0, 1], set[1, 1], set[2, 1]) &&
            Spawn.IsEqualOrUnqual(set[0, 2], set[1, 2], set[2, 2]) &&
            Spawn.IsEqualOrUnqual(set[0, 3], set[1, 3], set[2, 3]);
    }
}
