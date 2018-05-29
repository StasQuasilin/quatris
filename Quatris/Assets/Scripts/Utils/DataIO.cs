using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataIO : MonoBehaviour {

    public static DataIO io = null;
    public string path;

    FileStream stream;
    BinaryFormatter formatter;

    void Awake() {

        if (io == null) {
            io = this;
        } else {
            Destroy( this );
        }

        path = Application.persistentDataPath;
        formatter = new BinaryFormatter();
    }

    public void Save(ISerializable data, FileMode mode = FileMode.Create) {
        stream = new FileStream( FileName(data.ToString()), mode );
        formatter.Serialize( stream, data );
        stream.Close();
    }

    public T Load<T>() {

        T data = Activator.CreateInstance<T>();

        try {
            stream = new FileStream( FileName(data.ToString()), FileMode.Open );
            data = ( T ) formatter.Deserialize( stream );
            stream.Close();
        } catch (Exception) {
            Debug.Log( "No save data for " + data.ToString() );
        }

        return data;
    }

    string FileName(string dataName) {
        return path + "\\" + dataName + ".t";
    }
}

[Serializable]
public class Data : ISerializable {

    public int _scores;
    public int _level;
    public List<KeyColorData> _levelData;
    public List<KeyColorData> _shapeData;

    public Data() { }

    public Data(int scores, int level, Dictionary<Key2D,Color> levelData, Dictionary<Key2D, Color> shapeData) {
        _scores = scores;
        _level = level;

        _levelData = new List<KeyColorData>();
        foreach (var pair in levelData) {
            _levelData.Add( new KeyColorData( pair.Key, pair.Value ) );
        }

        _shapeData = new List<KeyColorData>();
        foreach (var pair in shapeData) {
            _shapeData.Add( new KeyColorData( pair.Key, pair.Value ) );
        }
    }

    public Data(SerializationInfo info, StreamingContext context) {
        _scores = ( int ) info.GetValue( "scores", typeof( int ) );
        _level = ( int ) info.GetUInt32( "level" );
        _levelData = ( List<KeyColorData> ) info.GetValue( "data", typeof( List<KeyColorData> ) );
        _shapeData = ( List<KeyColorData> ) info.GetValue( "shape", typeof( List<KeyColorData> ) );
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        info.AddValue( "scores", _scores );
        info.AddValue( "level", _level );
        info.AddValue( "data", _levelData );
        info.AddValue( "shape", _shapeData );
    }

    public Dictionary<Key2D, Color> LevelData {
        get {
            Dictionary<Key2D, Color> mtrx = new Dictionary<Key2D, Color>();

            foreach (KeyColorData d in _levelData) {
                mtrx.Add( d.key, new Color( d.color.r, d.color.g, d.color.b ) );
            }

            return mtrx;
        }
    }

    public Dictionary<Key2D, Color> ShapeData {
        get {
            Dictionary<Key2D, Color> mtrx = new Dictionary<Key2D, Color>();

            foreach (KeyColorData d in _shapeData) {
                mtrx.Add( d.key, new Color( d.color.r, d.color.g, d.color.b ) );
            }

            return mtrx;
        }
    }

}

[Serializable]
public class ColorData {
    public float r, g, b;

    public ColorData(Color color) {
        r = color.r;
        g = color.g;
        b = color.b;
    }
}

[Serializable]
public class KeyColorData {
    public Key2D key;
    public ColorData color;

    public KeyColorData(Key2D k, Color c) {
        key = k;
        color = new ColorData( c );
    }
}

