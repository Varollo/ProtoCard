using System.IO;
using UnityEngine;

public class TestCardBuilder : MonoBehaviour
{
    [SerializeField] private CardBuilder cardBuilder;
    [SerializeField] private TextAsset[] cardJsons;

    private void Start()
    {
        CardData? cardData = CardData.LoadCard(Path.Combine(IOManager.CardsFolderPath, cardJsons[Random.Range(0, cardJsons.Length)].name));

        if (cardData.HasValue)
            cardBuilder.BuildCard(cardData.Value);
        else
            Debug.LogError("Failed to load card data.");
    }
}