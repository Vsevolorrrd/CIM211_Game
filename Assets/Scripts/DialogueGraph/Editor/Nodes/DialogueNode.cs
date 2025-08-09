using Subtegral.DialogueSystem.DataContainers;
using UnityEditor.Experimental.GraphView;

namespace Subtegral.DialogueSystem.Editor
{
    public class DialogueNode : Node
    {
        public string DialogueText;
        public string GUID;
        public bool EntyPoint = false;
        public string DisplayText;
        public DialogueNodeType NodeType = DialogueNodeType.Basic;

        // Misc
        public string Actor = "Unknown";
        public Emotion CharacterEmotion = Emotion.Default;

        // Conditions
        public BoolCondition BoolCondition;
        public StringCondition StringCondition;
        public RandomCondition RandomCondition;


        // Events
        public DialogueEvent Event;

        // End
        public EndAction EndAction;
    }
}