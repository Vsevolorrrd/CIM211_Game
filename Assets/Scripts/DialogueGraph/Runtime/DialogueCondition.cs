using System;

namespace Subtegral.DialogueSystem.DataContainers
{
    [Serializable]
    public abstract class DialogueCondition
    {
        public string Key; // what to check
    }

    [Serializable]
    public class StringCondition : DialogueCondition
    {
        public string Value;
    }

    [Serializable]
    public class BoolCondition : DialogueCondition
    {
        public bool ExpectedValue;
    }

    [Serializable]
    public class RandomCondition : DialogueCondition
    {
        public int Value;
    }
}