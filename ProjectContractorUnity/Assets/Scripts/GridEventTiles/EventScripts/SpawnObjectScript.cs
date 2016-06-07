﻿using UnityEngine;
using System.Collections;

public class SpawnObjectScript : MonoBehaviour {

    private static float _oldTime;
    private static int _spawned;
    private static bool _isFirstTime = true;

    private static int _amountLight;
    private static int _amountMedium;
    private static int _amountHeavy;
    private static int _amountSuperHeavy;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public static void SpawnBottle(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventEveryXWave, bool pEventEveryWave,GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition)
    {
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                Debug.Log(_spawned);
                if (_spawned != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _spawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }

    public static void SpawnRandomLight(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition)
    {
        Debug.Log(_amountLight);
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (_isFirstTime)
            {
                _amountLight += pEventAmountOfObjects;
            }
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 3);
                if (_spawned != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.LightGarbage[random];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.LightGarbage[random];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.LightGarbage[random];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _spawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }

    public static void SpawnRandomMedium(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition)
    {
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                Debug.Log(_spawned);
                int random = Random.Range(0, 3);
                if (_spawned != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _spawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }

    public static void SpawnRandomHeavy(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition)
    {
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                Debug.Log(_spawned);
                if (_spawned != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    int random = Random.Range(0, 2);
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _spawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }
    public static void SpawnSuperHeavy(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition)
    {
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                Debug.Log(_spawned);
                if (_spawned != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    int random = Random.Range(0, 1);
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[random];
                        pGarbageWaveScript._spawnGarbage(5, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[random];
                        pGarbageWaveScript._spawnGarbage(5, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        _spawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[random];
                        pGarbageWaveScript._spawnGarbage(5, pTilePosition.x + 1, 1, pTilePosition.z, bottle);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _spawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }
}
