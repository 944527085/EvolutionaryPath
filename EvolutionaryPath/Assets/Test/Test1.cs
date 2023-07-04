using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         new test1(()=> { new test2(); });
    }
}
public class test1
{
    public test1(Action action=null)
    {
        Debug.Log("生成test1");
        action?.Invoke();
    }
}
public class test2
{
    public test2(Action action=null)
    {
        Debug.Log("生成test2");

        action?.Invoke();
    }
}
public class test3
{
    public test3()
    {
        Debug.Log("生成test3");
    }
}