using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem
{
    public class DialogueHandler : MonoBehaviour
    {

        public static DialogueHandler textBox;
        private DialogueNode currentNode = null;

        private int currentIndex = 0;
        public string textOnScreen = "";
        private bool finishedPrintingText = false;

        public int printSpeed = 3;

        public GameObject panel;
        public Text screenText;
        public GameObject choiceText;

        public delegate void DialogueInteraction();

        public static event DialogueInteraction OnDialogueStart;
        public static event DialogueInteraction OnDialogueEnd;

        private List<NpcController> nearbyNPCs = new List<NpcController>();



        private void Awake()
        {
            if (textBox == null)
            {
                textBox = this;
            }
            else
            {
                Destroy(this);
            }

            currentNode = null;
        }


        // Update is called once per frame
        void Update()
        {
            
            //called when node is null and button is pressed
            if (Input.GetKeyDown(KeyCode.G) && currentNode == null)
            {
                NpcController controller = GetNearestNPC();

                if (controller != null)
                {
                    controller.Interact();

                    if (currentNode != null)
                    {
                        currentNode.Start();
                    }
                }
            }


            //node is not empty
            if (currentNode != null)
            {
                Debug.Log("Node Not Null");
                if (!finishedPrintingText)
                {
                    finishedPrintingText = currentNode.Update();

                    if (Input.GetMouseButtonDown(0))
                    {
                        finishedPrintingText = currentNode.SkipText() || finishedPrintingText;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        finishedPrintingText = false;
                        currentNode.Next();
                        if (currentNode != null)
                        {
                            currentNode.Start();
                        }
                    }
                }

                screenText.text = textOnScreen;
            }


        }


        private void OnTriggerEnter(Collider other)
        {
            NpcController target = other.transform.GetComponent<NpcController>();

            if (target != null)
            {
                nearbyNPCs.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            NpcController target = other.transform.GetComponent<NpcController>();

            if (target != null)
            {
                nearbyNPCs.Remove(target);
            }
        }



        public NpcController GetNearestNPC()
        {
            float distance = 9999;
            NpcController nearest = null;

            foreach (NpcController npc in nearbyNPCs)
            {
                float tempDistance = (npc.transform.position - transform.position).magnitude;

                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    nearest = npc;
                }
            }

            return nearest;
        }


        //Sets the dialogue (used by other classes)
        public void SetNode(DialogueNode newNode)
        {
            currentNode = newNode;
        }





        //updates the string over time
        public bool UpdateText(string newText)
        {
            int newLength = Mathf.Min(textOnScreen.Length + printSpeed, newText.Length);
            textOnScreen = newText.Substring(0, newLength);

            Debug.Log(screenText.preferredHeight);

            return textOnScreen.Length == newText.Length;
        }


        public void ClearText()
        {
            textOnScreen = "";
        }

        public void SetText(string newText)
        {
            textOnScreen = newText;
        }

        public void StartInteraction(DialogueNode newNode)
        {
            panel.SetActive(true);
            SetNode(newNode);
            OnDialogueStart();
        }

        public void EndInteraction()
        {
            panel.SetActive(false);
            SetNode(null);
            OnDialogueEnd();
        }


        public void AddChoices(List<DialogueNode> choices)
        {

        }



    }
}