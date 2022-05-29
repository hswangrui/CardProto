using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActorType
{
    NONE = 100,
    TANK = 200,
    MAGE = 300,
    NURSE = 400,

    BOSS = 1000

}

[CreateAssetMenu(fileName = "ActorData", menuName = "CustomSO/ActorData", order = 1)]
public  class ActorSO :ScriptableObject
{

    public ActorType actorType=ActorType.NONE;
    public int health=100;
    public int hunger = 100;
    public int strength;
    public int fortunate;
    public int intelligence;


    private float p_atk;
    private float m_atk;
    private float criticle;


    public void  SetCustomData(int health=100,int hunger=100,int strength=10,int fortunate=20,int intelligence=20)
    {

        this.health = health;
        this.hunger = hunger;
        this.strength = strength;
        this.fortunate = fortunate;
        this.intelligence = intelligence;

        //数值计算公式
        this.p_atk = this.strength * 100.0f / 100f;
        this.m_atk = this.intelligence * 100.0f / 100f;
        this.criticle = this.fortunate * 100.0f / 100f;
    }



    //public int Health { get => health; set => health = value; }
    //public int Hunger { get => hunger; set => hunger = value; }
    //public int Strength { get => strength; set => strength = value; }
    //public int Fortunate { get => fortunate; set => fortunate = value; }
    //public int Intelligence { get => intelligence; set => intelligence = value; }

   
    
   
}
