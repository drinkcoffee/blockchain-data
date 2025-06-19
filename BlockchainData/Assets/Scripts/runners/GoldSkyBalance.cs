// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public class GoldSkyBalance : Runner {

    public GoldSkyBalance(MonoBehaviour monoBehaviour, TextMeshProUGUI output, string tokenAddress) : base(monoBehaviour, output) {
        Log("Fetching balances of an ERC20 contract");
        this.url = $"https://api.goldsky.com/api/public/project_clmk66h022r092n0k4omt6w0e/subgraphs/test-erc20-subgraph/1.0.0/gn";
        this.postBody = 
            "{ \"query\": " +
            " \"{ users(where: {address: \\\"0x046761f459dc35814a87fe338c8a377d34f0bc41\\\"}) " +
//            " \"{ users " +
            " { balance  address }" +
            "}\"}";
        this.authToken = "cmc1g59zbzcfb01ru0w5w8100";
    }

    protected override void processResponseInner(string json) {
        Log("json response: " + json);
    }
}
