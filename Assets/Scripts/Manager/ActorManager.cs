using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ActorManager :ManagerBase
{

    private List<Actor> actorList;
    private List<Actor> enemyList;

    private Actor selectActor=null;

    //事件
    public delegate void ActorSelectDelegate(Actor actors);
    public ActorSelectDelegate onActorSelect;


    public override void Init()
    {
        actorList = GameObject.FindObjectsOfType<Actor>().ToList();
        enemyList = actorList.FindAll(x => x.actorData.actorType == ActorType.BOSS);
        onActorSelect += OnActorSelect;
    }

    private void OnActorSelect(Actor actor)
    {
        if(actor.actorData.actorType!=ActorType.BOSS)
            return;
        if (selectActor != null)
        { //取消之前选择
            gameMrg.battleMrg.ClearTarget(selectActor);
            selectActor.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        }

        actor.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        gameMrg.battleMrg. AddTarget(actor);

        selectActor = actor;
    }
    public Actor[] GetAllEnemies()
    {
        return enemyList.ToArray();

    }


 
    public void ClearTarget(Actor actor)
    {

        gameMrg.battleMrg.ClearTarget(actor);
    }
    
}
