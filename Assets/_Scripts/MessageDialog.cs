using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogContent;
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text messageTxt;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private Button backgroundCancelButton;
    [SerializeField] private Button okButton;

    public void Show(string title, string message, bool cancelable, Action onConfirm)
    {
        titleTxt.text = title;
        messageTxt.text = message;

        cancelButton.SetActive(cancelable);
        closeButton.SetActive(cancelable);
        backgroundCancelButton.interactable = cancelable;
        
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(onConfirm.Invoke);
        okButton.onClick.AddListener(Hide);

        dialogContent.SetActive(true);
    }

    public void Hide()
    {
        dialogContent.SetActive(false);
    }
}
