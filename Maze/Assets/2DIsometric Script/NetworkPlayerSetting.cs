using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerSetting : NetworkManager {


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0f, 0.64f, -0.64f), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }
}
