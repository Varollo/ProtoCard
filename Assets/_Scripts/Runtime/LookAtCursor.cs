using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtCursor : MonoBehaviour
{
    [SerializeField] private float zDistanceScale = .5f;

    public void OnMouseOver()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = transform.position.z + (Screen.width + Screen.height) * zDistanceScale; // Arbitrary distance from the camera based on screen size

        transform.rotation = Quaternion.LookRotation(mousePosition - transform.position);
    }

    public void OnMouseOut()
    {
        transform.DORotate(Vector3.zero, .1f).SetEase(Ease.InOutQuad);
    }
}
