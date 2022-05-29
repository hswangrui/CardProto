using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase 
{
    public int cardCost; //��������
    public CardType cardType; //����
    public CardSkill[] cardSkills; //���ܰ�������Ч�����
    public string cardName; //����
    public string cardIntro; //˵��


}

public class CardSkill
{
  
    public SkillType skillType = SkillType.NONE;

    public CampType campType = CampType.NONE;
    //Ч��ֵ
    public int skillVal;

    public CardSkill(SkillType skillType,int val,CampType campType)
    {
        this.skillType = skillType;
        this.skillVal = val;
        this.campType = campType;
    }
}

//��������
public enum CardType
{
    NONE = 0,
    ATTACK = 10,
    EFFECT = 20,
    SPECIAL = 30

}
//������Ӫ
public enum CampType
{
    NONE=0,
    ENEMY=1,
    TEAMMATE=2
}

//Ч������
public enum SkillType
{
    NONE = 1000,
    INC_HEALTH = 2000, //����HP
    DEC_HEALTH = 3000, //����HP
    BUFF=4000,//buff
    DEBUFF=5000,//debuff
    DISPLACEMENT=6000//λ��

}