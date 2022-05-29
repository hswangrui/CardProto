using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardUI : MonoBehaviour
{
    public Text txt_cardName;
    public Image img_cardBg;
    public Text txt_cardIntro;
    public Text txt_cardCost;

    public Button btn_card;

    public Button btn_release;

    Color Col1 = new Color(.8f, .3f, .3f);
    Color Col2 = new Color(.3f, .8f, .8f);
    Color Col3 = new Color(.8f, .8f, .3f);
    public void SetCardUI(Card card)
    {
        this.txt_cardName.text = card.cardName;
       
        // this.txt_cardIntro.text = card.cardIntro;
        this.txt_cardCost.text = card.cardCost.ToString();
        SetCardIntro(card);

        switch (card.cardType)
        {
            case CardType.NONE:
                break;
            case CardType.ATTACK:
                {
                    img_cardBg.color = Col1;
                }
                break;
            case CardType.EFFECT:
                img_cardBg.color = Col2;
                break;
            case CardType.SPECIAL:
                img_cardBg.color = Col3;
                break;
            default:
                break;
        }


       
    }

   private void SetCardIntro(Card card)
    {
        CardSkill[] skills = card.cardSkills;

        string intro = "";
        foreach (var skill in skills)
        {
            string camp = "";
            if (skill.campType == CampType.ENEMY)
                camp = "敌方";
            else if (skill.campType == CampType.TEAMMATE)
                camp = "我方";

            switch (skill.skillType)
            {
                case SkillType.NONE:
                    break;
                case SkillType.DEC_HEALTH:
                    intro += string.Format("对{0}造成{1}点伤害,", camp, skill.skillVal);
                    break;
                case SkillType.INC_HEALTH:
                    intro += string.Format("恢复{0}{1}点生命,", camp, skill.skillVal);
                    break;
                case SkillType.BUFF:
                    intro += string.Format("对{0}施加Buff,", camp);
                    break;
                case SkillType.DEBUFF:
                    intro += string.Format("对{0}施加DeBuff,", camp);
                    break;
                case SkillType.DISPLACEMENT:
                    intro += string.Format("{0}进行位移,", camp);
                    break;
                default:
                    break;
            }

        }
        this.txt_cardIntro.text = intro;
    }
}
