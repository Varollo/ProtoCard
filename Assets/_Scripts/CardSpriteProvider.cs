using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CardSpriteProvider
{
    public static Sprite GetSpriteForCard(CardData cardData)
    {
        if (string.IsNullOrEmpty(cardData.ArtPath))
            return null;

        var path = cardData.ArtPath;

        return LoadSprite(path);
    }

    private static Sprite LoadSprite(string path)
    {
        if (!File.Exists(path))
            return null;

        byte[] fileData;

        try
        {
            fileData = File.ReadAllBytes(path);
        }
        catch (IOException e)
        {
            Debug.LogError("Failed to read card art file at path: " + path + "\n" + e.Message);
            return null;
        }

        Texture2D tex = new(2, 2);

        if (tex.LoadImage(fileData))
        {
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("Failed to load image data from file at path: " + path);
            return null;
        }
    }
}