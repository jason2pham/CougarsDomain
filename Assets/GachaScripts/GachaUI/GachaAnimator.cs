using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GachaAnimator : MonoBehaviour
{
    public Button summonButton;
    public GameObject summonPanel;
    public Text resultText;
    public Image itemImage;

    public Sprite commonSprite;
    public Sprite rareSprite;
    public Sprite epicSprite;

    // Reference to the SummonPool from GachaSystem.cs
    public SummonPool summonPool;

    private void Start()
    {
        // Ensure summonButton triggers StartSummonAnimation
        summonButton.onClick.AddListener(StartSummonAnimation);

        // Initialize the summon pool
        if (summonPool == null)
        {
            summonPool = new SummonPool();
            InitializeSummonPool();
        }
    }

    private void InitializeSummonPool()
    {
        // Create some example summonable items
        var swordStats = new Dictionary<string, int> { { "Strength", 10 } };
        Weapon sword = new Weapon("Flame Sword", "Epic", swordStats, 30, "Fire");

        var bowStats = new Dictionary<string, int> { { "Strength", 8 } };
        Weapon bow = new Weapon("Wind Bow", "Rare", bowStats, 20, "Wind");

        summonPool.AddSummonableItem(sword);
        summonPool.AddSummonableItem(bow);

        // Set rarity weights
        summonPool.SetRarityWeight("Common", 50f);
        summonPool.SetRarityWeight("Rare", 30f);
        summonPool.SetRarityWeight("Epic", 15f);
    }

    void StartSummonAnimation()
    {
        summonButton.interactable = false;
        StartCoroutine(PlaySummon());
    }

    IEnumerator PlaySummon()
    {
        // Show panel
        summonPanel.SetActive(true);
        resultText.text = "Summoning...";

        // Simulate summon animation delay
        yield return new WaitForSeconds(2f);

        // Pull the item
        SummonableItem item = summonPool.GenerateReward();

        // Show result
        resultText.text = $"You summoned: {item.Name} ({item.Rarity})";

        // Show rarity-based image
        switch (item.Rarity)
        {
            case "Common":
                itemImage.sprite = commonSprite;
                break;
            case "Rare":
                itemImage.sprite = rareSprite;
                break;
            case "Epic":
                itemImage.sprite = epicSprite;
                break;
            default:
                itemImage.sprite = null;
                break;
        }

        // Show result for a while
        yield return new WaitForSeconds(3f);

        // Hide panel and re-enable button
        summonPanel.SetActive(false);
        summonButton.interactable = true;
    }
}
