using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtCursor : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = transform.position.z + Screen.width + Screen.height;
        transform.rotation = Quaternion.LookRotation(mousePosition - transform.position);
    }
}
