using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR

[CustomEditor(typeof(AsteroidScriptableObject))]
public class AsteroidUIEditor : Editor
{
    public VisualTreeAsset m_UXML;

    public override VisualElement CreateInspectorGUI()
    {
        if (m_UXML == null)
            return null;

        VisualElement root = new VisualElement();
        m_UXML.CloneTree(root);

        return root;
    }
}
#endif