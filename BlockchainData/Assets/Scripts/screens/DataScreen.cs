// Copyright Immutable Pty Ltd 2025
// SPDX-License-Identifier: MIT
using UnityEngine;
using TMPro;

public class DataScreen : MonoBehaviour
{
    public TextMeshProUGUI output;

    void Start()
    {
    }

    public void OnButtonClick(string buttonText) {
        if (buttonText == "ImNft") {
            string accountAddress = "0xc2e9b05b51bbd384e2a0480ea17a9b41266622b3";
            string collectionAddress = "0x29c3A209d8423f9A53Bf8AD39bBb85087a2A938B";

            ImmutableNftApi api = new ImmutableNftApi(this, output, accountAddress, collectionAddress);
            api.Go();
        }
        else if (buttonText == "BsAcc") {
            string accountAddress = "0xc2e9b05b51bbd384e2a0480ea17a9b41266622b3";

            BlockscoutAccount api = new BlockscoutAccount(this, output, accountAddress);
            api.Go();
        }
        else {
            output.text = "Not supported yet!";
        }
    }
}
