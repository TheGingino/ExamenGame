using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitieCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotContainer;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color usedColor = new Color(1, 1, 1, 0.2f);

    private Image[] dots;

    private void OnEnable()
    {
        if (CombatMeter.Instance != null)
            CombatMeter.Instance.OnAbilityLimitChanged += UpdateDots;
    }

    private void OnDisable()
    {
        CombatMeter.Instance.OnAbilityLimitChanged -= UpdateDots;
    }
    
    private void Start()
    {
        SpawnDots();
        UpdateDots();
    }

    private void SpawnDots()
    {
        foreach (Transform child in dotContainer)
        {
            Destroy(child.gameObject);
        }

        dots = new Image[CombatMeter.Instance.maxAbilitiesPerTurn];

        for (int i = 0; i < dots.Length; i++)
        {
            GameObject dot = Instantiate(dotPrefab, dotContainer);
            dots[i] = dot.GetComponent<Image>();
            Debug.Log("Dot {i} image: {dots[i]}");
        }
    }

    private void UpdateDots()
    {
        if (dots == null) return;
        int remaining = CombatMeter.Instance.AbilitiesRemaining;

        for (int i = 0; i < dots.Length; i++)
            dots[i].color = i < remaining ? activeColor : usedColor;
    }
}