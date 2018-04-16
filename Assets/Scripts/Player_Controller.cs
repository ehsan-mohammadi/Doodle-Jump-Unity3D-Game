using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    Rigidbody2D Rigid;
    public float Movement_Speed = 10f;
    private float Movement = 0;
    private int Direction = 1;
    private Vector3 Player_LocalScale;

    public Sprite[] Spr_Player = new Sprite[2];

	// Use this for initialization
	void Start () 
    {
        Rigid = GetComponent<Rigidbody2D>();
        Player_LocalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Set Movement value
        Movement = Input.acceleration.x * Movement_Speed; //Input.GetAxis("Horizontal") * Movement_Speed; //Input.acceleration.x * Movement_Speed;
        
        // Player look right or left
        if (Movement > 0)
            transform.localScale = new Vector3(Player_LocalScale.x, Player_LocalScale.y, Player_LocalScale.z);
        else if (Movement < 0)
            transform.localScale = new Vector3(-Player_LocalScale.x, Player_LocalScale.y, Player_LocalScale.z);
	}

    void FixedUpdate()
    {
        // Calculate player velocity
        Vector2 Velocity = Rigid.velocity;
        Velocity.x = Movement;
        Rigid.velocity = Velocity;

        // Player change sprite
        if (Velocity.y < 0)
        {
            GetComponent<SpriteRenderer>().sprite = Spr_Player[0];

            // Active player collider
            GetComponent<BoxCollider2D>().enabled = true;

            // Fall propeller after fly up
            Propeller_Fall();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Spr_Player[1];

            // Deactive player collider
            GetComponent<BoxCollider2D>().enabled = false;
        }

        // Player wrap
        Vector3 Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        float Offset = 0.5f;

        if (transform.position.x > -Top_Left.x + Offset)
            transform.position = new Vector3(Top_Left.x - Offset, transform.position.y, transform.position.z);
        else if(transform.position.x < Top_Left.x - Offset)
            transform.position = new Vector3(-Top_Left.x + Offset, transform.position.y, transform.position.z);
    }

    void Propeller_Fall()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Active", false);
            transform.GetChild(0).GetComponent<Propeller>().Set_Fall(gameObject);
            transform.GetChild(0).parent = null;
        }
    }
}
