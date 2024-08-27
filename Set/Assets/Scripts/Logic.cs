using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{

    public static int[,] set = new int[3,4]; // выбранные игроком карты(их ствойства)
    public static byte count_i = 0; // для счета и сброса, чтобы выбраны были 3 карты
    public static byte win_or_loose = 0; // 0 - none | 1 - win | 2 - loose

    //public static float myTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count_i == 3)
        {           
            if (IsWin())
                win_or_loose = 1;
            else 
                win_or_loose = 2;

            //myTime = Time.time;

            set = new int[3, 4];
            count_i = 0;
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
