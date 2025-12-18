using SFB;
using System.IO;
using UnityEngine;

public class IOManager : MonoBehaviour
{
    public static readonly string CardsFolderPath = Path.Combine(Application.dataPath, "Cards");

    [SerializeField] private CardBuilder cardBuilder;
    [SerializeField] private MessageDialog messageDialog;

    private CardData _loadedCard;

    private void Start()
    {
        CreateNewCard();
    }

    private void CreateNewCard()
    {
        _loadedCard = new()
        {
            name = "CARD NAME",
            description = "Card description goes here. Please remember to fill it in!\r\n\r\nYou can also have multiple lines and line skips.",
            footer = "Card Footer",
            value_tl = 0,
            value_tr = 0,
            value_bl = 0,
            value_br = 0,
        };

        cardBuilder.BuildCard(_loadedCard);
    }

    public void NewCard()
    {
        messageDialog.Show("Create New Card", "Are you sure you want to create a new card? Any unsaved changes will be lost.", cancelable: true, CreateNewCard);
    }

    public void OpenCard()
    {
        var openPath = StandaloneFileBrowser.OpenFilePanel("Open Card", CardsFolderPath, "json", false);

        if (openPath.Length == 0 || string.IsNullOrEmpty(openPath[0]))
            return;

        CardData? cardData = CardData.LoadCard(openPath[0]);

        if (cardData.HasValue)
            _loadedCard = cardData.Value;
        else
            Debug.LogError("Failed to load card data from file: " + openPath[0]);

        cardBuilder.BuildCard(_loadedCard);
    }

    public void SaveCard()
    {
        var path = StandaloneFileBrowser.SaveFilePanel("Save Card", CardsFolderPath, "New Card", "json");

        if (string.IsNullOrEmpty(path))
            return;

        _loadedCard.CopyData(cardBuilder.GetCardData());

        var cardJson = JsonUtility.ToJson(_loadedCard, true);

        using FileStream fileStream = new(path, FileMode.Create);
        using StreamWriter writer = new(fileStream);
        writer.Write(cardJson);

        string artPath = Path.ChangeExtension(path, "png");

        if (_loadedCard.ArtPath != artPath)
        {
            try
            {
                File.Copy(_loadedCard.ArtPath, artPath, true);
            }
            catch (IOException iox)
            {
                Debug.LogError("Failed to copy art file: " + iox.Message);
                return;
            }
        }

        _loadedCard.ArtPath = artPath;

        cardBuilder.BuildCard(_loadedCard);
    }

    public void LoadCardImage()
    {
        var filePath = StandaloneFileBrowser.OpenFilePanel("Load Card Image", CardsFolderPath, "png", false);
        
        if (filePath.Length == 0 || string.IsNullOrEmpty(filePath[0]))
            return;

        _loadedCard.CopyData(cardBuilder.GetCardData());
        _loadedCard.ArtPath = filePath[0];

        cardBuilder.BuildCard(_loadedCard);
    }
}
