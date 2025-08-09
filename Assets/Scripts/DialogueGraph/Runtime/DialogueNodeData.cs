using UnityEngine;
using System;

namespace Subtegral.DialogueSystem.DataContainers
{
    [Serializable] // stores the data
    public class DialogueNodeData
    {
        public string NodeGUID;
        public string DialogueText;
        public Vector2 Position;

        public DialogueNodeType NodeType;
        public Emotion CharacterEmotion;
        public string Actor;
        public string DisplayText;

        // String Condition data
        public string StringConditionKey;

        // Bool Condition data
        public string BoolConditionKey;
        public bool BoolConditionExpectedValue;

        // Random Condition data
        public int RandomConditionValue;

        // Event data
        public DialogueEventType EventType;
        public string EventName;
        public float EventValue;

        // End data
        public EndAction EndAction;
    }
}