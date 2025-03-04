using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReGridPosition : IEquatable<ReGridPosition>
{
     public int x;
    public int z;

    public ReGridPosition(int x, int z)
    {

        this.x = x;
        this.z = z;
    }

    public static bool operator == (ReGridPosition a, ReGridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }
    public static bool operator != (ReGridPosition a, ReGridPosition b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        return obj is ReGridPosition position &&
            x == position.x &&
            z == position.z;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }
    public bool Equals(ReGridPosition other)
    {
        return this == other;
    }

    public static ReGridPosition operator +(ReGridPosition a, ReGridPosition b)
    {
        return new ReGridPosition(a.x + b.x, a.z + b.z);
    }
    public static ReGridPosition operator -(ReGridPosition a, ReGridPosition b)
    {
        return new ReGridPosition(a.x - b.x, a.z - b.z);
    }

    public override string ToString()
    {
        return "x: " + x + "; z: " + z;
    }
}
