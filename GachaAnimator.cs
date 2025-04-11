using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaAnimator : MonoBehaviour
{
    public Button summonButton;
    public GameObject summonPanel;
    public Text resultText;
    public Image itemImage;

    public Sprite commonSprite;
    public Sprite rareSprite;
    public Sprite epicSprite;

    public SummonPool summonPool;

    private void Start()
    {
        summonButton.onClick.AddListener(StartSummonAnimation);
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

        //  pull the item
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
