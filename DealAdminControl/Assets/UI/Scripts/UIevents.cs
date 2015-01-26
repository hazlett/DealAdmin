using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIevents : MonoBehaviour {

    private Canvas businessHome, newBusiness, editBusiness;
    private Button dollarSign1, dollarSign2, dollarSign3, dollarSign4, dollarSign5;
    private InputField name, type, tag, address, phone, website;
	// Use this for initialization
	void Start () {
        InitializeDollarButtons();
        InitializeCanvases();
        InitializeInputFields();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SelectPrice(int number)
    {
        switch (number)
        {
            case 1: Activate(dollarSign1);
                Deactivate(dollarSign2);
                Deactivate(dollarSign3);
                Deactivate(dollarSign4);
                Deactivate(dollarSign5);
                break;
            case 2: Activate(dollarSign1);
                Activate(dollarSign2);
                Deactivate(dollarSign3);
                Deactivate(dollarSign4);
                Deactivate(dollarSign5);
                break;
            case 3: Activate(dollarSign1);
                Activate(dollarSign2);
                Activate(dollarSign3);
                Deactivate(dollarSign4);
                Deactivate(dollarSign5);
                break;
            case 4: Activate(dollarSign1);
                Activate(dollarSign2);
                Activate(dollarSign3);
                Activate(dollarSign4);
                Deactivate(dollarSign5);
                break;
            case 5: Activate(dollarSign1);
                Activate(dollarSign2);
                Activate(dollarSign3);
                Activate(dollarSign4);
                Activate(dollarSign5);
                break;
        }
    }

    private void Activate(Button button)
    {
        ColorBlock newColor = button.colors;
        newColor.normalColor = new Color(1, 1, 1, 1);
        button.colors = newColor;
    }

    private void Deactivate(Button button)
    {
        ColorBlock newColor = button.colors;
        newColor.normalColor = new Color(1, 1, 1, 0);
        button.colors = newColor;
    }

    public void CancelNewBusiness()
    {
        newBusiness.enabled = false;
        businessHome.enabled = true;

        ClearFields();
    }

    public void NewBusinessScreen()
    {
        newBusiness.enabled = true;
        businessHome.enabled = false;
    }

    private void InitializeDollarButtons()
    {
        dollarSign1 = GameObject.Find("DollarSign1").GetComponent<Button>();
        dollarSign2 = GameObject.Find("DollarSign2").GetComponent<Button>();
        dollarSign3 = GameObject.Find("DollarSign3").GetComponent<Button>();
        dollarSign4 = GameObject.Find("DollarSign4").GetComponent<Button>();
        dollarSign5 = GameObject.Find("DollarSign5").GetComponent<Button>();
    }

    private void InitializeCanvases()
    {
        businessHome = GameObject.Find("BusinessHome").GetComponent<Canvas>();
        newBusiness = GameObject.Find("NewBusiness").GetComponent<Canvas>();
    }

    private void InitializeInputFields()
    {
        name = GameObject.Find("NameField").GetComponent<InputField>();
        tag = GameObject.Find("TagsField").GetComponent<InputField>();
        type = GameObject.Find("TypeField").GetComponent<InputField>();
        address = GameObject.Find("AddressField").GetComponent<InputField>();
        phone = GameObject.Find("PhoneField").GetComponent<InputField>();
        website = GameObject.Find("WebsiteField").GetComponent<InputField>();
    }

    private void ClearFields()
    {
        name.text = tag.text = type.text = address.text = phone.text = website.text = "";
    }
}
