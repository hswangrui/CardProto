using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityTimer;
public class BattleManager :ManagerBase
{

    //释放对象
    private List<Actor> targetList=null;
    //前释放技能类型 判断连招
    private Card lastUseCard =null;

    private bool isInCombo = false;

    public override void Init()
    {
        targetList = new List<Actor>();
        
    }



    //释放技能
    public  void DoCardSkill(Card card)
    {
     
        if (!ExistTarget())
            return;
  

        foreach (var skill in card.cardSkills)
        {

            switch (skill.skillType)
            {
                case SkillType.NONE:
                    break;
                case SkillType.INC_HEALTH:
                    foreach (var target in targetList)
                    {
                        target.GetHealth(skill.skillVal);
                    }
                   
                    break;
                case SkillType.DEC_HEALTH:
                    foreach (var target in targetList)
                    {
                        target.GetDamage(skill.skillVal);
                    }
                    break;
                case SkillType.BUFF:
                    break;
                case SkillType.DEBUFF:
                    break;
                case SkillType.DISPLACEMENT:
                    break;
                default:
                    break;
            }
          

        }
       


    
        lastUseCard = card;
        Debug.Log("Do Skill");
    }
 
    public bool ExistTarget()
    {
        return targetList != null && targetList.Count > 0;

    }
    public void AddTarget(Actor actor)
    {
        if(!targetList.Contains(actor))
            targetList.Add(actor);

    }

    public void AddTargets(Actor[] actors)
    {
        foreach (var actor in actors)
        {
            if (!targetList.Contains(actor))
                targetList.Add(actor);
        }

    }
    public void ClearTarget(Actor actor)
    {
        targetList.Remove(actor);
    }


    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <param name="card">卡片</param>
    /// <param name="releaser">我方角色</param>
    /// <param name="target">目标角色</param>
    /// <returns></returns>
    private int CalDamage(Card card,Actor releaser,Actor target)
    {
        int damage = 0;
        //计算公式
        //card.ca

        return damage;
    }
}
