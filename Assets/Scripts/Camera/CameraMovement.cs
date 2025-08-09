using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float border = 10f;

    [Header("Camera Movement Limits")]
    [SerializeField] float minX = -20f;
    [SerializeField] float maxX = 20f;
    void Update()
    {
        UpdateCamPos();
    }
    private void UpdateCamPos()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - border)
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= border)
        {
            pos.x -= speed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}