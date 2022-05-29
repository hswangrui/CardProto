using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase 
{
    public int cardCost; //点数花费
    public CardType cardType; //类型
    public CardSkill[] cardSkills; //可能包含多种效果混合
    public string cardName; //卡名
    public string cardIntro; //说明


}

public class CardSkill
{
  
    public SkillType skillType = SkillType.NONE;

    public CampType campType = CampType.NONE;
    //效果值
    public int skillVal;

    public CardSkill(SkillType skillType,int val,CampType campType)
    {
        this.skillType = skillType;
        this.skillVal = val;
        this.campType = campType;
    }
}

//卡牌类型
public enum CardType
{
    NONE = 0,
    ATTACK = 10,
    EFFECT = 20,
    SPECIAL = 30

}
//作用阵营
public enum CampType
{
    NONE=0,
    ENEMY=1,
    TEAMMATE=2
}

//效果类型
public enum SkillType
{
    NONE = 1000,
    INC_HEALTH = 2000, //增加HP
    DEC_HEALTH = 3000, //减少HP
    BUFF=4000,//buff
    DEBUFF=5000,//debuff
    DISPLACEMENT=6000//位移

}