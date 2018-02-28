using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    public Transform Target;

	void LateUpdate () 
    {
        if (Target.position.y > transform.position.y)
        {
            Vector3 New_Pos = new Vector3(transform.position.x, Target.position.y, transform.position.z);
            transform.position = New_Pos;
        }
	}
}
