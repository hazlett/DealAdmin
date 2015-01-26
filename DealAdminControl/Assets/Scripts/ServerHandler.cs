using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
public class ServerHandler : MonoBehaviour {
    private static ServerHandler instance;
    public static ServerHandler Instance { get { return instance; } }
    private int updating;
    private string server = "http://hazlett206.ddns.net/Deal/AdminControl/";
    private bool authenticating, authenticated, refreshing;
    internal bool Authenticating { get { return authenticating; } }
    internal bool Authenticated { get { return authenticated; } }
    private List<Business> businesses;
    internal List<Business> Businesses { get { return businesses; } }

    private Canvas mainCanvas, loginCanvas;
    private BusinessControl businessControl;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCanvas = GameObject.Find("BusinessHome").GetComponent<Canvas>();
        loginCanvas = GameObject.Find("LoginCanvas").GetComponent<Canvas>();
        businessControl = GameObject.Find("BusinessHome").GetComponent<BusinessControl>();
        refreshing = false;
        businesses = new List<Business>();
        updating = 0;
    }
    void OnGUI()
    {
        if (authenticating)
            GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "AUTHENTICATING");
        if ((updating != 0) || (refreshing))
        {
            GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "UPDATING");
        }
    }
    internal void StartAuthenticate(string password)
    {
        authenticating = true;
        StartCoroutine(Authenticate(password));
    }

    private IEnumerator Authenticate(string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("password", password);
        WWW www = new WWW(server + "AdminAuth.php", form);
        yield return www;
        if (www.error == null)
        {
            if (www.text == "successful")
            {
                Debug.Log("Login successful");
                authenticated = true;
                authenticating = false;
                mainCanvas.enabled = true;
                loginCanvas.enabled = false;
                businessControl.enabled = true;
            }
            else
            {
                Debug.Log("Auth return not successful: " + www.text);
            }
        }
        else
        {
            Debug.Log("Authentication Error: " + www.error);
        }
        authenticating = false;
    }
    internal void CreateBusiness(string business, string content)
    {
        updating = 1;
        StartCoroutine(NewBusiness(business, content));
    }
    private IEnumerator NewBusiness(string business, string content)
    {
        WWWForm form = new WWWForm();
        form.AddField("business", business);
        form.AddField("content", content);
        WWW www = new WWW(server + "AdminCreateBusiness.php", form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Create successful");
            StartRefresh();
        }
        else
        {
            Debug.Log("Create failed");
            updating = 0;
        }
    }
    internal void StartUpdateBusiness(string business, string content)
    {
        updating = 1;
        StartCoroutine(UpdateBusiness(business, content));
    }
    private IEnumerator UpdateBusiness(string business, string content)
    {
        WWWForm form = new WWWForm();
        form.AddField("business", business);
        form.AddField("content", content);
        WWW www = new WWW(server + "AdminUpdateBusiness.php", form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Update successful");
            StartRefresh();
        }
        else
        {
            Debug.Log("Update failed");
            updating = 0;
        }      
    }
    internal void DeleteBusiness(string business)
    {
        updating = 1;
        StartCoroutine(RemoveBusiness(business));
    }
    private IEnumerator RemoveBusiness(string business)
    {
        WWWForm form = new WWWForm();
        form.AddField("business", business);
        Debug.Log("Removing: " + business);
        WWW www = new WWW(server + "AdminRemoveBusiness.php", form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Remove successful");
            StartRefresh();
        }
        else
        {
            Debug.Log("Remove failed");
            updating = 0;
        }
    }
    internal void StartRefresh()
    {
        refreshing = true;
        updating = 0;
        StartCoroutine(Refresh());
    }
    private IEnumerator Refresh()
    {
        WWW www = new WWW(server + "AdminRefresh.php");
        yield return www;
        businesses = new List<Business>();
        if (www.error == null)
        {
            Debug.Log("Refresh successful");
            List<string> names = DeserializeToList(www.text);
            updating = names.Count;
            foreach (string name in names)
            {
                StartCoroutine(GetBusiness(name));
            }
        }
        else
        {
            Debug.Log("Refresh failed: " + www.error);
        }
        refreshing = false;
    }
    private IEnumerator GetBusiness(string business)
    {
        Debug.Log("Getting Business: " + business);
        WWWForm form = new WWWForm();
        form.AddField("business", business);
        WWW www = new WWW(server + "GetBusiness.php", form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("GetBusiness successful:\n " + www.text);
            if (www.text != "")
            {
                Business biz = DeserializeToBusiness(www.text);
                if (biz != null)
                {
                    businesses.Add(biz);
                }
            }
        }
        else
        {
            Debug.Log("GetBusiness failed");
        }
        updating--;
    }

    private List<string> DeserializeToList(string text)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlSerializer xmls = new XmlSerializer(typeof(List<string>));
        XmlReader reader = new XmlNodeReader(doc);
        List<string> list = xmls.Deserialize(reader) as List<string>;
        if (list == null)
        {
            list = new List<string>();
        }
        return list;
    }
    private Business DeserializeToBusiness(string text)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlSerializer xmls = new XmlSerializer(typeof(Business));
        XmlReader reader = new XmlNodeReader(doc);
        Business biz = xmls.Deserialize(reader) as Business;
        return biz;
    }
}
