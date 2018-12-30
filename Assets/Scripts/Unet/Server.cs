using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Server : MonoBehaviour
{
    private uint roomSize = 6;

    private string roomName;

    NetworkManager networkManager;
    public GameObject playerServe;

void Start()
    {
        networkManager = NetworkManager.singleton;

        if (networkManager.matchMaker == null)
        {

            networkManager.playerPrefab = playerServe;
            networkManager.StartMatchMaker();
            CreateInternetMatch("Servidor");
        }

       
    }
   

    //call this method to request a match to be created on the server
    public void CreateInternetMatch(string matchName)
{
        networkManager.matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
}

//this method is called when your request for creating a match is returned
private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
{
    if (success)
    {
        //Debug.Log("Create match succeeded");

        MatchInfo hostInfo = matchInfo;
        NetworkServer.Listen(hostInfo, 9000);

        NetworkManager.singleton.StartHost(hostInfo);
            print("SERVIDOR CRIADO");
    }
    else
    {
        Debug.LogError("Create match failed");
    }
}

    public void SetRoomName(string _name)
    {
        roomName = _name;
    }
}