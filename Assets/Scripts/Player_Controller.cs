using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    Rigidbody2D Rigid;
    public float Movement_Speed = 10f;
    private float Movement = 0;

	// Use this for initialization
	void Start () 
    {
        Rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Movement = Input.GetAxis("Horizontal") * Movement_Speed;
	}

    void FixedUpdate()
    {
        Vector2 Velocity = Rigid.velocity;
        Velocity.x = Movement;
        Rigid.velocity = Velocity;
    }
}
