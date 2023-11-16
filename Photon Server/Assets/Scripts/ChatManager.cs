using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField input;
    public Transform chatContent;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            input.ActivateInputField();

            if (input.text.Length == 0)
                return;
            string chat = PhotonNetwork.NickName + " : " + input.text;

            photonView.RPC(nameof(Chatting), RpcTarget.All);
        }

    }

    void Chatting(string msg)
    {
        //chatprefab을 만들어 text에 값 저장
        GameObject chat = Instantiate(Resources.Load<GameObject>("String"));
        chat.GetComponent<Text>().text = msg;

        //스크롤 뷰 - content에 자식으로 등록
        chat.transform.SetParent(chatContent);

        //채팅을 입력한 후에 이어서 입력할 수 있도록 설정
        input.ActivateInputField();

        //input 텍스트를 초기화
        input.text = "";
    }

}
