using System.Collections.Generic;
using UnityEngine;

namespace Subtegral.DialogueSystem.Runtime
{
    public class D_conditionManager : MonoBehaviour
    {
        private List<string> stringConditions = new List<string>(24);
        private Dictionary<string, bool> boolConditions = new Dictionary<string, bool>(10);

        // strings
        public bool StringCondition(string condition)
        {
            return stringConditions.Contains(condition);
        }
        public void AddStringCondition(string condition)
        {
            if (!stringConditions.Contains(condition))
                stringConditions.Add(condition);
        }

        // bools
        public bool BoolCondition(string key, bool expected)
        {
            return boolConditions.TryGetValue(key, out var value) && value == expected;
        }

        public void SetBoolCondition(string key, bool value)
        {
            boolConditions[key] = value;
        }

        // Random
        public bool RandomCondition(int difficulty)
        {
            int R = Random.Range(0, 101);
            return R <= difficulty;
        }
    }
}