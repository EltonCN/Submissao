using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestPrintSigil : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;
    public void printSigil(InkSigil sigil)
    {
        print("I received sigil: "+ sigil.name);

        if(debugText != null)
        {
            debugText.text = sigil.name;
            debugText.SetAllDirty();
        }
    }
}
