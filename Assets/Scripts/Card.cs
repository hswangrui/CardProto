using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : CardBase
{
    private CardManager cardMrg;
    //UI
    public CardUI cardUI;
    public Card(string cardName, CardType cardType,int cardCost,CardSkill[] cardSkills)
    {
        cardMrg = GameManager.Instance.cardMrg;

        this.cardName = cardName;
        this.cardType = cardType;
        this.cardCost = cardCost;
        this.cardSkills = cardSkills;

    }



    private void CreateUI()
    {
        GameObject go= GameObject.Instantiate(GameManager.Instance.cardPrefab, GameManager.Instance.cardRoot);
        cardUI = go.GetComponent<CardUI>();
        cardUI.SetCardUI(this);


        cardUI.btn_card.onClick.AddListener(OnSelectCard);
        cardUI.btn_release.onClick.AddListener(OnReleaseCard);
    }

    private void OnReleaseCard()
    {

        cardMrg.onCardRelease(this);
    }

    private void OnSelectCard()
    {

        cardMrg.onCardSelect(this);
    }

   


    //¸üÐÂÅÆ
    public void UpdateCard()
    {
        if (cardUI != null)
        {
            cardUI.SetCardUI(this);
        }
        else
        {
            CreateUI();
        }

    }

    public void RemoveCardUI()
    {
        if (cardUI != null)
        {
            cardUI.btn_card.onClick.RemoveAllListeners();
            GameObject.Destroy(cardUI.gameObject);
            this.cardUI = null;
        }
    }




   
}
