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
        //chatprefab�� ����� text�� �� ����
        GameObject chat = Instantiate(Resources.Load<GameObject>("String"));
        chat.GetComponent<Text>().text = msg;

        //��ũ�� �� - content�� �ڽ����� ���
        chat.transform.SetParent(chatContent);

        //ä���� �Է��� �Ŀ� �̾ �Է��� �� �ֵ��� ����
        input.ActivateInputField();

        //input �ؽ�Ʈ�� �ʱ�ȭ
        input.text = "";
    }

}
