using HoldfastSharedMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private InputField _f1MenuInputField;
    private string _token;

    public void setInputField(InputField f1MenuInputField)
    {
        _f1MenuInputField = f1MenuInputField;
    }

    public InputField getInputField()
    {
        return _f1MenuInputField;
    }

    public void setToken(string token)
    {
        _token = token;
    }

    public void SendEvent(string e, WWWForm form)
    {

        if (_token.Equals(""))
        {
            Debug.Log("No token provided for PlayerStatsMod, aborting stat post...");
            return;
        }

        form.AddField("event", e);
        form.AddField("token", _token);

        Debug.Log("Sending event to API for processing...");
        WWW www = new WWW("https://holdfast-api.plexidev.org/event", form);

        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        Debug.Log(www.text);
    }
}