using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class PhotonPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] TextMeshProUGUI nickName;

    [SerializeField] float score;
    [SerializeField] float mouseX;
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed = 5.0f;

    [SerializeField] Vector3 direction;
    [SerializeField] Camera temporayCamera;

    void Awake()
    {
        nickName.text = photonView.Owner.NickName;
    }
    
    void Start()
    {
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporayCamera.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        Movement(Time.deltaTime);

        mouseX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    public void Movement(float time)
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        transform.position += transform.TransformDirection(direction) * speed * time;
    }

    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score);
        }
        else
        {
            score = (float)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Preasure Box"))
        {
            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            if(view.IsMine)
            {
                score++;

                PhotonNetwork.Destroy(view);
            }
        }
    }
}
