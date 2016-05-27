using UnityEngine;
using System.Collections;

public class AddDialogue : MonoBehaviour
{
    private GameObject _step1;
    private GameObject _step2;
    private GameObject _step3;



    // Use this for initialization
    private void Start ()
    {
        _step1 = transform.FindChild("Step1").gameObject;
        _step2 = transform.FindChild("Step2").gameObject;
        _step3 = transform.FindChild("Step3").gameObject;
    }

    public void ShowStep2()
    {
        _step2.SetActive(true);   
    }

    public void HideStep2()
    {
        _step2.SetActive(false);
    }

    public void ShowStep3Buildings()
    {
        HideStep3();
        _step3.SetActive(true);
        _step3.transform.FindChild("Buildings").gameObject.SetActive(true);
    }

    public void ShowStep3Trees()
    {
        HideStep3();
        _step3.SetActive(true);
        _step3.transform.FindChild("Trees").gameObject.SetActive(true);
    }

    public void HideStep3()
    {
        _step3.transform.FindChild("Buildings").gameObject.SetActive(false);
        _step3.transform.FindChild("Trees").gameObject.SetActive(false);
        _step3.SetActive(false);
    }

    public void ToogleMenu()
    {
        if (_step2.activeSelf)
        {
            HideStep2();
            HideStep3();
        }
        else
        {
            ShowStep2();
        }
    }
}
