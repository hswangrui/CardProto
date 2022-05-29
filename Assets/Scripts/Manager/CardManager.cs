using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Random = UnityEngine.Random;

public class CardManager : ManagerBase
{
    //��ɫ������
    public int maxCost = 10;
    //��ɫ��ʼ����
    public IntReactiveProperty totolCost;


    public int maxHoldCard = 5;

    //���еĿ���
    public List<Card> cardList;

    //��ǰѡ�еĿ�
    public Card selectedCard=null;



    //�¼�
    public delegate void CardSelectDelegate(Card card);
    public  CardSelectDelegate onCardSelect;

    public delegate void CardReleaseDelegate(Card card);
    public  CardReleaseDelegate onCardRelease;


  
    public override void Init()
    {
        totolCost = new IntReactiveProperty(maxCost);
        cardList = new List<Card>();
        onCardSelect += OnCardSelect;
        onCardRelease += OnCardRelease;
    }
    /// <summary>
    /// ��俨��
    /// </summary>
    /// <param name="getCards"></param>
    public void FillCards(List<Card>getCards)
    {
        if(cardList.Count>=maxHoldCard)
        {
           // Debug.LogFormat("�������Ϊ{0}����!", maxHoldCard);
        }
        else
        {
            //countС��0���Զ���������
           int needCardNum = Mathf.Min(getCards.Count, maxHoldCard - cardList.Count);
            for (int i = 0; i < needCardNum; i++)
            {
                Card c = getCards[i];
                cardList.Add(c);
                Debug.LogFormat("���һ���� ����{0} ����{1}", c.cardName, c.cardType);
            }
        }
        getCards.Clear();
        UpdatePlayerCards();
    }
   
    public void AddCost(int cost)
    {
            totolCost.Value += cost;
            totolCost.Value = Mathf.Clamp(totolCost.Value, 0, maxCost);
       // Debug.LogFormat("��ӵ���:{0} ",cost);
    }


  

    /// <summary>
    /// ���½�ɫ���е�����������Ϣ
    /// </summary>
    public void UpdatePlayerCards()
    {
        if (cardList.Count > 0)
        {
            foreach (var card in cardList)
            {
                card.UpdateCard();
            }

        }

    }


    public bool CanGetCard(int preAddCardnum)
    {
        return totolCost.Value+preAddCardnum >=0&&cardList.Count+ preAddCardnum <=maxHoldCard;
    }
    public bool CanGetCard()
    {
        return totolCost.Value > 0 && cardList.Count < maxHoldCard;
    }

    public bool CanAddCost(int preAddCost)
    {
        return totolCost.Value+preAddCost <= maxCost && totolCost.Value+preAddCost>=0;
    }

    public bool CanAddCost()
    {
        return totolCost.Value >=0 &&totolCost.Value<=maxCost;
    }
  

    private void OnCardSelect(Card card)
    {
        if (selectedCard != null)
            selectedCard.cardUI.btn_release.gameObject.SetActive(false);

        Debug.Log("ѡ��"+card.cardName);
         card.cardUI.btn_release.gameObject.SetActive(true);

        selectedCard = card;
    }

    private void OnCardRelease(Card card)
    {
        int lastCost = totolCost.Value - card.cardCost;

        if (!gameMrg.battleMrg.ExistTarget())
        {
            Debug.Log("�ͷ�ʧ�� Ŀ�겻����!");
            return;
        }

        if (lastCost >= 0)
        {
            Debug.Log("�ͷ�" + card.cardName);

            gameMrg.battleMrg.DoCardSkill(card);
            card.RemoveCardUI();

            cardList.Remove(card);

            selectedCard = null;

            totolCost.Value = lastCost;
        }
        else
            Debug.Log("�ͷ�ʧ�� ��������!");
    }

    public void RemovePlayerCard(Card card)
    {
        card.RemoveCardUI();
        cardList.Remove(card);


    }


    /// <summary>
    /// �������������
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public  List<Card> CreateRandomCards(int num)
    {
        Card newCard = null;
        List<Card> _cardlist = new List<Card>(num);
        for (int i = 0; i < num; i++)
        {
            //���������
            float typeSeed = Random.Range(0.0f, 100.0f);
            CardType cardType = CardType.NONE;
            //���Ƽ���
            CardSkill cardSkill = null;
            if (typeSeed <= 70)
            {
                cardType = CardType.ATTACK;
                cardSkill = new CardSkill(SkillType.DEC_HEALTH, Random.Range(5,26),CampType.ENEMY);
            }
            else if (typeSeed <= 90)
            {
                cardType = CardType.EFFECT;
                cardSkill = new CardSkill(SkillType.DEBUFF,150,CampType.ENEMY);
            }
            else if (typeSeed <= 100)
            {
                cardType = CardType.SPECIAL;
                cardSkill = new CardSkill(SkillType.DISPLACEMENT, 20,CampType.TEAMMATE);
            }
            string cardname = "Card" + Random.Range(0, 101);
            int cardCost = Random.Range(1, 6);

            List<CardSkill> skillList = new List<CardSkill>() { cardSkill };

            newCard = new Card( cardname, cardType, cardCost,skillList.ToArray());
            _cardlist.Add(newCard);
        }
        return _cardlist;
    }



}
