using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Generator : MonoBehaviour {

    public GameObject Platform;

	// Use this for initialization
	void Start () 
    {
        float Current_Y = 0;
        float Offset = 1f;

        Vector3 Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        for (int i = 0; i < 100; i++)
        {
            float Dist_X = Random.Range(Top_Left.x + Offset, -Top_Left.x - Offset);
            float Dist_Y = Random.Range(2f, 4f);

            Current_Y += Dist_Y;
            Vector3 Platform_Pos = new Vector3(Dist_X, Current_Y, 0);

            Instantiate(Platform, Platform_Pos, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
