using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSingleton : Singleton<SimpleSingleton>
{
    public  void DoSomething()
    {
        print("I'm singleton! Hello!");
    }
}
