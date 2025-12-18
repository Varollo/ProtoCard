using SFB;
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
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
        var openPath = StandaloneFileBrowser.OpenFilePanel("Open Card", CardsFolderPath, "prc", false);

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
        var path = StandaloneFileBrowser.SaveFilePanel("Save Card", CardsFolderPath, "New Card", "prc");

        if (string.IsNullOrEmpty(path))
            return;

        _loadedCard.CopyData(cardBuilder.GetCardData());

        using (ZipArchive zip = ZipFile.Open(Path.Combine(Path.GetDirectoryName(path), ".temp_card.prc"), ZipArchiveMode.Create))
        {
            AddArtToSaveFile(zip);
            AddDataToSaveFile(zip);
        }

        CommitSaveFile(path);

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

    private void CommitSaveFile(string path)
    {
        string tempPath = Path.Combine(Path.GetDirectoryName(path), ".temp_card.prc");
        
        if (File.Exists(path))
            File.Delete(path);

        File.Move(tempPath, path);
    }

    private void AddDataToSaveFile(ZipArchive zip)
    {
        var cardJson = JsonUtility.ToJson(_loadedCard, true);
        var jsonEntry = zip.CreateEntry("card_data.json");

        using StreamWriter zipWriter = new(jsonEntry.Open());
        zipWriter.Write(cardJson);
    }

    private void AddArtToSaveFile(ZipArchive zip)
    {
        if (Path.GetExtension(_loadedCard.ArtPath).ToLower() == ".png")
        {
            zip.CreateEntryFromFile(_loadedCard.ArtPath, "card_art.png");
        }
        else
        {
            using ZipArchive sourceArchive = ZipFile.OpenRead(_loadedCard.ArtPath);

            ZipArchiveEntry imageEntry = sourceArchive.GetEntry("card_art.png");
            ZipArchiveEntry newEntry = zip.CreateEntry("card_art.png");

            using Stream sourceStream = imageEntry.Open();
            using Stream targetStream = newEntry.Open();

            sourceStream.CopyTo(targetStream);
        }
    }
}
