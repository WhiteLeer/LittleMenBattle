using System.Collections.Generic;
using UnityEngine;

// 主题
public interface I_Subject_Init
{
    void Attach(I_Observer_Init observer);
    void Detach(I_Observer_Init observer);
    void Notify();
}

// 观察者
public interface I_Observer_Init
{
    void Handle_Init();
}

// 实现
public class Subject_Init : MonoBehaviour, I_Subject_Init
{
    private readonly List<I_Observer_Init> _observers = new();

    public void Attach(I_Observer_Init observer)
    {
        _observers.Add(observer);
    }

    public void Detach(I_Observer_Init observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Handle_Init();
        }
    }
}