using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_White : MonoBehaviour {

	public void Deactive()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<PlatformEffector2D>().enabled = false;
    }
}
