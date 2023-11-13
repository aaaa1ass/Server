using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreate;
    public InputField roomName;
    public InputField roomPerson;
    public Transform roomContent;

    // �� ��� ����
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();


    // Update is called once per frame
    void Update()
    {
        if (roomName.text.Length > 0 && roomPerson.text.Length > 0)
        {
            roomCreate.interactable = true;
        }
        else
        {
            roomCreate.interactable = false;
        }
    }
    
    public override void OnJoinedRoom()// �� ���� �� ȣ��Ǵ� �ݹ��Լ�
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        foreach(RoomInfo info in roomCatalog.Values)
        {
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            room.transform.SetParent(roomContent);

            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
