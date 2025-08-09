using UnityEngine;

public class RecipeNote : MonoBehaviour
{
    public RectTransform notebookPanel;
    public Vector2 hiddenPosition;
    public Vector2 visiblePosition;
    public float slideSpeed = 10f;

    private bool isVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleNotebook();
        }

        Vector2 targetPos = isVisible ? visiblePosition : hiddenPosition;
        notebookPanel.anchoredPosition = Vector2.Lerp(notebookPanel.anchoredPosition, targetPos, Time.deltaTime * slideSpeed);
    }

    public void ToggleNotebook()
    {
        isVisible = !isVisible;
    }
}