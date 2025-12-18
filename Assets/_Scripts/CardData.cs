using System;
using System.IO;
using UnityEngine;

public struct CardData
{
    public string name;
    public string description;
    public string footer;
    public int value_tl;
    public int value_tr;
    public int value_bl;
    public int value_br;

    public string ArtPath { get; set; }

    public void CopyData(CardData data)
    {
        name = data.name;
        description = data.description;
        footer = data.footer;
        value_tl = data.value_tl;
        value_tr = data.value_tr;
        value_bl = data.value_bl;
        value_br = data.value_br;
    }

    public static CardData? LoadCard(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError($"Card file not found: {path}");
            return null;
        }

        try
        {
            string jsonContent = File.ReadAllText(path);
            CardData cardData = JsonUtility.FromJson<CardData>(jsonContent);

            cardData.ArtPath = Path.ChangeExtension(path, "png");

            return cardData;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load card data from file: {path}\n{e.Message}");
            return null;
        }
        
    }
}
