public class Vector2Int {

    public int x, y;
    private int hash;

    public Vector2Int(int x, int y) {
        this.x = x;
        this.y = y;

        CalcHash();
    }

    void CalcHash() {
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

    public override string ToString() {
        return "(" + x + ", " + y + ")";
    }
}
