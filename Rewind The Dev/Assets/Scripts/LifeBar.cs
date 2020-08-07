using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] GameObject fillRect;

    float currentLife;
    float startingScale;

    void Start()
    {
        currentLife = life;
        startingScale = fillRect.transform.localScale.x;
    }

    public void ChangeValue(int value)
    {
        if (currentLife < life) currentLife += value;
        if (currentLife > life) currentLife = life;

        float num = currentLife / life;
        Vector3 newScale = fillRect.transform.localScale;
        newScale.x = num * startingScale;
        fillRect.transform.localScale = newScale;
    }
}
