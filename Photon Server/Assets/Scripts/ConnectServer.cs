using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectServer : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_Dropdown server;

    private void Awake()
    {
        server.options[0].text = "Onion";
        server.options[1].text = "Weather";
        server.options[2].text = "Solo";
    }

    public void SelectServer()
    {
        // ���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

    public override void OnConnectedToMaster() 
    {
        PhotonNetwork.JoinLobby
        (
                new TypedLobby
                (
                    server.options[server.value].text,
                    LobbyType.Default
                )
        );
    }
}
