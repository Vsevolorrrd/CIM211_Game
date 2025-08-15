using Subtegral.DialogueSystem.DataContainers;
using System.Linq;
using System.Collections;
using UnityEngine;
using Characters;

namespace Subtegral.DialogueSystem.Runtime
{
    public class D_Manager : Singleton<D_Manager>
    {
        [SerializeField] DialogueContainer dialogueContainer;

        private DialogueNodeData savedDialogueNodeData;
        private bool awatingImput = false;

        // Dialogue managers
        private D_conditionManager conditionManager;
        private D_EventManager eventManager;
        private D_TrustManager trustManager;
        private D_UI UIManager;

        void Start()
        {
            conditionManager = GetComponent<D_conditionManager>();
            eventManager = GetComponent<D_EventManager>();
            trustManager = GetComponent<D_TrustManager>();
            UIManager = GetComponent<D_UI>();
        }

        public void StartDialogue(DialogueContainer dialogue)
        {
            dialogueContainer = dialogue;
            UIManager.OpenDialogueUI();
            var narrativeData = dialogueContainer.NodeLinks.First(); //Entrypoint node
            ProceedToNarrative(narrativeData.TargetNodeGUID);
        }

        public void ProceedToNarrative(string narrativeDataGUID)
        {
            var nodeData = dialogueContainer.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID);
            UIManager.ClearButtons();
            UIManager.ClearText();

            switch (nodeData.NodeType)
            {
                case DialogueNodeType.Basic:
                    BasicNode(nodeData);
                    break;

                case DialogueNodeType.Choice:
                    ChoiceNode(nodeData);
                    break;

                case DialogueNodeType.Event:
                    EventNode(nodeData);
                    break;

                case DialogueNodeType.StringCondition:
                    StringConditionNode(nodeData);
                    break;

                case DialogueNodeType.BoolCondition:
                    BoolConditionNode(nodeData);
                    break;

                case DialogueNodeType.RandomCondition:
                    RandomConditionNode(nodeData);
                    break;

                case DialogueNodeType.End:
                    EndNode(nodeData);
                    break;

                default:
                    Debug.LogWarning($"Invalid node type: {nodeData.NodeType}");
                    break;
            }
        }

        private void BasicNode(DialogueNodeData nodeData)
        {
            UIManager.CreateText(nodeData);
            Character character = CharacterDatabase.GetCharacterByID(nodeData.Actor);
            CharacterManager.Instance.SetCharacterVisuals(character, nodeData.CharacterEmotion.ToString().ToLowerInvariant());

            savedDialogueNodeData = nodeData;
            awatingImput = true;
        }

        private void ChoiceNode(DialogueNodeData nodeData)
        {
            UIManager.CreateButtons(dialogueContainer, nodeData, this);
        }
        private void EventNode(DialogueNodeData nodeData)
        {
            var nextLink = dialogueContainer.NodeLinks.FirstOrDefault(x => x.BaseNodeGUID == nodeData.NodeGUID);
            if (nextLink == null) return;

            if (nodeData.EventType == DialogueEventType.Delay) // for delay
            {
                StartCoroutine(Delay(nextLink.TargetNodeGUID, nodeData.EventValue));
                return;
            }

            ProceedToNarrative(nextLink.TargetNodeGUID);
            eventManager.DialogueEvent(nodeData, conditionManager, trustManager);
        }
        private void StringConditionNode(DialogueNodeData nodeData)
        {
            bool result = false;

            result = conditionManager.StringCondition(nodeData.StringConditionKey);

            var nextLink = dialogueContainer.NodeLinks.FirstOrDefault(x =>
            x.BaseNodeGUID == nodeData.NodeGUID &&
            x.PortName == (result ? "True" : "False"));

            if (nextLink != null)
                ProceedToNarrative(nextLink.TargetNodeGUID);
        }
        private void BoolConditionNode(DialogueNodeData nodeData)
        {
            bool result = conditionManager.BoolCondition(nodeData.BoolConditionKey, nodeData.BoolConditionExpectedValue);

            var nextLink = dialogueContainer.NodeLinks.FirstOrDefault(x =>
            x.BaseNodeGUID == nodeData.NodeGUID &&
            x.PortName == (result ? "True" : "False"));

            if (nextLink != null)
                ProceedToNarrative(nextLink.TargetNodeGUID);
        }
        private void RandomConditionNode(DialogueNodeData nodeData)
        {
            bool result = conditionManager.RandomCondition(nodeData.RandomConditionValue);

            var nextLink = dialogueContainer.NodeLinks.FirstOrDefault(x =>
            x.BaseNodeGUID == nodeData.NodeGUID &&
            x.PortName == (result ? "True" : "False"));

            if (nextLink != null)
                ProceedToNarrative(nextLink.TargetNodeGUID);
        }

        private void EndNode(DialogueNodeData nodeData)
        {
            Debug.Log("Dialogue has ended.");

            UIManager.CloseDialogueUI();
            //if (nodeData.EndAction == EndAction.LoadScene)
            //SceneLoader.Instance.LoadScene(nodeData.DialogueText);
            if (nodeData.EndAction == EndAction.StartDialogue)
            {
                var dialogueFiles = Resources.LoadAll<DialogueContainer>("Dialogues");
                var dialogueNames = dialogueFiles.Select(file => file.name).ToList();

                foreach (var dialogueName in dialogueNames)
                {
                    if (dialogueName == nodeData.DialogueText)
                    {
                        StartDialogue(Resources.Load<DialogueContainer>($"Dialogues/{dialogueName}"));
                        break;
                    }
                }
            }
        }

        private void Update()
        {
            if (!awatingImput || savedDialogueNodeData == null) return;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                var nextLink = dialogueContainer.NodeLinks.FirstOrDefault(x => x.BaseNodeGUID == savedDialogueNodeData.NodeGUID);
                if (nextLink != null)
                {
                    awatingImput = false;
                    ProceedToNarrative(nextLink.TargetNodeGUID);
                }
                else
                {
                    Debug.LogWarning("No next link found");
                }
            }
        }
        private IEnumerator Delay(string targetNodeGUID, float time)
        {
            float timer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            ProceedToNarrative(targetNodeGUID);
        }
    }
}
