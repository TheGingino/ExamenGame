using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitieCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotContainer;

    private Image[] _dots;

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

        _dots = new Image[CombatMeter.Instance.maxAbilitiesPerTurn];

        for (int i = 0; i < _dots.Length; i++)
        {
            GameObject dot = Instantiate(dotPrefab, dotContainer);
            _dots[i] = dot.GetComponent<Image>();
            Debug.Log("Dot {i} image: {dots[i]}");
        }
    }

    private void UpdateDots()
    {
        if (_dots == null) return;

        int used = CombatMeter.Instance.maxAbilitiesPerTurn - CombatMeter.Instance.AbilitiesRemaining;

        for (int i = 0; i < _dots.Length; i++)
        {
            _dots[i].gameObject.SetActive(i >= used); // turns off when ability is used
        }
        // forceerd canvas opnieuw te bouwen
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(dotContainer as RectTransform);
    }
}