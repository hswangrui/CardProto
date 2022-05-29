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


    //全局牌堆
    private List<Card> cardHolder=new List<Card>();
    //牌堆最大数
    private int cardHolderMaxCount = 20;
    //当前剩余牌堆数
    private IntReactiveProperty currentHolderCount = new IntReactiveProperty(0);


    public Transform cardRoot;
    public GameObject cardPrefab;

    //自动补牌间隔
    private float cardRecoverTime =2f;
    //自动补点数间隔
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

        //定时补牌
        Observable.Timer(TimeSpan.FromSeconds(cardRecoverTime))
                 .RepeatUntilDestroy(this)
                 .Subscribe(_ =>
                 {
                     if (cardMrg.CanGetCard())
                     {
                         cardMrg.FillCards(GetFromCardHolder(1));
                     }
                       
                 });
        //定时补点数
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
        //玩家随机获取五张卡
        cardMrg.FillCards(GetFromCardHolder(5));
        //获取测试目标
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
    /// 从牌堆中随机获取卡片
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
            //预先检测玩家点数
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
    /// 重置牌堆 重新生成牌
    /// </summary>
    public void RefreshCardHolder()
    {
        cardHolder.Clear();
        cardHolder = cardMrg.CreateRandomCards(cardHolderMaxCount);
        Debug.Log("牌堆不足 自动刷新.....");
    }
   




}
