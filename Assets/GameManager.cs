using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class GameManager : MonoBehaviour
    {
        public GameObject PlayerController;
        public GameObject AiController;
        public List<PlayerInformation> Players = new List<PlayerInformation>();
        public PlayerStartData DefaultStartData;
        public static PlayerInformation Default = null;

        private void Awake()
        {
            foreach (var player in Players)
            {
                if (player.IsAi)
                    CreateNewAiPlayer(player);
                else
                    CreateDefaultPlayer(player);
                DefaultStartData.SetupPlayerStart(player);
            }
        }

        private void CreateNewAiPlayer(PlayerInformation player)
        {
            GameObject aiController = Instantiate(AiController);
            aiController.GetComponentInChildren<SelectionController>().SetPlayer(player);
        }

        private void CreateDefaultPlayer(PlayerInformation player)
        {
            GameObject playerController = Instantiate(PlayerController);
            playerController.GetComponentInChildren<SelectionController>().SetPlayer(player);
            if (Default == null)
            {
                Default = player;
            }
        }


        private static void SetDefaultPlayer(PlayerInformation player)
        {
            if (!player.IsAi && Default == null)
            {
                Default = player;
            }//FindObjectOfType<SelectionController>()._player = player; }
        }
    } 
}
