using System.Collections.Generic;
using UnityEngine;

public abstract class SigilDetector : MonoBehaviour
{
    abstract public InkSigil detectSigil(List<Vector2> draw);
}