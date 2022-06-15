using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string ourGameVersion = "1.0";
    [SerializeField] GameObject LobbyScreen;
    [SerializeField] GameObject RoomUIPrefab;
    [SerializeField] GameObject RoomUIParent;
    [SerializeField] TMP_InputField roomNameToCreate;

    List<Room> lobbyRoomList;
    bool latestRoomInfo = false;
    string roomName;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = ourGameVersion;
    }

    public void UIUpdateRoomInfo()
    {
        if (PhotonNetwork.InLobby)
        {
            latestRoomInfo = false;
            PhotonNetwork.LeaveLobby();
        }
    }

    public void UICreateRoom()
    {
        roomName = roomNameToCreate.text;
        bool nameExists = false;

        if (lobbyRoomList.Count > 0)
            foreach (var item in lobbyRoomList)
                if (roomName == item.Name)
                    nameExists = true;

        if (roomName == "")
            OnCreateRoomFailed(1, "Room Name Can't be Empty");
        else if (nameExists)
            OnCreateRoomFailed(1, "Room name already exists, try another one.");
        else
            PhotonNetwork.CreateRoom(roomName);

    }

    #region Photon Methods

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        if (!LobbyScreen.activeSelf)
            LobbyScreen.SetActive(true);

        Debug.Log("joined lobby");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        PhotonNetwork.LoadLevel(1);
        Debug.Log("room was crearted");
    }

    //We want a welcome or "x" joined to the game? if true fill method v
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log($"Room creation was failed: {returnCode}, {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join Room, Reloading.");
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        if (!latestRoomInfo)
        {
            this.lobbyRoomList = new List<Room>();

            if (roomList.Count > 0)
                foreach (RoomInfo rInfo in roomList)
                {
                    //transferring room list info into actual Data
                    this.lobbyRoomList.Add(new Room(rInfo.Name, rInfo.PlayerCount, rInfo.MaxPlayers));
                }

            //Destroy UI room list
            if (RoomUIParent.transform.childCount > 0)
                foreach (Transform room in RoomUIParent.transform)
                    Destroy(room.gameObject);

            //Create new UI room list
            if (this.lobbyRoomList.Count > 0)
                foreach (Room existingRoom in this.lobbyRoomList)
                {
                    GameObject newRoomUI = Instantiate(RoomUIPrefab, RoomUIParent.transform);
                    InitRoomUI(newRoomUI, existingRoom.Name, existingRoom.PlayerAmount, existingRoom.MaxPlayers);
                }

            latestRoomInfo = true;
        }
    }

    public override void OnLeftLobby()
    {
        if (!latestRoomInfo)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #endregion

    private void InitRoomUI(GameObject instantiatedPrefab, string name, int playerAmount, int MaxPlayers)
    {
        Room roomComponent = instantiatedPrefab.GetComponent<Room>();
        roomComponent.PlayerAmount = playerAmount;
        roomComponent.MaxPlayers = MaxPlayers;
        roomComponent.Name = name;

        instantiatedPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        instantiatedPrefab.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{playerAmount}/{MaxPlayers}";
    }

}