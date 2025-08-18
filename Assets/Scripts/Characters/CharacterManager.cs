using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance { get; private set; }

        private Dictionary<string, Character> characters = new();

        [SerializeField] Transform spawn;
        private GameObject currentCharacter;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadAllCharacters();
        }

        void LoadAllCharacters()
        {
            Character[] loadedCharacters = Resources.LoadAll<Character>("Characters");
            foreach (var character in loadedCharacters)
            {
                if (!characters.ContainsKey(character.CharacterID))
                characters.Add(character.CharacterID, character);
            }
        }
        public Character GetCharacter(string id)
        {
            if (characters.TryGetValue(id, out var character))
            return character;

            Debug.LogWarning($"Character with ID '{id}' not found.");
            return null;
        }
        public void SetCharacterVisuals(Character character, string emotion = "default")
        {
            if (currentCharacter.GetComponent<CharacterHolder>().character != character)
            {
                CharacterHolder characterHolder = currentCharacter.GetComponent<CharacterHolder>();
                characterHolder.SetAllFacesOff();
                characterHolder.characterFader.FadeOut();
                
                var spawnedChar = Instantiate(character.characterPrefab, spawn.position, Quaternion.identity);
                var charHolder = spawnedChar.GetComponent<CharacterHolder>();
                charHolder.SwitchEmotions(emotion);
                charHolder.character = character;
                currentCharacter = spawnedChar;

                return;
            }

            currentCharacter.GetComponent<CharacterHolder>().SwitchEmotions(emotion);

        }
        public GameObject SpawnCharacter(Character character, string emotion = "default")
        {
            if (currentCharacter != null)
            Destroy(currentCharacter);

            var spawnedChar = Instantiate(character.characterPrefab, spawn.position, Quaternion.identity);
            var charHolder = spawnedChar.GetComponent<CharacterHolder>();
            charHolder.SwitchEmotions(emotion);
            charHolder.character = character;
            currentCharacter = spawnedChar;
            charHolder.characterFader.FadeIn();

            return spawnedChar;
        }
        public void RemoveCurrentCharacter()
        {
            CharacterHolder characterHolder = currentCharacter.GetComponent<CharacterHolder>();
            characterHolder.SetAllFacesOff();
            characterHolder.characterFader.FadeOut();
        }
    }
}