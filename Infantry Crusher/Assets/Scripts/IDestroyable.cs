using System.Collections;
using UnityEngine;

public interface IDestroyable
{
    public void Damaged(Bullet bullet);

    public GameObject gameObject();
}
