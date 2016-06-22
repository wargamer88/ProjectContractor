﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SpawnObjectScript {

    #region Variables
    //Set old time for a timer
    private static float _oldTime;
    //check if it is the first time to spawn an object to prevent multiple spawning at the same time.
    private static bool _isFirstTime = true;
    //Tutorial Bottle count
    private static int _amountBottle;
    #endregion

    /// <summary>
    /// <para>Event to Spawn a bottle if the wave is the wave that is in the inspector</para>
    /// </summary>
    /// <param name="pEventWave">Wave that is in the inspector</param>
    /// <param name="pEventTimeBetween">Time that is between the spawning of object in the inspector</param>
    /// <param name="pEventAmountOfObjects">Amount of object that needs to be spawned</param>
    /// <param name="pGarbageWaveScript">The garbage waveScript so I can check if we are in the right wave</param>
    /// <param name="pTilePosition">Position of the tile the object needs to be spawned in</param>
    /// <param name="pExe">The script of the tile so can read the tile name</param>
    public static void SpawnBottle(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects,GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, ExecuteEventScript pExe)
    {
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            //timer for spawning
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                if (_amountBottle != pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    GameObject bottle = pGarbageWaveScript.LightGarbage[0];
                    pGarbageWaveScript.SpawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -1, pExe.gameObject.name);
                    _amountBottle++;
                }
            }
        }
    }
    /// <summary>
    /// <para>Event to Spawn a light garbage if the wave is the wave that is in the inspector</para>
    /// </summary> 
    /// <param name="pEventWave">Wave that is in the inspector</param>
    /// <param name="pEventTimeBetween">Time that is between the spawning of object in the inspector</param>
    /// <param name="pEventAmountOfObjects">Amount of object that needs to be spawned</param>
    /// <param name="pGarbageWaveScript">The garbage waveScript so I can check if we are in the right wave</param>
    /// <param name="pTilePosition">Position of the tile the object needs to be spawned in</param>
    /// <param name="pExe">The script of the tile so can read the tile name</param>
    /// <param name="pEventAmountOfObjectsSpawned">amount that is already spawned on another tile</param>
    /// <param name="pEventSpeed">Speed of the object</param>
    /// <returns></returns>
    public static int SpawnRandomLight(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, ExecuteEventScript pExe, float pEventSpeed)
    {
        //set speed to defualt
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }
        //check if current wave of tile the same is as the wave in the garbagewavescript
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            //timer for spawning
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 3); //random between the light garbage
                if (pEventAmountOfObjectsSpawned  < pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    //check if the generator of the lane is dead
                    if (!pGarbageWaveScript.DeadLaneList.Contains(pExe.transform.name.Substring(0, 1)))
                    {
                        GameObject bottle = pGarbageWaveScript.LightGarbage[random];

                        pGarbageWaveScript.SpawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed, pExe.gameObject.name);
                    }
                    pEventAmountOfObjectsSpawned++;
                    _isFirstTime = false;
                }
            }
        }
        
        return pEventAmountOfObjectsSpawned;
    }

    /// <summary>
    /// <para>Event to Spawn a medium garbage if the wave is the wave that is in the inspector</para>
    /// </summary> 
    /// <param name="pEventWave">Wave that is in the inspector</param>
    /// <param name="pEventTimeBetween">Time that is between the spawning of object in the inspector</param>
    /// <param name="pEventAmountOfObjects">Amount of object that needs to be spawned</param>
    /// <param name="pGarbageWaveScript">The garbage waveScript so I can check if we are in the right wave</param>
    /// <param name="pTilePosition">Position of the tile the object needs to be spawned in</param>
    /// <param name="pExe">The script of the tile so can read the tile name</param>
    /// <param name="pEventAmountOfObjectsSpawned">amount that is already spawned on another tile</param>
    /// <param name="pEventSpeed">Speed of the object</param>
    public static int SpawnRandomMedium(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, ExecuteEventScript pExe, float pEventSpeed)
    {
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }
        //check if current wave of tile the same is as the wave in the garbagewavescript
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            //timer for spawning
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 3);//random between the medium garbage
                if (pEventAmountOfObjectsSpawned < pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    //check if the generator of the lane is dead
                    if (!pGarbageWaveScript.DeadLaneList.Contains(pExe.transform.name.Substring(0, 1)))
                    {
                        GameObject bottle = pGarbageWaveScript.MediumGarbage[random];
                        pGarbageWaveScript.SpawnGarbage(2, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed, pExe.gameObject.name);
                    }
                    pEventAmountOfObjectsSpawned++;
                    _isFirstTime = false;
                }
            }
        }
        return pEventAmountOfObjectsSpawned;
    }
    /// <summary>
    /// <para>Event to Spawn a heavy garbage if the wave is the wave that is in the inspector</para>
    /// </summary> 
    /// <param name="pEventWave">Wave that is in the inspector</param>
    /// <param name="pEventTimeBetween">Time that is between the spawning of object in the inspector</param>
    /// <param name="pEventAmountOfObjects">Amount of object that needs to be spawned</param>
    /// <param name="pGarbageWaveScript">The garbage waveScript so I can check if we are in the right wave</param>
    /// <param name="pTilePosition">Position of the tile the object needs to be spawned in</param>
    /// <param name="pExe">The script of the tile so can read the tile name</param>
    /// <param name="pEventAmountOfObjectsSpawned">amount that is already spawned on another tile</param>
    /// <param name="pEventSpeed">Speed of the object</param>
    public static int SpawnRandomHeavy(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, ExecuteEventScript pExe, float pEventSpeed)
    {
        //set speed to defualt
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }
        //check if current wave of tile the same is as the wave in the garbagewavescript
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                int random = Random.Range(0, 2);//random between the heavy garbage
                if (pEventAmountOfObjectsSpawned < pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    //check if the generator of the lane is dead
                    if (!pGarbageWaveScript.DeadLaneList.Contains(pExe.transform.name.Substring(0, 1)))
                    {
                        GameObject bottle = pGarbageWaveScript.HeavyGarbage[random];
                        pGarbageWaveScript.SpawnGarbage(3, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed, pExe.gameObject.name);
                    }
                    pEventAmountOfObjectsSpawned++;
                    _isFirstTime = false;
                }
            }
        }
        return pEventAmountOfObjectsSpawned;
    }

    /// <summary>
    /// <para>Event to Spawn a super heavy garbage if the wave is the wave that is in the inspector</para>
    /// </summary> 
    /// <param name="pEventWave">Wave that is in the inspector</param>
    /// <param name="pEventTimeBetween">Time that is between the spawning of object in the inspector</param>
    /// <param name="pEventAmountOfObjects">Amount of object that needs to be spawned</param>
    /// <param name="pGarbageWaveScript">The garbage waveScript so I can check if we are in the right wave</param>
    /// <param name="pTilePosition">Position of the tile the object needs to be spawned in</param>
    /// <param name="pExe">The script of the tile so can read the tile name</param>
    /// <param name="pEventAmountOfObjectsSpawned">amount that is already spawned on another tile</param>
    /// <param name="pEventSpeed">Speed of the object</param>
    public static int SpawnSuperHeavy(int pEventWave, float pEventTimeBetween, int pEventAmountOfObjects, int pEventAmountOfObjectsSpawned, GarbageWaveScript pGarbageWaveScript, Vector3 pTilePosition, ExecuteEventScript pExe, float pEventSpeed)
    {
        //set speed to defualt
        if (pEventSpeed == 0)
        {
            pEventSpeed = 1;
        }
        //check if current wave of tile the same is as the wave in the garbagewavescript
        if (pGarbageWaveScript.Wave == pEventWave)
        {
            //timer for spawning
            if (Time.time > (_oldTime + pEventTimeBetween) || _isFirstTime)
            {
                if (pEventAmountOfObjectsSpawned < pEventAmountOfObjects)
                {
                    _oldTime = Time.time;
                    //check if the generator of the lane is dead
                    if (!pGarbageWaveScript.DeadLaneList.Contains(pExe.transform.name.Substring(0, 1)))
                    {
                        GameObject bottle = pGarbageWaveScript.SpecialGarbage[0];
                        pGarbageWaveScript.SpawnGarbage(1, pTilePosition.x + 1, 4, pTilePosition.z, bottle, -pEventSpeed, pExe.gameObject.name);
                    }
                    pEventAmountOfObjectsSpawned++;
                    _isFirstTime = false;
                }
            }
        }
        return pEventAmountOfObjectsSpawned;
    }
}