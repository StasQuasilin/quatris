using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LanguageBase : MonoBehaviour {

    Dictionary<string, string> library = new Dictionary<string, string>();
    string dataPath;
    
    StreamReader reader;
    char[] separator = { ':', '=' };

    public enum Language {
        eng, ru, ua
    }

    public static LanguageBase l;

    void Start() {
        if (l == null) {
            l = this;
        } else {
            Destroy( this );
        }

        dataPath = Application.dataPath + "\\";
        Load( Language.eng );
    }

    void Load(Language lang) {

        try {

            reader = new StreamReader( dataPath + lang, false );

            library.Clear();

            string line;
            while (( line = reader.ReadLine() ) != null) {
                string[] split = line.Split( separator );
                library.Add( split[ 0 ], split[ 1 ] );
            }

            reader.Close();

        } catch (FileNotFoundException e) {
            Debug.Log( string.Format( "No '{0}; language file", lang ) );
            File.Create( dataPath + lang );
        }

        
    }

	public string get(string key) {
        if (library.ContainsKey(key)) {
            return library[ key ];
        } else {
            return key;
        }
    }

    void OnGUI() {
        GUI.Label( new Rect( 2, 2, 100, 20 ), get( "tst" ) );
    }
}
