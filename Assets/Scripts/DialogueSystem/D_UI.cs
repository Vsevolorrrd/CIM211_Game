using Subtegral.DialogueSystem.DataContainers;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using TMPro;
using Characters;

namespace Subtegral.DialogueSystem.Runtime
{
    public class D_UI : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] TextMeshProUGUI dialogueText;
        [SerializeField] GameObject UI;

        [Header("Choice")]
        [SerializeField] Transform buttonContainer;
        [SerializeField] Button choicePrefab;


        public void OpenDialogueUI()
        {
            UI.gameObject.SetActive(true);
        }

        public void CloseDialogueUI()
        {
            dialogueText.text = "";
            ClearButtons();
            UI.gameObject.SetActive(false);
        }

        public void CreateText(DialogueNodeData nodeData)
        {
            if (nodeData.Actor == "narrator_id")
            {
                dialogueText.text = nodeData.DialogueText;
            }
            else
            {
                Sprite characterSprite = CharacterDatabase.GetCharacterByID(nodeData.Actor).Visuals;
                CharacterManager.Instance.SetCharacterVisuals(characterSprite);
                string text = $"<b>{nodeData.Actor}</b>\n{nodeData.DialogueText}";
                dialogueText.text = text;
            }
        }

        public void ClearText()
        {
            dialogueText.text = "";
        }

        public void CreateButtons(DialogueContainer container, DialogueNodeData nodeData, D_Manager manager)
        {
            ClearButtons();
            var links = container.NodeLinks.Where(x => x.BaseNodeGUID == nodeData.NodeGUID).ToList();

            foreach (var choice in links)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.DisplayText;
                button.onClick.AddListener(() => manager.ProceedToNarrative(choice.TargetNodeGUID));
            }
        }

        public void ClearButtons()
        {
            foreach (Transform child in buttonContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}