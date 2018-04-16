using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager 
{
    private static int High_Score = 0;
    private static string Player_Name = string.Empty;
    private static string Player_Hash = string.Empty;

    public static void Set_HighScore(int Score)
    {
        High_Score = Score;
    }

    public static int Get_HighScore()
    {
        return High_Score;
    }

    public static void Set_PlayerName(string Name)
    {
        Player_Name = Name;
    }

    public static string Get_PlayerName()
    {
        return Player_Name;
    }

    public static void Set_PlayerHash(string Hash)
    {
        Player_Hash = Hash;
    }

    public static string Get_PlayerHash()
    {
        return Player_Hash;
    }
}
