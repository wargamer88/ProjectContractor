using UnityEngine;
using System.Collections;
using System;

public class Arguments : MonoBehaviour {
	
	private int _userID;
	private int _gameID;
	private string _username;
	private int _gametime;
	private string _conURL;

    public int UserID { get { return _userID; } }
    public int GameID { get { return _gameID; } }
    public string Username { get { return _username; } }
    public int Gametime { get { return _gametime; } }
    public string ConURL { get { return _conURL; } }

    void Awake() {
		string[] arguments = Environment.GetCommandLineArgs();
		_userID = Convert.ToInt32(arguments[2]);
		_gameID = Convert.ToInt32(arguments[3]);
		_username = arguments[4];
		_gametime = Convert.ToInt32(arguments[5]);
		_conURL = arguments[6];

        _userID = 2;
        _gameID = 6;
        _username = "YVONNE";
        _gametime = 10;
        _conURL = "http://www.serellyn.net/HEIM/php/";

    }
}
