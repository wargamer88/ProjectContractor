﻿using UnityEngine;
using System.Collections;

public class SpawnObjectScript : MonoBehaviour {

    private static float _oldTime;
    private static int _spawned;
    private static bool _isFirstTime = true;

    public static bool IsFirstTime { set { _isFirstTime = value; } }

    private static int _amountBottle;

    private static Vector3 _previousPosition;

    private static int _amountLightSpawned;
    public static int AmountLightSpawned { set { _amountLightSpawned = value; } }
    private static int _amountMediumSpawned;
    public static int AmountMediumSpawned { set { _amountMediumSpawned = value; } }
    private static int _amountHeavySpawned;
    public static int AmountHeavySpawned { set { _amountHeavySpawned = value; } }
    private static int _amountSuperHeavySpawned;
    public static int AmountSpuerHeavySpawned { set { _amountSuperHeavySpawned = value; } }

    private static int _previousWave;

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
                if (_amountBottle != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                        pGarbageWaveScript._spawnGarbage(1,pTilePosition.x + 1, 4, pTilePosition.z, bottle);
                        _amountBottle++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
                        _amountBottle++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _amountBottle++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }

    public static int SpawnRandomLight(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, ExecuteEventScript pExe, float pEventSpeed)
    {
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }

        if (pGarbageWaveScript.Wave == pEventWave)
        {
            
            // Debug.Log("pEventWave : " + pEventWave);
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 3);
                //if (pExe.name == "C10" && pGarbageWaveScript.Wave == 8)
                //{
                //    Debug.Log("Kom HIERIN");
                //    Debug.Log("Needs to spawn: " + pEventAmountOfObjects);
                //    Debug.Log("Get to spawn: " + pEventAmountOfObjectsSpawned);
                //}
                if (pEventAmountOfObjectsSpawned  < pEventAmountOfObjects)
                {

                    //Debug.Log("Hier zijn we nu wel");
                    _oldTime = Time.time;
                    //if (pEventEveryXWave != 0 && pEventEveryWave)
                    //{
                    //    pEventWave = pEventWave + pEventEveryXWave;
                    //    GameObject bottle = pGarbageWaveScript.LightGarbage[random];
                    //    pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle,-pEventSpeed);
                    //    pEventAmountOfObjectsSpawned++;
                    //}
                    //else if (pEventEveryWave)
                    //{
                    //    pEventWave++;
                    //    GameObject bottle = pGarbageWaveScript.LightGarbage[random];
                    //    pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                    //    pEventAmountOfObjectsSpawned++;
                    //    //_event = _choices.SpawnBottle;
                    //}
                    //else
                    //{
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.LightGarbage[random];
                        pGarbageWaveScript._spawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        pEventAmountOfObjectsSpawned++;

                        //}
                    //}
                    _isFirstTime = false;
                }
                else
                {

                   // pExe.IsEventDone = true;
                }
            }
        }
        
        return pEventAmountOfObjectsSpawned;
    }

    public static int SpawnRandomMedium(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, float pEventSpeed)
    {
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            // Debug.Log("pEventWave : " + pEventWave);
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 3);
                if (pEventAmountOfObjectsSpawned < pEventAmountOfObjects)
                {

                    //Debug.Log("Hier zijn we nu wel");
                    _oldTime = Time.time;
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        pEventAmountOfObjectsSpawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        pEventAmountOfObjectsSpawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        pEventAmountOfObjectsSpawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
                else
                {

                    // pExe.IsEventDone = true;
                }
            }
        }
        return pEventAmountOfObjectsSpawned;

        #region oldMediumbeforecopiedMichielLight
        //if (pGarbageWaveScript.Wave == pEventWave)
        //{
        //    if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
        //    {
        //        Debug.Log(_spawned);
        //        int random = Random.Range(0, 3);
        //        if (pEventAmountOfObjects != pEventAmountOfObjectsSpawned)
        //        {
        //            _oldTime = Time.time;
        //            if (pEventEveryXWave != 0 && pEventEveryWave)
        //            {
        //                pEventWave = pEventWave + pEventEveryXWave;
        //                GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
        //                pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
        //                pEventAmountOfObjectsSpawned++;
        //            }
        //            else if (pEventEveryWave)
        //            {
        //                pEventWave++;
        //                GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
        //                pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1,4, pTilePosition.z, bottle);
        //                pEventAmountOfObjectsSpawned++;
        //                //_event = _choices.SpawnBottle;
        //            }
        //            else
        //            {
        //                //if (_eventEveryWave)
        //                //{
        //                GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
        //                pGarbageWaveScript._spawnGarbage(2, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
        //                //_isEventDone = true;
        //                // _event = _choices.None;
        //                _amountMediumSpawned++;
        //                //}
        //            }
        //            _isFirstTime = false;
        //        }
        //    }
        //}
        //return pEventAmountOfObjectsSpawned;
        #endregion
    }

    public static int SpawnRandomHeavy(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, float pEventSpeed)
    {
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            // Debug.Log("pEventWave : " + pEventWave);
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 2);
                if (pEventAmountOfObjectsSpawned < pEventAmountOfObjects)
                {

                   // Debug.Log("Hier zijn we nu wel");
                    _oldTime = Time.time;
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        pEventAmountOfObjectsSpawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        pEventAmountOfObjectsSpawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        pEventAmountOfObjectsSpawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
                else
                {

                    // pExe.IsEventDone = true;
                }
            }
        }
        return pEventAmountOfObjectsSpawned;
        #region oldheavybeforecopiedMichielLight
        //if (pGarbageWaveScript.Wave == pEventWave)
        //{
        //    if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
        //    {
        //        if (pEventAmountOfObjects != pEventAmountOfObjectsSpawned)
        //        {
        //            _oldTime = Time.time;
        //            int random = Random.Range(0, 2);
        //            if (pEventEveryXWave != 0 && pEventEveryWave)
        //            {
        //                pEventWave = pEventWave + pEventEveryXWave;
        //                GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
        //                pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
        //                pEventAmountOfObjectsSpawned++;
        //            }
        //            else if (pEventEveryWave)
        //            {
        //                pEventWave++;
        //                GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
        //                pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
        //                pEventAmountOfObjectsSpawned++;
        //                //_event = _choices.SpawnBottle;
        //            }
        //            else
        //            {
        //                //if (_eventEveryWave)
        //                //{
        //                GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
        //                pGarbageWaveScript._spawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle);
        //                //_isEventDone = true;
        //                // _event = _choices.None;
        //                pEventAmountOfObjectsSpawned++;
        //                //}
        //            }
        //            _isFirstTime = false;
        //        }

        //    }
        //}
        //return pEventAmountOfObjectsSpawned;
        #endregion
    }
    public static void SpawnSuperHeavy(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventEveryXWave, bool pEventEveryWave, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, float pEventSpeed)
    {
        if (pEventSpeed == 0)
        {
            pEventSpeed = -1;
        }
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                //Debug.Log(_spawned);
                if (_amountSuperHeavySpawned != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    int random = Random.Range(0, 1);
                    if (pEventEveryXWave != 0 && pEventEveryWave)
                    {
                        pEventWave = pEventWave + pEventEveryXWave;
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[random];
                        pGarbageWaveScript._spawnGarbage(5, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        _amountSuperHeavySpawned++;
                    }
                    else if (pEventEveryWave)
                    {
                        pEventWave++;
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[random];
                        pGarbageWaveScript._spawnGarbage(5, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        _amountSuperHeavySpawned++;
                        //_event = _choices.SpawnBottle;
                    }
                    else
                    {
                        //if (_eventEveryWave)
                        //{
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[random];
                        pGarbageWaveScript._spawnGarbage(5, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed);
                        //_isEventDone = true;
                        // _event = _choices.None;
                        _amountSuperHeavySpawned++;
                        //}
                    }
                    _isFirstTime = false;
                }
            }
        }
    }
}