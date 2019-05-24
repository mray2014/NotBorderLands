using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General
{

    [ExecuteInEditMode]
    public class EditorUpdater : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<IUniqueEditorElement>().ExecuteInEditor();
        }
    }
}
