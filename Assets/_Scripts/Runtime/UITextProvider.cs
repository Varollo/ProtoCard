using UnityEngine;

public abstract class UITextProvider : MonoBehaviour
{
    public abstract string GetText();
    public abstract void SetText(string text);
}
