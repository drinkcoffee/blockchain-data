// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public class CodexTokenInfo : Runner {

    public CodexTokenInfo(MonoBehaviour monoBehaviour, TextMeshProUGUI output, string tokenAddress) : base(monoBehaviour, output) {
        Log("Fetching information about an ERC20 contract");
        this.url = $"https://graph.codex.io/graphql";
        this.postBody = 
        "query {" +
          $"token(input: {{ address: \"{tokenAddress}\", networkId: ${network} }}) {{" +
   	        "id " +
            "address " +
            "name" +
         "} }";
        this.authToken = "16671736f2c76a52a813d6d9b949ceb80ecabb6e";
    }

    protected override void processResponseInner(string json) {
        Log("json response: " + json);
    }
}
