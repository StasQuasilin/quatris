using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2D {

    private int _x, _y;
    private int hash;

    public Key2D(int x, int y) {
        this._x = x;
        this._y = y;
        CalcHash();
    }

    void CalcHash() {
        hash = 7;
        hash = 71 * hash + _x;
        hash = 71 * hash + _y;
    }

    public override int GetHashCode() {
        return hash;
    }

    public override bool Equals(object obj) {
        return obj.GetHashCode() == GetHashCode();
    }

    public int x {
        get {
            return _x;
        }
    }

    public int y {
        get {
            return _y;
        }
    }

}
