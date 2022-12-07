using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMenu : MonoBehaviour
{
    public Text textMessage;

    public void changeMessage(string newPhraseName)
    {
        textMessage.text = Lean.Localization.LeanLocalization.GetTranslationText(newPhraseName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
