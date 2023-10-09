using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class STM_DestructableWall : MonoBehaviour, IDestructable
{
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
