using System.Collections.Generic;
using UnityEngine;

// 主题
public interface I_Subject_Select
{
    void Attach(I_Observer_Select observer);
    void Detach(I_Observer_Select observer);
    void Notify(GameObject obj);
}

// 观察者
public interface I_Observer_Select
{
    void Handle_Select(GameObject obj);
}

// 实现
public class Subject_Select : MonoBehaviour, I_Subject_Select
{
    private readonly List<I_Observer_Select> _observers = new();

    public void Attach(I_Observer_Select observer)
    {
        _observers.Add(observer);
    }

    public void Detach(I_Observer_Select observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(GameObject obj)
    {
        foreach (var observer in _observers)
        {
            observer.Handle_Select(obj);
        }
    }
}