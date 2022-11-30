using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrintSigil : MonoBehaviour
{
    public void printSigil(InkSigil sigil)
    {
        print("I received sigil: "+ sigil.name);
    }
}
