using UnityEngine;
using System.Collections;

public class DBconnection : MonoBehaviour {
    
	void Start() {

	}

	public IEnumerator UploadScore(int score) {
        Arguments argumentsScript = FindObjectOfType<Arguments>();
        Debug.Log("Score uploaded");
        WWW post = new WWW(argumentsScript.ConURL + "insertScore.php?userID=" + argumentsScript.UserID + "&gameID=" + argumentsScript.GameID + "&score=" + score);
		yield return post;
		Application.Quit();
	}
}
