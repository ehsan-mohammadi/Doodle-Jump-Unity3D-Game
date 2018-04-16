using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour {

    private bool Attach = false;
    private bool Fall = false;
    private float Destroy_Distance;

    private GameObject Game_Controller;

    // Use this for initialization
    void Start()
    {
        Game_Controller = GameObject.Find("Game_Controller");

        // Set distance to destroy the propeller out of screen
        Destroy_Distance = Game_Controller.GetComponent<Game_Controller>().Get_DestroyDistance();
    }

	void FixedUpdate () 
    {
        // Propeller fall
        if(Fall)
        {
            GetComponent<AudioSource>().Stop();
            transform.Rotate(new Vector3(0, 0, -3.5f));
            transform.position -= new Vector3(0, 0.3f, 0);
            
            // Destroy propeller
            if (transform.position.y - Camera.main.transform.position.y < Destroy_Distance)
                Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Player" && !Attach)
        {
            if (Other.transform.childCount == 0)
            {
                // Set propeller parent
                transform.parent = Other.transform;
                transform.localPosition = new Vector3(0, -0.02f, 0);
                GetComponent<BoxCollider2D>().enabled = false;

                // Add force to up
                Rigidbody2D Rigid = Other.collider.GetComponent<Rigidbody2D>();

                if (Rigid != null)
                {
                    Vector2 Force = Rigid.velocity;
                    Force.y = 80f;
                    Rigid.velocity = Force;

                    // Play propeller sound
                    GetComponent<AudioSource>().Play();

                    // Set propeller animation
                    GetComponent<Animator>().SetBool("Active", true);

                    // Propeller sprite send to front
                    GetComponent<SpriteRenderer>().sortingOrder = 12;
                }

                Attach = true;
            }
        }
    }

    public void Set_Fall(GameObject Player)
    {
        Fall = true;

        // Active player colider
        Player.GetComponent<BoxCollider2D>().enabled = true;
    }
}
