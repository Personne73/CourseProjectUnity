using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GAMESTATE{ menu, play, pause, victory, gameover}

public class GameManager : MonoBehaviour, IEventHandler
{
    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    GAMESTATE m_State;
    public bool IsPlaying => m_State == GAMESTATE.play;
    
    int m_Score;
    void SetScore(int score)
    {
        m_Score = score;
        EventManager.Instance.Raise(new GameStatisticsChangedEvent(){eScore = m_Score, eCountDown = 0}); // ATTENTION
    }
    
    void IncrementScore(int increment)
    {
        SetScore(m_Score + increment);
    }
    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<EnemyHasBeenHitEvent>(EnemyHasBeenHit);
    }
    
    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    public void Awake()
    {
        if (!m_Instance) m_Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(GAMESTATE.menu);
    }

    void SetState(GAMESTATE newState)
    {
        m_State = newState;
        switch (m_State)
        {
            case GAMESTATE.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
                break;
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
                break;
        }
    }
    
    // MenuManager events callbacks
    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        SetState(GAMESTATE.play);
    }
     // Ball event callbacks
     void EnemyHasBeenHit(EnemyHasBeenHitEvent e)
     {
         IDestroyable destroyable = e.eEnemy.GetComponent<IDestroyable>();
         if (null != destroyable) destroyable.Kill();

         IScore scoreGainer = e.eEnemy.GetComponent<IScore>();
         if(null!=scoreGainer) IncrementScore(scoreGainer.Score);
     }
     
    // Update is called once per frame
    void Update()
    {
        
    }
}
