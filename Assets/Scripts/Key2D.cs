using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2D {

    int x, y;
    Vector3 value;
    private int hash;

    public Key2D(int x, int y) {
        this.x = x;
        this.y = y;

        value = new Vector3(x, 0, y);
        CalcHash();
    }

    private void CalcHash() {
        hash = 7;
        hash = 71 * hash + x;
        hash = 71 * hash + y;
    }

    public override int GetHashCode() {
        return hash;
    }

    public override bool Equals(object obj) {
        return obj.GetHashCode() == GetHashCode();
    }

    public int X {
        get {
            return x;
        }
    }

    public int Y {
        get {
            return y;
        }
    }

    public void Add(int x, int y) {
        this.x += x;
        this.y += y;

        CalcHash();
    }
         


    public Vector3 Vector {
        get {
            return value;
        }
    }
}
