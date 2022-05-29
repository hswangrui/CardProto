using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Manager
    public UIBattle uiBattle;

    public static GameManager Instance;
    public ActorManager actorMrg;
    public BattleManager battleMrg;

    public CardManager cardMrg;
    #endregion


    //ȫ���ƶ�
    private List<Card> cardHolder=new List<Card>();
    //�ƶ������
    private int cardHolderMaxCount = 20;
    //��ǰʣ���ƶ���
    private IntReactiveProperty currentHolderCount = new IntReactiveProperty(0);


    public Transform cardRoot;
    public GameObject cardPrefab;

    //�Զ����Ƽ��
    private float cardRecoverTime =2f;
    //�Զ����������
    private float costRecoverTime = 2f;

    private void Awake()
    {
        Instance = this;

        actorMrg = new ActorManager();
        battleMrg = new BattleManager();
        cardMrg = new CardManager();

        Init();
    }

    private void Init()
    {
        actorMrg.Init();
        battleMrg.Init();
        cardMrg.Init();

        BattleUIRegister();

        StartBattle();
    }

    private void StartBattle()
    {

        //��ʱ����
        Observable.Timer(TimeSpan.FromSeconds(cardRecoverTime))
                 .RepeatUntilDestroy(this)
                 .Subscribe(_ =>
                 {
                     if (cardMrg.CanGetCard())
                     {
                         cardMrg.FillCards(GetFromCardHolder(1));
                     }
                       
                 });
        //��ʱ������
        Observable.Timer(TimeSpan.FromSeconds(costRecoverTime))
                .RepeatUntilDestroy(this)
                .Subscribe(_ =>
                {
                    if (cardMrg.CanAddCost())
                    {
                        cardMrg.AddCost(1);

                    }
                });
        
        RefreshCardHolder();
        //��������ȡ���ſ�
        cardMrg.FillCards(GetFromCardHolder(5));
        //��ȡ����Ŀ��
        //battleMrg.AddTargets(actorMrg.GetAllEnemies());
    }

    private void BattleUIRegister()
    {
        UIBattle uimrg = uiBattle;
        cardMrg.totolCost.ObserveEveryValueChanged(x => x.Value, FrameCountType.EndOfFrame)
                         .Subscribe(x => { uimrg.txt_totalCost.text = x.ToString(); });

        Observable.EveryEndOfFrame()
            .Subscribe(x => {
                currentHolderCount.Value = cardHolder.Count;
                uimrg.txt_cardTotalNum.text = currentHolderCount.Value.ToString();
            });


    }



    /// <summary>
    /// ���ƶ��������ȡ��Ƭ
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public List<Card> GetFromCardHolder(int num=1)
    {

        if (cardHolder.Count < num)
            RefreshCardHolder();

        Card seleterd = null;
        List<Card> cardList = new List<Card>(num);

        int checkPoint = cardMrg.totolCost.Value;
        for (int i = 0; i < num; i++)
        {
            int index = Random.Range(0, cardHolder.Count);
            seleterd = cardHolder[index];
            //Ԥ�ȼ����ҵ���
          //  checkPoint -= seleterd.cardCost;
           // if (checkPoint>=0&&cardMrg.CanGetCard(1))
          //  {
                cardList.Add(seleterd);
                cardHolder.Remove(seleterd);
         //   }
          //  else
          //  {
         //       break;
        //    }
        }
        return cardList;
    }

    /// <summary>
    /// �����ƶ� ����������
    /// </summary>
    public void RefreshCardHolder()
    {
        cardHolder.Clear();
        cardHolder = cardMrg.CreateRandomCards(cardHolderMaxCount);
        Debug.Log("�ƶѲ��� �Զ�ˢ��.....");
    }
   




}
