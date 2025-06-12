// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;



// Documentation for API: https://docs.immutable.com/api/zkevm/reference/#/operations/ListNFTsByAccountAddress
public class ImmutableNftApi : Runner {

    public ImmutableNftApi(MonoBehaviour monoBehaviour, TextMeshProUGUI output, string accountAddress, string collectionAddress) : base(monoBehaviour, output) {
        this.url = $"https://api.immutable.com/v1/chains/imtbl-zkevm-mainnet/accounts/{accountAddress}/nfts?contract_address={collectionAddress}";
    }


    protected override void processResponseInner(string json) {
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
