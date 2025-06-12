// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public abstract class Runner {
    protected MonoBehaviour monoBehaviour;
    private TextMeshProUGUI outputText;
    protected string url;
    protected string postBody = null;
    protected string authToken = null;

    protected uint network = 13371;

    private DateTime start;
    public bool RequestInProgress = false;
    private string displayOut;


    public Runner(MonoBehaviour monoBehaviour, TextMeshProUGUI output) {
        this.monoBehaviour = monoBehaviour;
        this.outputText = output;
    }

    public void Go() {
        if (this.postBody == null) {
            this.monoBehaviour.StartCoroutine(fetchGet(processResponse));
        }
        else {
            this.monoBehaviour.StartCoroutine(fetchPost(processResponse));
        }
    }

    private IEnumerator fetchGet(System.Action<string> callback) {
        RequestInProgress = true;
        Log($"Fetching: {this.url}");
        indicateRequestSent();
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(this.url)) {
            request.SetRequestHeader("Accept", "application/json");
            yield return request.SendWebRequest();
            indicateResponseReceived();

            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success) {
                callback?.Invoke(request.downloadHandler.text);
            } else {
                Log($"ERROR: Error fetching NFTs: {request.error}");
                callback?.Invoke(null);
            }
        }
    }

    private IEnumerator fetchPost(System.Action<string> callback) {
        RequestInProgress = true;
        Log($"Fetching: {this.url}, with: {this.postBody}");
        indicateRequestSent();
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Post(this.url, this.postBody, "application/json")) {
            request.SetRequestHeader("Accept", "application/json");
            if (this.authToken != null) {
                request.SetRequestHeader("Authorization", "Bearer " + this.authToken);
            }
            yield return request.SendWebRequest();
            indicateResponseReceived();

            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success) {
                callback?.Invoke(request.downloadHandler.text);
            } else {
                Log($"ERROR: Error fetching NFTs: {request.error}");
                callback?.Invoke(null);
            }
        }
    }

    private void processResponse(string json) {
        //Log("json response: " + json);
        if (json != null) {
            try {
                processResponseInner(json);
            } catch (Exception e) {
                Log($"ERROR: Error parsing response: {e.Message}");
            }
        } else {
            Log("Response was null");
        }
        Log("Done");
        RequestInProgress = false;
    }

    protected abstract void processResponseInner(string json);

    protected void LogWithTime(string log) {
        string timestamp = DateTime.Now.ToString("yyyyMMdd: HHmmss.fff");
        string logEntry = $"{timestamp}: {log}";
        Log(logEntry);
    }

    protected void Log(string log) {
        Debug.Log(log);
        displayOut += log + "\n";
        outputText.text = displayOut;
    }


    protected void indicateRequestSent() {
        LogWithTime("Request sent");
        start = DateTime.Now;
    }

    protected void indicateResponseReceived() {
        LogWithTime("Response received");
        var now = DateTime.Now;
        TimeSpan interval = now - start;
        LogWithTime($"Latency: {interval.TotalMilliseconds} milli-seconds");
    }
}
