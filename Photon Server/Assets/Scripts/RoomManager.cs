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

    // 룸 목록 저장
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
    
    public override void OnJoinedRoom()// 룸 입장 후 호출되는 콜백함수
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

    public void OnclickCreateRoom() 
    { 
        //룸 옵션 설정
        RoomOptions Room = new RoomOptions();

        //최대 접속자 수
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        //룸 오픈 여부
        Room.IsOpen = true;

        //룸 목록 노출 여부
        Room.IsVisible = true;

        //룸 생성
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }

    public void AllDeleteRoom()
    {
        //Transform 오브젝트에 있는 하위 오브젝트에 접근하여 전체 삭제
        foreach (Transform trans in roomContent)
        {
            Destroy(trans.gameObject);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    private void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomCatalog.ContainsKey(roomList[i].Name))
            {
                //RemovedFromList 룸에서 삭제 여부
                if (roomList[i].RemovedFromList)
                {
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            roomCatalog[roomList[i].Name] = roomList[i];
        }

        
    }
}
