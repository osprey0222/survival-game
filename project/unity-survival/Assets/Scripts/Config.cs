using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    internal static readonly float COUNT_TIME = 0.5f;//ms
    internal static readonly float TRACK_LENGTH = 100f;
    internal static readonly float QUALIFY_TIME = 14f;
    internal static readonly uint HIGH_SCORE = 50000;
    internal static readonly float FIRST_LOGO_TIME = 4.0f;
    internal static readonly float SECOND_LOGO_TIME = 3.0f;
    internal static readonly float THIRD_LOGO_TIME = 4.0f;
    internal static readonly float MAIN_LOGO_TIME = 1.0f;
    public static float LOADING_TIME = 0f;
    public static float TIMEER_INTERVAL = 1f;
    public static string UI_PREFAB_PATH = "Prefab/UI/";
    public static Dictionary<int, LevelParam> LEVELS_INFO = new Dictionary<int, LevelParam>() {
        { -1, new LevelParam(1, 0.05f,1) } ,
        { 0, new LevelParam(2, 0.1f,3) } ,
        { 1, new LevelParam(3, 0.15f,5) } ,
        { 2, new LevelParam(4, 0.2f,7) } ,
        { 3, new LevelParam(4, 0.25f,8) } ,
        { 4, new LevelParam(4, 0.3f,10) } ,
        { 5, new LevelParam(4, 0.35f,12) } ,
        { 6, new LevelParam(4, 0.4f,14) } ,
        { 7, new LevelParam(5, 0.45f,16) } ,
        { 8, new LevelParam(5, 0.5f,18) } ,
        { 9, new LevelParam(5, 0.55f,20) } ,
        { 10, new LevelParam(6, 0.6f,22) } ,
        {11, new LevelParam(6, 0.65f,25) } ,
    };
}
public class LevelParam
{
    public int ToyCount { get; set; }
    public float ToyPlusSpeed { get; set; }
    public int CookieGoalCount { get; set; }
    public LevelParam(int toyCount, float plusSpeed, int cookieCount)
    {
        ToyCount = toyCount;
        ToyPlusSpeed = plusSpeed;
        CookieGoalCount = cookieCount;
    }
}
