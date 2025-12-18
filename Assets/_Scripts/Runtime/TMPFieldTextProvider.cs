using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class TMPFieldTextProvider : UITextProvider
{
    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public override string GetText()
    {
        return inputField.text;
    }

    public override void SetText(string text)
    {
        inputField.text = text;
    }
}
