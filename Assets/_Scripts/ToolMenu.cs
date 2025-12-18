using UnityEngine;
using UnityEngine.EventSystems;

public class ToolMenu : MonoBehaviour
{
    [SerializeField] private GameObject content;

    public void Open()
    {
        content.SetActive(true);
    }

    public void Close()
    {
        content.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
