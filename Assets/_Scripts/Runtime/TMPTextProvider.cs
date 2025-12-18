using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextProvider : UITextProvider
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public override string GetText()
    {
        return text.text;
    }

    public override void SetText(string text)
    {
        this.text.text = text;
    }
}
