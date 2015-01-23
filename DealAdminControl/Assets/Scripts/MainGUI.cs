using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
    private string businessName, businessDeal;
	void Start () {
        ResetBusiness();
        ServerHandler.Instance.StartRefresh();
	}

	void Update () {
	
	}
    private void ResetBusiness()
    {
        businessName = "";
        businessDeal = "";
    }
    void OnGUI()
    {

        GUILayout.Label("<b>BUSINESSES</b>\n");
        foreach (Business business in ServerHandler.Instance.Businesses)
        {
            GUILayout.Label("Business: " + business.BusinessName);
            GUILayout.Label("Deal:");
            business.Deal = GUILayout.TextArea(business.Deal);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Update Deal"))
            {
                ServerHandler.Instance.StartUpdateBusiness(business.BusinessName, business.ToXML());
            }
            if (GUILayout.Button("Remove Business"))
            {
                ServerHandler.Instance.DeleteBusiness(business.BusinessName);
            }           
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(15.0f);

        GUILayout.Label("<b>NEW BUSINESS</b>");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Business Name: ");
        businessName = GUILayout.TextField(businessName);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Business Deal: ");
        businessDeal = GUILayout.TextArea(businessDeal);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("CREATE BUSINESS"))
        {
            Business biz = new Business(businessName, businessDeal);
            ServerHandler.Instance.CreateBusiness(businessName, biz.ToXML());
            ResetBusiness();
        }
    }
}
