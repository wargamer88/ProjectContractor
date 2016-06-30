using UnityEngine;
using System.Collections;

public class DBconnection : MonoBehaviour {
    
    /// <summary>
    /// <para>Only call with StartCoroutine with the Score as a parameter</para>
    /// <para>This uploads the score of the player with all the info of Arguments.cs to the HEIM Mainframe</para>
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
	public IEnumerator UploadScore(int score) {
        Arguments argumentsScript = FindObjectOfType<Arguments>();
        WWW post = new WWW(argumentsScript.ConURL + "insertScore.php?userID=" + argumentsScript.UserID + "&gameID=" + argumentsScript.GameID + "&score=" + score);
		yield return post;
        Debug.Log("Score uploaded");
        Application.Quit();
	}
}
