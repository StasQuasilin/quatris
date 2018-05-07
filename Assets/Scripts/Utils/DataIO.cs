using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataIO {

    private FileStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    char[] spliter = new char []{' '};

    public DataIO(string p) {
        stream = new FileStream( p, FileMode.OpenOrCreate );
    }

    public void Save(Dictionary<Vector2Int, Color> data) {
        writer = new StreamWriter( stream );
        
        foreach (var pair in data) {

            writer.WriteLine(
                pair.Key.x + spliter[0] +
                pair.Key.y + spliter[0] +
                pair.Value.r + spliter[ 0 ] +
                pair.Value.g + spliter[ 0 ] +
                pair.Value.b + spliter[ 0 ]
                );
           
        }

        writer.Close();
    }

    public Dictionary<Vector2Int, Color> Load() {

        reader = new StreamReader( stream );
        Dictionary<Vector2Int, Color> result = new Dictionary<Vector2Int, Color>();
        string line;

        while ((line = reader.ReadLine()) != null) {
            string[] split = line.Split();
            result.Add(
                    new Vector2Int(
                        int.Parse(split[0]),    //x
                        int.Parse(split[1])     //y
                        ),
                    new Color(
                        float.Parse(split[2]),  //r
                        float.Parse(split[3]),  //g
                        float.Parse(split[4])   //b
                        )
                );
                
        }

        reader.Close();
        return result;
    }
}
