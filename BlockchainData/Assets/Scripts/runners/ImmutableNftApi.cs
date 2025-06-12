// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public class ImmutableNftApi : Runner {
    private string account;
    private string collection;

    public ImmutableNftApi(MonoBehaviour monoBehaviour, TextMeshProUGUI output, 
        string accountAddress, string collectionAddress) : base(monoBehaviour, output) {
        this.account = accountAddress;
        this.collection = collectionAddress;
    }

    public void Go() {
        this.monoBehaviour.StartCoroutine(FetchImmutableNfts(
            account, collection, ProcessNftResponse));
    }

    /**
        * Fetch NFTs for a specific account and contract from Immutable X testnet.
        *
        * @param accountAddress The account address to fetch NFTs for.
        * @param contractAddress The contract address to fetch NFTs for.
        * @param callback Function to receive the JSON response string.
        */
    public IEnumerator FetchImmutableNfts(string accountAddress, string contractAddress, System.Action<string> callback) {
        RequestInProgress = true;
        string url = $"https://api.immutable.com/v1/chains/imtbl-zkevm-mainnet/accounts/{accountAddress}/nfts?contract_address={contractAddress}";
        Log("Fetching: " + url);
        indicateRequestSent();
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(url)) {
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

    /**
    * Process the JSON response by parsing it and logging the NFT data.
    *
    * @param json The JSON response string.
    */
    public void ProcessNftResponse(string json) {
        //Log("json response: " + json);
        if (json != null) {
            try {
                var nftData = JsonUtility.FromJson<NftResponse>(json);
                if (nftData == null) {
                    Log("ERROR: NFT data null");
                }
                else if (nftData.result == null) {
                    Log("ERROR: nftData.result null");
                }
                else {
                    var tokenIds = new int[nftData.result.Length];
                    var balances = new int[nftData.result.Length];
                    Log($"Number of owned NFTs: {nftData.result.Length}");
                    for (int i = 0; i < nftData.result.Length; i++) {
                        tokenIds[i] = Int32.Parse(nftData.result[i].token_id);
                        balances[i] = Int32.Parse(nftData.result[i].balance);
                        Log($"NFT ID: {tokenIds[i]}, Balance: {balances[i]}");
                    }
                }
            } catch (Exception e) {
                Log($"ERROR: Error parsing NFT data: {e.Message}");
            }
            Log("Done");
        } else {
            Log("Failed to fetch NFTs.");
        }
        RequestInProgress = false;
    }

    [Serializable]
    private class NftResponse {
        public NftData[] result;
        //public PageData page;
    }

    [Serializable]
    private class NftData {
        //public Chain chain;
        public string token_id;
        // public string contract_address;
        // public string contract_type;
        // public string indexed_at;
        // public string updated_at;
        // public string metadata_synced_at;
        // public string metadata_id;
        // public string name;
        // public string description;
        // public string image;
        // public string external_link;
        // public string animation_url;
        // public string youtube_url;
        // public Attribute[] attributes;
        public string balance;
    }

    [Serializable]
    private class Chain {
        public string id;
        public string name;
    }

    [Serializable]
    private class Attribute {
        public string display_type;
        public string trait_type;
        public string value;
    }

    [Serializable]
    private class PageData {
        public string next_cursor;
        public string previous_cursor;
    }
}
