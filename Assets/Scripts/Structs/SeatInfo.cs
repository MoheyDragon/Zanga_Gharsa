using System;
using UnityEngine;
[Serializable]
public struct SeatInfo
{
    public Vector3 position;
    public bool isAccessable;

    public SeatInfo(Vector3 position, bool isAccessable)
    {
        this.position = position;
        this.isAccessable = isAccessable;
    }
}
