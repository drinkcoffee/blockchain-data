// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public class Runner {
    protected MonoBehaviour monoBehaviour;
    private TextMeshProUGUI outputText;

    private DateTime start;
    public bool RequestInProgress = false;
    private string displayOut;

    public Runner(MonoBehaviour monoBehaviour, TextMeshProUGUI output) {
        this.monoBehaviour = monoBehaviour;
        this.outputText = output;
    }

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
