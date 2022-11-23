using System.Collections.Generic;
using UnityEngine;

public abstract class SigilDetector : MonoBehaviour
{
    [SerializeField] GameEvent detectedSigilEvent;
    public InkSigil detectSigil(List<Vector2> draw)
    {
        InkSigil detectedSigil = detectSigilImplementation(draw);
        detectedSigilEvent?.Raise<InkSigil>(detectedSigil);

        return detectedSigil;
    }

    abstract protected InkSigil detectSigilImplementation(List<Vector2> draw);
}