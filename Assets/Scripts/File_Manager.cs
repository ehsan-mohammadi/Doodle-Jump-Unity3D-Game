using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class File_Manager : MonoBehaviour {

    private static string File_Path;

	// Use this for initialization
	void Start () 
    {
        // Screen stay on
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        File_Path = Path.Combine(Application.persistentDataPath, "Info.DJP");
        Get_Info();
	}

    private void Get_Info()
    {
        if(File.Exists(File_Path))
        {
            string[] Data = File.ReadAllLines(File_Path);
            
            if(Data.Length != 1) // File corrupted
            {
                Data_Manager.Set_PlayerName(string.Empty);
                Data_Manager.Set_PlayerHash(string.Empty);
                Data_Manager.Set_HighScore(0);
            }
            else
            {
                // Convert data to byte
                byte[] Byte_Data = System.Convert.FromBase64String(Data[0]);

                // Convert to string
                string[] Str_Data = System.Text.Encoding.UTF8.GetString(Byte_Data).Split('|');

                if (Str_Data.Length != 3) // File corrupted
                {
                    Data_Manager.Set_PlayerName(string.Empty);
                    Data_Manager.Set_PlayerHash(string.Empty);
                    Data_Manager.Set_HighScore(0);
                }
                else
                {
                    Data_Manager.Set_PlayerName(Str_Data[0]);
                    Data_Manager.Set_PlayerHash(Str_Data[1]);
                    Data_Manager.Set_HighScore(System.Convert.ToInt32(Str_Data[2]));
                }
            }
        }
    }

    public static void Save_Info()
    {
        // Write information
        string Str_Data = Data_Manager.Get_PlayerName() + "|" + Data_Manager.Get_PlayerHash() + "|" + Data_Manager.Get_HighScore();
        byte[] Byte_Data = System.Text.Encoding.UTF8.GetBytes(Str_Data);
        string[] Data = new string[1];
        Data[0] = System.Convert.ToBase64String(Byte_Data);

        File.WriteAllLines(File_Path, Data);
    }
}
