// Network Manager V1.4 By VF
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace AD.Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager main;
        public byte maxPlayers = 30;
        [HideInInspector] public Vector3 spawnPoint = Vector3.zero;
         
        [SerializeField] private string map;
        [SerializeField] private int countRoom;

        private void Awake() { if (main == null) { main = this; DontDestroyOnLoad(gameObject); } else{ Destroy(gameObject); } }
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            maxPlayers = 30;
        } 

        public void Join(string map) => StartCoroutine(JoinEnumerator(map));
        public void Join(string map, int identity) => StartCoroutine(JoinEnumerator(map, identity));
        IEnumerator JoinEnumerator(string map, int identity = 0)
        {
            Debug.Log("I'm going to " + map);

            if (map == "ChristmasScene")
            {
                SceneManager.LoadScene("Loading");
            }

            if (map == "MoneyExpo2024")
            {
                SceneManager.LoadScene("Loading");
            }

            if (map == "AomsinLand(DevelopScene)-Edit")
            {
                SceneManager.LoadScene("Loading");
            }            
            this.map = map;
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
            }
            else PhotonNetwork.ConnectUsingSettings();

            print(PhotonNetwork.NetworkingClient.Server);
            print(ServerConnection.MasterServer);
            print(PhotonNetwork.IsConnectedAndReady);
            yield return new WaitUntil(() => PhotonNetwork.NetworkingClient.Server == ServerConnection.MasterServer && PhotonNetwork.IsConnectedAndReady);
            if (identity == 0) JoinOrCreateRoom(map);
            else JoinOrCreatePrivateRoom(map, identity);
        }

        private void Update()
        {
            /*if(Input.GetKeyDown(KeyCode.Alpha1) && !SceneManager.GetActiveScene().name.Equals("AomsinLand(DevelopScene)-Edit"))
                vTestJoins("AomsinLand(DevelopScene)-Edit");*/
        }

        public void vTestJoins(string map)
        {
            Debug.Log("vTestJoins = " + map);
            StartCoroutine(JoinEnumerator(map));
            //SceneManager.LoadScene("Loading");
        }

        public override void OnJoinedRoom()
        {
            countRoom = 0;
            PhotonNetwork.LoadLevel(map);
        }
        private void JoinOrCreateRoom(string map)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers;
            PhotonNetwork.JoinOrCreateRoom("N" + map + countRoom, roomOptions, null);
            //Debug.Log(map + countRoom);
        }
        private void JoinOrCreatePrivateRoom(string map, int identity)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            roomOptions.IsVisible = false;
            PhotonNetwork.JoinOrCreateRoom("P" + map + identity, roomOptions, null);
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            if (returnCode == 32765)
            {
                countRoom++;
                Join(map);
            }
        }
        public override void OnDisconnected(DisconnectCause cause) => Join(map);
    }
}
