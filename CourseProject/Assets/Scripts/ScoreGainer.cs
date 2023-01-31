using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGainer : MonoBehaviour, IScore
{
    [SerializeField] private int m_score;
    public int Score => m_score;
}
