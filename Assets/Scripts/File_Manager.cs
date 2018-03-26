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

        File_Path = "Your path";
        Get_Info();
	}

    private void Get_Info()
    {
        // Do something to get information from file
    }

    public static void Save_Info()
    {
        // Write information to file
    }
}
