using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;

public class SubmitCallBack
{
    // CallBack after submit score
    public string Your_CallBack { get; set; }

    public SubmitCallBack(string CallBack)
    {
        Your_CallBack = CallBack;
    }
}
public class SubmitInfo
{
    // Player information to send server
    public string Your_Data { get; set; }

    public SubmitInfo(string Data)
    {
        Your_Data = Data;
    }
}
public class UserRecord
{
    // Players information to show in leaderboard; Top ten record
    public string Player_Information { get; set; }
    public int Player_HighScore { get; set; }

    public UserRecord(string Player_Info, int Player_Score)
    {
        Player_Information = Player_Info;
        Player_HighScore = Player_Score;
    }
}

public class Submit_Score : MonoBehaviour
{

    public GameObject HighScore_Canvas;
    public static string Url = "Your server URL";

    public void Post_Score(string Name, int High_Score)
    {
        if (Name != Data_Manager.Get_PlayerName())
        {
            Data_Manager.Set_PlayerName(Name);
            Data_Manager.Set_HighScore(High_Score);

            // Generate hash
            string Hash_Data = Generate_Hash("Your info");
            Data_Manager.Set_PlayerHash(Hash_Data);
        }

        SubmitInfo Info = new SubmitInfo("Your data");
        StartCoroutine(Post(Info));
    }

    public void Show_TopTen()
    {
        SubmitInfo Info = new SubmitInfo("Your data");
        StartCoroutine(TopTen_Records(Info));
    }

    private string Generate_Hash(string Hash_Data)
    {
        // Generate a hash code
        return "Your hash";
    }

    IEnumerator Post(SubmitInfo Info)
    {
        SubmitCallBack Call_Back;

        // Set server url and data to send
        string Submit_Url = Url + "/YourWebPage";

        // Post request to server
        var Request = new UnityWebRequest(Submit_Url, "POST");
        byte[] Body_Raw = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Info));
        Request.uploadHandler = (UploadHandler)new UploadHandlerRaw(Body_Raw);
        Request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        Request.SetRequestHeader("Content-Type", "application/json");
        yield return Request.Send();

        if (Request.responseCode == 200)
        {
            string Content = Request.downloadHandler.text;
            Call_Back = JsonConvert.DeserializeObject<SubmitCallBack>(Content);

            if (Call_Back.Your_CallBack == "Insert or Update your information")
            {
                // Save informations
                File_Manager.Save_Info();

                // Deactive submit score menu and active ten highscore
                Button_OnClick.Set_SubmitScoreMenu(false);
                Button_OnClick.Set_TopTenMenu(true);

                StartCoroutine(TopTen_Records(Info));
            }
            else if (Call_Back.Your_CallBack == "Name is taken")
            {
                // Server error
                Button_OnClick.Set_InteractableSubmitScore(true);
                Button_OnClick.Set_Error(true, "this name is taken");
            }
            else
            {
                // Server error
                Button_OnClick.Set_InteractableSubmitScore(true);
                Button_OnClick.Set_Error(true, "server error");
            }
        }
        else
        {
            // Server error
            Button_OnClick.Set_InteractableSubmitScore(true);
            Button_OnClick.Set_Error(true, "server error");
        }
    }

    IEnumerator TopTen_Records(SubmitInfo Info)
    {
        List<UserRecord> Records = new List<UserRecord>();

        // Set server url and data to send
        string Records_Url = Url + "/YourWebPage";

        // Post request to server
        var Request = new UnityWebRequest(Records_Url, "POST");
        byte[] Body_Raw = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Info));
        Request.uploadHandler = (UploadHandler)new UploadHandlerRaw(Body_Raw);
        Request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        Request.SetRequestHeader("Content-Type", "application/json");
        yield return Request.Send();

        if (Request.responseCode == 200)
        {
            string Content = Request.downloadHandler.text;
            Records = JsonConvert.DeserializeObject<List<UserRecord>>(Content);

            if (Records[0].Player_Information == "your server has error")
            {
                Button_OnClick.Set_RecordMessage("server error");
                Button_OnClick.Set_RecordLoading(false);
            }
            else if (Records[0].Player_Information == "There are no player yet")
            {
                Button_OnClick.Set_RecordMessage("list is empty!");
                Button_OnClick.Set_RecordLoading(false);
            }
            else
            {
                // Show records
                for (int i = 0; i < Records.Count - 1; i++)
                    Button_OnClick.Set_RecordListView(i, (i + 1) + ". " + Records[i].Player_Information, Records[i].Player_HighScore);

                // Set player record
                Button_OnClick.Set_RecordMessage(Records[Records.Count - 1].Player_Information + ": " + Records[Records.Count - 1].Player_HighScore);
            }
        }
        else
        {
            // Server error
            Button_OnClick.Set_RecordMessage("server error");
            Button_OnClick.Set_RecordLoading(false);
        }
    }
}
