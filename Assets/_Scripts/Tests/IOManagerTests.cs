using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class IOManagerTests
{
    private static readonly string _testDataPath = Path.Combine(Application.dataPath,"_Scripts", "Tests", "TestData", "test_data.prc");
    private static readonly string _testImgPath = Path.Combine(Application.dataPath,"_Scripts", "Tests", "TestData", "test_img.png");

    private readonly IOManager _ioManager;

    public IOManagerTests()
    {
        GameObject go = new();
        _ioManager = go.AddComponent<IOManager>();
    }

    [UnityTest]
    public IEnumerator IOManager_OpenCard_DataLoaded()
    {
        _ioManager.OpenCard(_testDataPath);

        Assert.AreNotEqual(new CardData(), _ioManager.GetLoadedCard());

        yield break;
    }

    [UnityTest]
    public IEnumerator IOManager_OpenCard_ImageLoaded()
    {
        _ioManager.OpenCard(_testDataPath);

        Assert.IsNotEmpty(_ioManager.GetLoadedCard().ArtPath);

        yield break;
    }

    [UnityTest]
    public IEnumerator IOManager_OpenCard_ThenNewCard_DataCleared()
    {
        _ioManager.OpenCard(_testDataPath);

        var originalCard = _ioManager.GetLoadedCard();

        _ioManager.NewCard();

        var newCard = _ioManager.GetLoadedCard();

        Assert.AreNotEqual(originalCard, newCard);

        yield break;
    }

    [UnityTest]
    public IEnumerator IOManager_SaveCard_ThenOpenCard_DataMatches()
    {
        _ioManager.NewCard();

        var originalCard = _ioManager.GetLoadedCard();

        _ioManager.SaveCard(_testDataPath);
        yield return null;
        _ioManager.OpenCard(_testDataPath);

        var loadedCard = _ioManager.GetLoadedCard();

        // ArtPath is different since it points to the file location, so we compare other fields
        Assert.True(originalCard.name == loadedCard.name &&
                    originalCard.description == loadedCard.description &&
                    originalCard.footer == loadedCard.footer &&
                    originalCard.value_tl == loadedCard.value_tl &&
                    originalCard.value_tr == loadedCard.value_tr &&
                    originalCard.value_bl == loadedCard.value_bl &&
                    originalCard.value_br == loadedCard.value_br);

        yield break;
    }

    [UnityTest]
    public IEnumerator IOManager_LoadCardImage_ImagePathChanges()
    {
        _ioManager.NewCard();

        var originalCard = _ioManager.GetLoadedCard();

        _ioManager.LoadCardImage(_testImgPath);

        var updatedCard = _ioManager.GetLoadedCard();

        Assert.AreNotEqual(originalCard.ArtPath, updatedCard.ArtPath);

        yield break;
    }
}
