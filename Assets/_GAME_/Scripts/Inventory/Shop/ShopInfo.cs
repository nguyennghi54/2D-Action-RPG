using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescText;

    [Header("Stats field")] public TMP_Text[] statTexts;
    private RectTransform infoPanelRect;

    void Awake()
    {
        infoPanelRect = infoPanel.GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO item)
    {
        infoPanel.alpha = 1;
        itemNameText.text = item.itemName;
        itemDescText.text = item.description;
        // Populate stat effects
        List<string> statList = new  List<string>();
        if(item.heal > 0)
            statList.Add($"+{item.heal} HP");
        foreach (KeyValuePair<UnitStat, float> stat in item.statEffectDict)
        {
            if (stat.Value > 0)
                statList.Add($"+{stat.Value} {stat.Key}");
        }
        if (statList.Count <= 0)
            return;
        for (int i = 0; i < statTexts.Length; i++)
        {
            if (i < statList.Count)
            {
                statTexts[i].text = statList[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescText.text = "";
    }

    public void FollowMouseHover()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 offset = new Vector3(5, -10, 0);
        infoPanelRect.position = mousePos + offset;
    }
}
