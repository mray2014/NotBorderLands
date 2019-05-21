using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class SpawnerComponent : MonoBehaviour {

    [Range(0,100)]
    public float itemdropChance;

    float commonDropChance = 0;
    float unCommonDropChance = 0;
    float rareDropChance = 0;
    float epicDropChance = 0;
    float legendaryDropChance = 0;

    private float zero = 0;

    void OnGUI()
    {
        commonDropChance = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), commonDropChance, 0.0F, 100.0F);
    }
}


//public class SpawnerComponent : EditorWindow
//{

//    [Range(0, 100)]
//    public float itemdropChance;

//    float commonDropChance = 0;
//    float unCommonDropChance = 0;
//    float rareDropChance = 0;
//    float epicDropChance = 0;
//    float legendaryDropChance = 0;

//    private float zero = 0;

//    [MenuItem("Example/SpawnerComponent")]
//    static void Init()
//    {
//        SpawnerComponent window = (SpawnerComponent)GetWindow(typeof(SpawnerComponent));
//        window.Show();
//    }

//    void OnGUI()
//    {
//        EditorGUILayout.MinMaxSlider(ref zero, ref commonDropChance, 0, 100);
//        EditorGUILayout.MinMaxSlider(ref commonDropChance, ref unCommonDropChance, 0, 100);
//        EditorGUILayout.MinMaxSlider(ref unCommonDropChance, ref rareDropChance, 0, 100);
//        EditorGUILayout.MinMaxSlider(ref rareDropChance, ref epicDropChance, 0, 100);
//        EditorGUILayout.MinMaxSlider(ref epicDropChance, ref legendaryDropChance, 0, 100);

//        if (commonDropChance > unCommonDropChance)
//        {
//            unCommonDropChance = commonDropChance;
//        }
//        if (unCommonDropChance > rareDropChance)
//        {
//            rareDropChance = unCommonDropChance;
//        }
//        if (rareDropChance > epicDropChance)
//        {
//            epicDropChance = rareDropChance;
//        }
//        if (epicDropChance > legendaryDropChance)
//        {
//            legendaryDropChance = epicDropChance;
//        }
//    }
//}
