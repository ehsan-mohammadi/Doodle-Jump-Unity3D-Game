using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Game_Controller : MonoBehaviour {

    private GameObject Player;

    private float Max_Height = 0;
    public Text Txt_Score;

    private int Score;

    private Vector3 Top_Left;
    private Vector3 Camera_Pos;

    private bool Game_Over = false;

    public Text Txt_GameOverScore;
    public Text Txt_GameOverHighsocre;

	void Awake () 
    {
        Player = GameObject.Find("Doodler");

        // Initialize boundary 
        Camera_Pos = Camera.main.transform.position;
        Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
	}
	
	void FixedUpdate () 
    {
        if(!Game_Over)
        {
            // Calculate max height
            if (Player.transform.position.y > Max_Height)
            {
                Max_Height = Player.transform.position.y;
            }

            // Check player fall
            if (Player.transform.position.y - Camera.main.transform.position.y < Get_DestroyDistance())
            {
                // Play game over sound
                GetComponent<AudioSource>().Play();
                
                // Set game over
                Set_GameOver();
                Game_Over = true;
            }
        }
	}

    void OnGUI()
    {
        // Set score
        Score = (int)(Max_Height * 50);
        Txt_Score.text = Score.ToString();
    }

    public bool Get_GameOver()
    {
        return Game_Over;
    }

    public float Get_DestroyDistance()
    {
        return Camera_Pos.y + Top_Left.y;
    }

    void Set_GameOver()
    {
        if (Data_Manager.Get_HighScore() < Score)
            Data_Manager.Set_HighScore(Score);

        Txt_GameOverScore.text = Score.ToString();
        Txt_GameOverHighsocre.text = Data_Manager.Get_HighScore().ToString();
        GameObject Background_Canvas = GameObject.Find("Background_Canvas");

        // Active game over menu
        Button_OnClick.Set_GameOverMenu(true);

        // Enable animation
        Background_Canvas.GetComponent<Animator>().enabled = true;

        // Save file
        File_Manager.Save_Info();
    }
}
