using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class ScoresContainer {

    private static ScoresContainer instance;

    public static ScoresContainer Instance {
        get {

            if (instance == null) {
                instance = new ScoresContainer();
            }

            return instance;
        }
    }

    int scores;

    public void Add(int count) {
        scores += count;
    }

    public int Scores {
        get {
            return scores;
        }
        set {
            scores = value;
        }
    }

    public void Clear () {
        scores = 0;
    }


}
class Scores : ISerializable {

    internal List<ScoresData> data = new List<ScoresData>();

    public Scores() { }

    public Scores(SerializationInfo info, StreamingContext context) {
        data = ( List<ScoresData> ) info.GetValue( "data", typeof( List<ScoresData> ) );
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        info.AddValue( "data", data );
    }

    public void Add(int scores) {
        data.Add( new ScoresData( scores ) );
        data.Sort();
    }
}

[Serializable]
public class ScoresData : IComparable<ScoresData> {

    public DateTime date;
    public int scores;
    int hash;

    public ScoresData(int scores) {
        date = DateTime.Now;
        this.scores = scores;

        hash = date.GetHashCode() * 71 + scores;
    }

    public int CompareTo(ScoresData other) {
        if (scores > other.scores) {
            return 1;
        } else if (scores < other.scores) {
            return -1;
        } else {
            return 0;
        }
    }
}
