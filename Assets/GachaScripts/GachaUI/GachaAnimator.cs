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

    public Transform modelSpawnPoint;
    private GameObject currentModelInstance;

    public SummonPool summonPool;

    public GameObject flameSwordPrefab;
    public GameObject windBowPrefab;

    private void Start()
    {
        summonButton.onClick.AddListener(StartSummonAnimation);

        if (summonPool == null)
        {
            summonPool = new SummonPool();
            InitializeSummonPool();
        }
    }

    private void InitializeSummonPool()
    {
        var swordStats = new Dictionary<string, int> { { "Strength", 10 } };
        var sword = new Weapon("Flame Sword", "Epic", swordStats, 30, "Fire")
        {
            ModelPrefab = flameSwordPrefab
        };

        var bowStats = new Dictionary<string, int> { { "Strength", 8 } };
        var bow = new Weapon("Wind Bow", "Rare", bowStats, 20, "Wind")
        {
            ModelPrefab = windBowPrefab
        };

        summonPool.AddSummonableItem(sword);
        summonPool.AddSummonableItem(bow);

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
        summonPanel.SetActive(true);
        resultText.text = "Summoning...";

        yield return new WaitForSeconds(2f);

        SummonableItem item = summonPool.GenerateReward();
        resultText.text = $"You summoned: {item.Name} ({item.Rarity})";

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

        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
        }

        if (item.ModelPrefab != null)
        {
            currentModelInstance = Instantiate(item.ModelPrefab, modelSpawnPoint.position, Quaternion.identity, modelSpawnPoint);
        }

        yield return new WaitForSeconds(3f);

        summonPanel.SetActive(false);
        summonButton.interactable = true;
    }
}
