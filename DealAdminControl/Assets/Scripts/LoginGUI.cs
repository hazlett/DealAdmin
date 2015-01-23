using UnityEngine;
using System.Collections;

public class LoginGUI : MonoBehaviour {
    private string password = "";

    void OnGUI()
    {
        password = GUI.PasswordField(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.2f), password, '*');
        if (GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.5f, Screen.width * 0.8f, Screen.height * 0.2f), "LOGIN"))
        {
            ServerHandler.Instance.StartAuthenticate(password);
        }
    }
}
