using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject preasureBox;

    private void Awake()
    {
        PhotonNetwork.Instantiate
            (
                "Character",
                RandomPosition(5),
                Quaternion.identity
            );
    }

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CreateObject());
        }
    }

    public Vector3 RandomPosition(int value)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction *= value;

        direction.y = 0;

        return direction;
    }

    private IEnumerator CreateObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            PhotonNetwork.Instantiate
                (
                    "Treasure Box",
                    RandomPosition(25),
                    Quaternion.identity
                );
        }

    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}
