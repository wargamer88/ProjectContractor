using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

    private float _amountOfWater = 0;
    private int _roundedAmountOfWater = 0;

    public float AmountOfWater { get { return _amountOfWater; } set { _amountOfWater = value; } }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        _roundedAmountOfWater = Mathf.RoundToInt(_amountOfWater);

        switch (_roundedAmountOfWater)
        {
            case 0:
                this.GetComponent<Renderer>().enabled = false;
                break;
            case 1:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 2:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 3:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 4:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 5:
                this.GetComponent<Renderer>().enabled = true;
                break;
            default:
                break;
        }
    }
}
