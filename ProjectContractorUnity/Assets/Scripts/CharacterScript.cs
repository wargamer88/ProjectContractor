using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

    [SerializeField]
    private GameObject _builder;

    [SerializeField]
    private GameObject _scientist;

    [SerializeField]
    private GameObject _supervisor;

    public void CheerCharacters()
    {
        _builder.GetComponent<Animator>().Play("Cheering");
        _scientist.GetComponent<Animator>().Play("Cheer");
        _supervisor.GetComponent<Animator>().Play("Cheer");
    }
}
