using System;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace Player
{
    public class DialogManager : MonoBehaviour
    {
        public TextManager textManager;
        private SpawnNPCs spawnNPCs;
        private SocialScoreManager socialScoreManager;

        [SerializeField] private GameObject canvas;

        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private TextMeshProUGUI choice1;
        [SerializeField] private TextMeshProUGUI choice2;
        [SerializeField] private TextMeshProUGUI choice3;
        [SerializeField] private int firstChoice = -1;

        public GameObject NPC;


        public static DialogManager Instance { get; private set; }
        private void Awake()
        {
            canvas.SetActive(false);
            if (Instance != null && Instance != this)
            {
                // If another instance exists, destroy this one to enforce the singleton.
                Debug.Log("dis is gebroken");
                Destroy(this.gameObject);
            }
            else
            {
                // Set this instance as the singleton.
                Instance = this;
                Debug.Log(gameObject.name);
            }
        }

        private void Start()
        {
            socialScoreManager = SocialScoreManager.Instance;
            spawnNPCs = SpawnNPCs.Instance;
            textManager = TextManager.Instance;
        }

        public void StartDialog()
        {
            canvas.SetActive(true);
            mainText.text = textManager.currentTextFile.opener;
            choice1.text = textManager.currentTextFile.playerOptions[0];
            choice2.text = textManager.currentTextFile.playerOptions[1];
            choice3.text = textManager.currentTextFile.playerOptions[2];
        }

        public void ChooseChoice1()
        {
            if (firstChoice == -1)
            {
                mainText.text = textManager.currentTextFile.response[0];
                choice1.text = textManager.currentTextFile.PlayerResponsesStrings1[0];
                choice2.text = textManager.currentTextFile.PlayerResponsesStrings1[1];
                choice3.text = textManager.currentTextFile.PlayerResponsesStrings1[2];
                firstChoice = 1;
            }
            else
            {
                switch (firstChoice)
                {
                    case 1:
                        socialScoreManager.SubtractFromScore(5);
                        break;
                    case 2:
                        socialScoreManager.SubtractFromScore(4);
                        break;
                    case 3:
                        socialScoreManager.SubtractFromScore(3);
                        break;
                }
                firstChoice = -1;
                Destroy(NPC);
                spawnNPCs.SpawnNewNPC();
                canvas.SetActive(false);
            }
        }
        public void ChooseChoice2()
        {
            if (firstChoice == -1)
            {
                mainText.text = textManager.currentTextFile.response[0];
                choice1.text = textManager.currentTextFile.PlayerResponsesStrings2[0];
                choice2.text = textManager.currentTextFile.PlayerResponsesStrings2[1];
                choice3.text = textManager.currentTextFile.PlayerResponsesStrings2[2];
                firstChoice = 2;
            }
            else
            {
                switch (firstChoice)
                {
                    case 1:
                        socialScoreManager.SubtractFromScore(2);
                        break;
                    case 2:
                        socialScoreManager.SubtractFromScore(1);
                        break;
                    case 3:
                        socialScoreManager.AddToScore(1);
                        break;
                }
                firstChoice = -1;
                Destroy(NPC);
                spawnNPCs.SpawnNewNPC();
                canvas.SetActive(false);
            }
        }
        public void ChooseChoice3()
        {
            if (firstChoice == -1)
            {
                mainText.text = textManager.currentTextFile.response[0];
                choice1.text = textManager.currentTextFile.PlayerResponsesStrings3[0];
                choice2.text = textManager.currentTextFile.PlayerResponsesStrings3[1];
                choice3.text = textManager.currentTextFile.PlayerResponsesStrings3[2];
                firstChoice = 3;
            }
            else
            {
                switch (firstChoice)
                {
                    case 1:
                        socialScoreManager.AddToScore(2);
                        break;
                    case 2:
                        socialScoreManager.AddToScore(3);
                        break;
                    case 3:
                        socialScoreManager.AddToScore(4);
                        break;
                }
                firstChoice = -1;
                Destroy(NPC);
                spawnNPCs.SpawnNewNPC();
                canvas.SetActive(false);
            }
        }
    }
}