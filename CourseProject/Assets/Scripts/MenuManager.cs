using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using Event = UnityEngine.Event;

public class MenuManager : MonoBehaviour, IEventHandler
{
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_VictoryPanel;
    [SerializeField] GameObject m_GameOverPanel;

    List<GameObject> m_Panels = new List<GameObject>();
    
    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item=>item.SetActive(item == panel));
    }
    
    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
    }
    
    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Awake()
    {
        m_Panels = new List<GameObject>(new GameObject[] { m_MainMenuPanel, m_VictoryPanel, m_GameOverPanel });
    }

    // GameManager Events callbacks
    void GameMenu(GameMenuEvent e)
    {
        OpenPanel(m_MainMenuPanel);
    }
    
    void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
    }
    // UI callbacks
    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }
}
