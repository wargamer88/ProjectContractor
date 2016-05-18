using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbageWaveScript : MonoBehaviour {

    private List<int> _spawnXPoint = new List<int>() { -20, -10, 10,20 };
    private bool _canSpawn = true;
	// Use this for initialization
	void Start () {
        //z95
        //y0
        //x4 = -20 -10 10 20
	}
	
	// Update is called once per frame
	void Update () {
        if (_canSpawn)
        {
            int random = Random.Range(0, 4);
            List<int> oldRandoms = new List<int>();
            //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
            cube.AddComponent<GarbageMoveScript>();
            //cube.GetComponent<BoxCollider>().isTrigger = true;
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Rigidbody>().useGravity = false;
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
            cube.GetComponent<Renderer>().material.color = Color.green;
            //cube.AddComponent<GarbageAgainstGeneratorScript>();
            oldRandoms.Add(random);
            random = Random.Range(0, 4);
            if (!oldRandoms.Contains(random))
            {
                //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
                GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube1.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
                cube1.AddComponent<GarbageMoveScript>();
                //cube.GetComponent<BoxCollider>().isTrigger = true;
                cube1.AddComponent<Rigidbody>();
                cube1.GetComponent<Rigidbody>().useGravity = false;
                cube1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
                cube1.GetComponent<Renderer>().material.color = Color.blue;
                //cube1.AddComponent<GarbageAgainstGeneratorScript>();
                oldRandoms.Add(random);
            }
            random = Random.Range(0, 4);
            if (!oldRandoms.Contains(random))
            {
                //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
                GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube2.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
                cube2.AddComponent<GarbageMoveScript>();
                //cube.GetComponent<BoxCollider>().isTrigger = true;
                cube2.AddComponent<Rigidbody>();
                cube2.GetComponent<Rigidbody>().useGravity = false;
                cube2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
                cube2.GetComponent<Renderer>().material.color = Color.black;
               // cube2.AddComponent<GarbageAgainstGeneratorScript>();
                oldRandoms.Add(random);
            }
            random = Random.Range(0, 4);
            if (!oldRandoms.Contains(random))
            {
                //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
                GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube3.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
                cube3.AddComponent<GarbageMoveScript>();
                //cube.GetComponent<BoxCollider>().isTrigger = true;
                cube3.AddComponent<Rigidbody>();
                cube3.GetComponent<Rigidbody>().useGravity = false;
                cube3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
                cube3.GetComponent<Renderer>().material.color = Color.yellow;
                //cube3.AddComponent<GarbageAgainstGeneratorScript>();
                oldRandoms.Add(random);
            }
            _canSpawn = false;
            StartCoroutine(_waitForSeconds());
        }
    }

    private IEnumerator _waitForSeconds()
    {
        yield return new WaitForSeconds(5);
        _canSpawn = true;
    }
}
