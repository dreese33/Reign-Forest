using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public Vector3 screenSize;

    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        screenSize = Camera.main.ViewportToWorldPoint(Vector3.up + Vector3.right);

        float sizeX = screenSize.x / rt.rect.width;
        float sizeY = screenSize.y / rt.rect.height;
        rt.localScale = new Vector3(sizeX * 0.25f, sizeY, 1);
    }
}
