using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DetectMouseOver : MonoBehaviour
{
    public UnityEvent OnMouseOver;
    public UnityEvent OnMouseOut;
    public UnityEvent OnMouseEnter;
    public UnityEvent OnMouseExit;

    private Canvas _canvas;

    public bool IsMouseOver { get; private set; }

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = transform.position.z + (Screen.width + Screen.height) * .5f; // Arbitrary distance from the camera

        Rect rect = new()
        {
            size = ((RectTransform)transform).rect.size * _canvas.scaleFactor,
            center = transform.position,
        };

        if (rect.Contains((Vector2)mousePosition))
        {
            OnMouseOver?.Invoke();

            if (!IsMouseOver)
                OnMouseExit?.Invoke();

            IsMouseOver = true;
        }
        else
        {
            OnMouseOut?.Invoke();

            if (IsMouseOver)
                OnMouseEnter?.Invoke();

            IsMouseOver = false;
        }
    }
}
