using UnityEngine;
using System.Collections;
using System;

public class Arguments : MonoBehaviour {

    #region variables
    //variable for the _userID of HEIM mainframe
    private int _userID;
    //variable for the _gameID of HEIM mainframe
    private int _gameID;
    //variable for the _username of HEIM mainframe
    private string _username;
    //variable for the _gametime of HEIM mainframe
    private int _gametime;
    //variable for the _conURL of HEIM mainframe
    private string _conURL;

    //properties to only read the corresponding values
    public int UserID { get { return _userID; } }
    public int GameID { get { return _gameID; } }
    public string Username { get { return _username; } }
    public int Gametime { get { return _gametime; } }
    public string ConURL { get { return _conURL; } } 
    #endregion

    /// <summary>
    /// When this gameobject awakes it will read the CommandLineArguments and put them in the corresponding variables
    /// </summary>
    void Awake() {
		string[] arguments = Environment.GetCommandLineArgs();
		_userID = Convert.ToInt32(arguments[2]);
		_gameID = Convert.ToInt32(arguments[3]);
		_username = arguments[4];
		_gametime = Convert.ToInt32(arguments[5]);
		_conURL = arguments[6];

        //_userID = 2;
        //_gameID = 6;
        //_username = "YVONNE";
        //_gametime = 10;
        //_conURL = "http://www.serellyn.net/HEIM/php/";

    }
}
