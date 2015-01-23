using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginHandler : MonoBehaviour {

    private InputField password;

	void Start () {
        password = GameObject.Find("InputField").GetComponent<InputField>();
	}

    public void Authenticate()
    {
        Debug.Log(password.text);
        ServerHandler.Instance.StartAuthenticate(password.text);
    }
}
