using HoldfastSharedMethods;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO
// Queue system for API events

public class PlayerStats : MonoBehaviour {

    public PlayerStats() {
        this.Feedback = true;
        this.IsConfigSet = false;
    }

    public InputField F1MenuInputField { get; set; }
    public string Token { get; set; }
    public bool Feedback { get; set; }
    public bool IsConfigSet { get; set; }

    public void SendEvent(string e, WWWForm form) {
        StartCoroutine(SendRequest(e, form));
    }

    IEnumerator SendRequest(string e, WWWForm form) {
        Debug.Log("[PlayerStatsMod] Sending request...");

        if (F1MenuInputField == null) {
            Debug.Log("[PlayerStatsMod] F1MenuInputField not yet initialized, aborting stat post...");
            yield break; 
        }

        // Check if the config has been set
        if (!IsConfigSet) { // If it hasn't, try again every 250ms for the next five seconds.
            int attempts = 20;
            while (attempts > 0) {
                Debug.Log("[PlayerStatsMod] Config has not yet been initialized, waiting 250ms... | Attempts Remaining: " + attempts);
                yield return new WaitForSeconds(0.25f);
                if (IsConfigSet) break;
                attempts -= 1;
            }
        }

        // Check if Token has been initialized
        if (Token == null) {
            Debug.Log("[PlayerStatsMod] No token provided, aborting stat post...");
            yield break;
        }

        // Add global options to form
        form.AddField("event", e);
        form.AddField("token", Token);
        form.AddField("feedback", Feedback.ToString());

        // Create request
        WWW www = new WWW("https://holdfast-api.plexidev.org/event", form);

        // Check if feedback is required
        if (!Feedback) yield break;
        yield return www;

        // Deserialize to JSON
        Response response = JsonConvert.DeserializeObject<Response>(www.text);

        // Select what to do based on the event
        if (e == "playerKilledPlayer") {
            // Run the commands received from the server
            foreach (var command in response.Commands) {
                F1MenuInputField.onEndEdit.Invoke(command);
            }
        } else if (e == "roundStart") {
            // Store the round to reference

        }

    }

    public class Response {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("commands")]
        public List<String> Commands { get; set; }
    }
}

