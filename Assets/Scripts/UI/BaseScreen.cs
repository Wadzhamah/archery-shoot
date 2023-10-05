using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public event Action OnOpen;
    public event Action OnClose;

    public virtual void Open()
    {
        OnOpen?.Invoke();
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        OnClose?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void Init()
    {
        
    }
}
