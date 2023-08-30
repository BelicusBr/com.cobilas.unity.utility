using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private void OnGUI()
    {
        GUI.Label(new Rect(Vector2.zero, Vector2.right * 130f + Vector2.up * 25f), "Teste!!!");
    }
}
