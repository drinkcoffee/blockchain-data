// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public class BlockscoutTokens : Runner {

    public BlockscoutTokens(MonoBehaviour monoBehaviour, TextMeshProUGUI output, string accountAddress) : base(monoBehaviour, output) {
        Log("Fetching list of tokens owned by address");
        this.url = $"https://explorer.immutable.com/api/v2/addresses/{accountAddress}/token-balances";
    }

    protected override void processResponseInner(string json) {
        Log("json response: " + json);
    }
}
