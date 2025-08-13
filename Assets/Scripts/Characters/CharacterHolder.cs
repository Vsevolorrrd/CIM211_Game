using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    public Character character;
    [SerializeField] GameObject defaultFace;
    [SerializeField] GameObject angryFace;
    [SerializeField] GameObject sadFace;
    [SerializeField] GameObject happyFace;

    public void SwitchEmotions(string emotion)
    {
        SetAllFacesOff();

        switch (emotion)
        {
            case "angry":
                if (angryFace != null)
                angryFace.SetActive(true);
                else defaultFace.SetActive(true);
                break;
            case "sad":
                if (sadFace != null)
                sadFace.SetActive(true);
                else defaultFace.SetActive(true);
                break;
            case "happy":
                if (happyFace != null)
                happyFace.SetActive(true);
                else defaultFace.SetActive(true);
                break;
            default:
                defaultFace.SetActive(true);
                break;
        }
    }
    private void SetAllFacesOff()
    {
        defaultFace.SetActive(false);
        angryFace.SetActive(false);
        sadFace.SetActive(false);
        happyFace.SetActive(false);
    }
}
