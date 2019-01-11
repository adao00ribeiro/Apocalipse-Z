using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
public class NetManager : NetworkManager
{
    public string chosenCharacter = "PlayerServe";
    public static string status = "atualizando";
    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public string chosenClass;
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        string selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);
        if (selectedClass=="PlayerServe")
        {
    
            GameObject player = Instantiate(spawnPrefabs[1], new Vector3(389.3574f, 12.7f, 275.6585f), Quaternion.identity) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        }
        else
        {
            GameObject player = Instantiate(spawnPrefabs[0], new Vector3(379.693f, 33.65f, 519.3025f), Quaternion.identity) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        }
        
      
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    
}