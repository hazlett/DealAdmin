using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BusinessControl : MonoBehaviour {

    public RectTransform contentPanel;

    void OnEnable()
    {
        PopulateList();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void PopulateList()
    {
        Debug.Log("Populating List");
        foreach (Business business in ServerHandler.Instance.Businesses)
        {
            Debug.Log(business.BusinessName);
        }
    }
}
