using DG.Tweening;
using UnityEngine;

public class ScaleOnHover : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 1.1f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    public void OnMouseOver()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale * scaleFactor, 10 * Time.deltaTime);
    }
    public void OnMouseOut()
    {
        transform.DOScale(originalScale, .1f).SetEase(Ease.InOutQuad);
    }
}
