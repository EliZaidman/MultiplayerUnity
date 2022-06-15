using Photon.Pun;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public int PlayerAmount, MaxPlayers;

    public Room(string name, int playerAmount, int maxPlayers)
    {
        this.Name = name;
        this.MaxPlayers = maxPlayers;
        this.PlayerAmount = playerAmount;
    }   

    public void JoinRoom()
    {
        Debug.Log("JoiningRoom");

        if (Name != null)
            PhotonNetwork.JoinRoom(Name);
    }

}