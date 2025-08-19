using System;
using System.Linq;
using UnityEngine;

namespace Characters
{
    public class CharacterFader : MonoBehaviour
    {
        public SpriteRenderer character;
        public bool isFading = false;
        [SerializeField] float targetAlpha;
        [SerializeField] float fadeDuration = 1f;

        private void OnEnable()
        {
            character = GetComponent<SpriteRenderer>();
            fadeDuration = 1f;
        }

        private void Update()
        {
            if (isFading)
            {
                float alpha = Mathf.Lerp(character.color.a, targetAlpha, Time.deltaTime * 3f * fadeDuration);
                character.color = new Color(character.color.r, character.color.g, character.color.b, alpha);

                if (targetAlpha == 0f)
                {
                    if (character.color.a == 0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        public void FadeIn()
        {
            isFading = true;
            targetAlpha = 1.0f;
            character.color = new Color(character.color.r, character.color.g, character.color.b, 0f);
        }

        public void FadeOut()
        {
            isFading = true;
            targetAlpha = 0f;
        }
    }
}