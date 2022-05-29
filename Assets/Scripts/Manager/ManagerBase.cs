using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase 
{
   protected GameManager gameMrg = GameManager.Instance;

   abstract public void Init();
}
