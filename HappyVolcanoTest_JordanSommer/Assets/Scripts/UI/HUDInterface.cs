﻿using UnityEngine;

public class HUDInterface : MonoBehaviour
{
    public delegate void CubeAmountChanged(int amount);
    public CubeAmountChanged OnCubeAmountChanged;

    public delegate void ColorAmountChanged(int amount);
    public ColorAmountChanged OnColorAmountChanged;

    public delegate void HitTimeChanged(float hitTime);
    public HitTimeChanged OnHitTimeChanged;

    public void ChangeCubeAmount(string input)
    {
        if (input.Length == 0)
            return;
        int amount = 0;
        if (int.TryParse(input, out amount))
            OnCubeAmountChanged?.Invoke(amount);
        else
            Debug.Log("Not a valid integer entered.");
    }

    public void ChangeColorAmount(string input)
    {
        if (input.Length == 0)
            return;
        int amount = 0;
        if (int.TryParse(input, out amount))
            OnColorAmountChanged?.Invoke(amount);
        else
            Debug.Log("Not a valid integer entered.");
    }

    public void ChangeHitTime(string input)
    {
        if (input.Length == 0)
            return;
        float time = 0;
        if (float.TryParse(input, out time))
            OnHitTimeChanged?.Invoke(time);
        else
            Debug.Log("Not a valid float entered.");
    }
}
