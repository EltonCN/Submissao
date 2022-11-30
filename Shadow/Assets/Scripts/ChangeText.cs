using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;

[System.Serializable]
struct EventLocalizationData
{
    public GameEvent gameEvent;
    public string textName;
}

[RequireComponent(typeof(LeanLocalizedBehaviour))]
public class ChangeText : MonoBehaviour, EventListener
{
    [SerializeField] EventLocalizationData[] localizationDatas;

    LeanLocalizedBehaviour localizedBehaviour;

    void Start()
    {
        localizedBehaviour = GetComponent<LeanLocalizedBehaviour>();
    }

    void OnEnable()
    {
        foreach(EventLocalizationData localizationData in localizationDatas)
        {
            localizationData.gameEvent.RegisterListener(this);
        }
    }
    
    void OnDisable()
    {
        foreach(EventLocalizationData localizationData in localizationDatas)
        {
            localizationData.gameEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(string eventName)
    {
        foreach(EventLocalizationData localizationData in localizationDatas)
        {
            if(localizationData.gameEvent.name == eventName)
            {
                localizedBehaviour.TranslationName = localizationData.textName;
                break;
            }
        }
    }

    public void OnEventRaised<T>(string eventName, T arg)
    {
        throw new System.NotImplementedException();
    }
}
