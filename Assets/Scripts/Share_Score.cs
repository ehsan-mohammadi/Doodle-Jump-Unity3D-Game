using System.Collections;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

public class Share_Score : MonoBehaviour {

    public GameObject Share_Canvas;
    
    private bool Processing = false;
    private bool Focus = false;

    public void Share()
    {
        if(!Processing)
        {
            // Active canvas and set high score
            Share_Canvas.SetActive(true);
            Share_Canvas.transform.GetChild(4).GetComponent<Text>().text = Data_Manager.Get_HighScore().ToString();

            StartCoroutine(Share_ScreenShot());
        }
    }

    IEnumerator Share_ScreenShot()
    {
        Processing = true;

        yield return new WaitForEndOfFrame();

        // Capture from share canvas and save in game data
        Application.CaptureScreenshot("DoodleJump_Score.png", 2);
        string Dest_Path = Path.Combine(Application.persistentDataPath, "DoodleJump_Score.png");

        yield return new WaitForSecondsRealtime(0.3f);

        // Send capture in android
        if(!Application.isEditor)
        {
            AndroidJavaClass Intent_Class = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject Intent_Object = new AndroidJavaObject("android.content.Intent");
            Intent_Object.Call<AndroidJavaObject>("setAction", Intent_Class.GetStatic<string>("ACTION_SEND"));
            
            AndroidJavaClass Uri_Class = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject Uri_Object = Uri_Class.CallStatic<AndroidJavaObject>("parse", "file://" + Dest_Path);

            Intent_Object.Call<AndroidJavaObject>("putExtra", Intent_Class.GetStatic<string>("EXTRA_STREAM"), Uri_Object);
            Intent_Object.Call<AndroidJavaObject>("putExtra", Intent_Class.GetStatic<string>("EXTRA_TEXT"), "Can you beat my score?\n\n#Doodle_Jump");

            Intent_Object.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass Unity_Class = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject CurrentActivity_Object = Unity_Class.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject Chooser_Object = Intent_Class.CallStatic<AndroidJavaObject>("createChooser", Intent_Object, "Share your new score");
            CurrentActivity_Object.Call("startActivity", Chooser_Object);

            yield return new WaitForSecondsRealtime(1f);
        }

        yield return new WaitUntil(() => Focus);

        // Disable share canvas
        Share_Canvas.SetActive(false);
        Processing = false;
    }

    private void OnApplicationFocus(bool State)
    {
        Focus = State;
    }
}
