using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    protected Button pauseButton;

    [SerializeField]
    protected GameObject pauseMenuUI;

    [SerializeField]
    protected GameObject gameUI;

    [SerializeField]
    protected Camera menuCamera;

    [SerializeField]
    protected Camera mainCamera;

    protected virtual void Start() {}
}
