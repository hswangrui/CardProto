using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Actor : MonoBehaviour
{
    private ActorManager actorMrg;
    //�������ݴ�����ScriptObject����
    public ActorSO actorData;

    private IntReactiveProperty c_Health;

    public TextMesh mshTxt_Hp;
    private void Start()
    {
        Init();
    }

    public bool isDead = false;
    //��ʼ��
    public void Init()
    {
        actorMrg = GameManager.Instance.actorMrg;
        isDead = false;

        c_Health = new IntReactiveProperty(actorData.health);
        c_Health.ObserveEveryValueChanged(x => x.Value, FrameCountType.EndOfFrame)
                 .Subscribe(x => {
                     mshTxt_Hp.text = string.Format("HP:{0}", x);
                     if (x <= 0)
                     {
                         //����
                         Dead();
                       
                     }
                 
                 });


      

    }

    public void GetDamage(int damage)
    {
        int h = this.c_Health.Value;
        h -= damage;
        h = Mathf.Clamp(h, 0, actorData.health);

        this.c_Health.Value = h ;

    }

    public void GetHealth(int health)
    {
        int h = this.c_Health.Value;
        h += health;
        h = Mathf.Clamp(h, 0, actorData.health);
        this.c_Health.Value += h;

    }

    
    private void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
        actorMrg.ClearTarget(this);
        Debug.LogFormat("����{0}������",gameObject.name);
    }


    private void OnMouseUpAsButton()
    {

        actorMrg.onActorSelect(this);

        
    }


}

