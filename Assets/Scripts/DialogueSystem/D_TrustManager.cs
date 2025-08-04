using UnityEngine;

namespace Subtegral.DialogueSystem.Runtime
{
    public class D_TrustManager : MonoBehaviour
    {
        [SerializeField] int startingTrust = 0;
        [SerializeField] int trust;
        public int GetTrust() { return trust; }

        private void Start()
        {
            trust = startingTrust;
        }
        public void UpdateTrust(int modifier)
        {
            trust += modifier;
        }
    }
}