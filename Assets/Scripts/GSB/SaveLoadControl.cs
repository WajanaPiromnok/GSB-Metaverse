using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadControl : MonoBehaviour
{
    public GameObject PlayerMp;
    public GameObject PlayerFp;

    public CharacterEditor characterEditorMan;
    public CharacterEditor characterEditorWoman;

    [SerializeField] bool _showPlayerMp = false;
    [SerializeField] bool _showPlayerFp = false;

    private void Update()
    {
        if (PlayerMp.activeSelf)
        {
            _showPlayerMp = true;
            _showPlayerFp = false;
        }
        
        if(PlayerFp.activeSelf)
        {
            _showPlayerMp = false;
            _showPlayerFp = true;
        }

        if (_showPlayerMp == true)
        {
            characterEditorMan.enabled = true;
            characterEditorWoman.enabled = false;
        }

        if (_showPlayerFp == true)
        {
            characterEditorMan.enabled = false;
            characterEditorWoman.enabled = true;
        }
    }
}
