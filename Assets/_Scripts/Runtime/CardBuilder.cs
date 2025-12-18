using System;
using UnityEngine;
using UnityEngine.UI;

public class CardBuilder : MonoBehaviour
{
    [SerializeField] private Image cardArtImg;
    [Space]
    [SerializeField] private UITextProvider cardNameTxt;
    [SerializeField] private UITextProvider footerTxt;
    [Space]
    [SerializeField] private GameObject descriptionContainer;
    [SerializeField] private UITextProvider descriptionTxt;
    [Space]
    [SerializeField] private UITextProvider valueTLTxt;
    [SerializeField] private UITextProvider valueTRTxt;
    [SerializeField] private UITextProvider valueBLTxt;
    [SerializeField] private UITextProvider valueBRTxt;

    public void BuildCard(CardData cardData)
    {
        SetField(cardNameTxt, cardData.name);
        SetField(footerTxt, cardData.footer);

        SetField(valueTLTxt, cardData.value_tl.ToString("00"));
        SetField(valueTRTxt, cardData.value_tr.ToString("00"));
        SetField(valueBRTxt, cardData.value_br.ToString("00"));
        SetField(valueBLTxt, cardData.value_bl.ToString("00"));

        SetDescription(cardData.description);
        SetArt(cardData);
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description))
            descriptionContainer.SetActive(false);
        else
            descriptionTxt.SetText(description);
    }

    private void SetArt(CardData cardData)
    {
        var sprite = CardSpriteProvider.GetSpriteForCard(cardData);

        if (sprite)
        { 
            cardArtImg.sprite = sprite;
            cardArtImg.gameObject.SetActive(true);
        }
        else
        {
            cardArtImg.gameObject.SetActive(false);
        }
    }

    private void SetField(UITextProvider textProvider, string content)
    {
        if (string.IsNullOrEmpty(content))
            textProvider.gameObject.SetActive(false);
        else
            textProvider.SetText(content);
    }

    public CardData GetCardData()
    {
        return new CardData
        {
            name = cardNameTxt.GetText(),
            footer = footerTxt.GetText(),
            description = descriptionTxt.GetText(),
            value_tl = int.Parse(valueTLTxt.GetText()),
            value_tr = int.Parse(valueTRTxt.GetText()),
            value_bl = int.Parse(valueBLTxt.GetText()),
            value_br = int.Parse(valueBRTxt.GetText()),
        };
    }

    internal void ReloadCard()
    {
        BuildCard(GetCardData());
    }
}
