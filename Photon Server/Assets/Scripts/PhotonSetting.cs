using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
//using PlayFab.PfEditor.EditorModels;
using PlayFab.ClientModels;
using PlayFab;

public class PhotonSetting : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField userID;


    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0";

        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");

        PhotonNetwork.LoadLevel("Photon Lobby");

       
    }

    public void Login() 
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        
        PlayFabClientAPI.LoginWithEmailAddress(
            request,
            LoginSuccess,
            (PlayFabError error) => NotificationManager.NotificationWindow(error.ToString())
            );
    }

    public void SignUp()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = userID.text
        };

        PlayerPrefs.SetString("Name", userID.text);

        PlayFabClientAPI.RegisterPlayFabUser(
            request,
            (RegisterPlayFabUserResult result) => NotificationManager.NotificationWindow(result.ToString()),
            (PlayFabError error) => NotificationManager.NotificationWindow(error.ToString())
            );
    }
}
