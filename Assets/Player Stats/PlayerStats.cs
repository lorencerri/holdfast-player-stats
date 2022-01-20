using HoldfastSharedMethods;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private InputField _f1MenuInputField;
    private string _token;
    private bool _feedback = true;
    
    public void SetInputField(InputField f1MenuInputField)
    {
        _f1MenuInputField = f1MenuInputField;
    }

    public InputField GetInputField()
    {
        return _f1MenuInputField;
    }

    public void SetFeedback(bool feedback) {
        _feedback = feedback;
    }

    public void SetToken(string token)
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
        form.AddField("feedback", _feedback.ToString());

        WWW www = new WWW("https://holdfast-api.plexidev.org/event", form);

        if (_feedback) StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        if (_f1MenuInputField == null) { yield break; }
        yield return www;

        Response response = JsonConvert.DeserializeObject<Response>(www.text);

        foreach (var command in response.Commands)
        {
            _f1MenuInputField.onEndEdit.Invoke(command);
        }
    }

    public class Response
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("commands")]
        public List<String> Commands { get; set; }
    }
}

