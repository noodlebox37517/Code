using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Code.UI
{
    public class PadlockUI : MonoBehaviour
    {

        [SerializeField] private Padlock _padlock;
        [SerializeField] private TextMeshProUGUI _combinationText;
        private bool _isActive = false;
        private int[] _rollerNumbers = new int[] {0,0,0,0};
        private int rollerIndex = 0;
        private int _code = 9999;

        private void Start()
        {
            _code = GameManager.instance.gameData.Code;
        }

        private void Update() 
        {
            if (_isActive)
            {
                var input = GetInput();
                if (input != null)
                {
                    AddNumber((int)input);
                }
            }
        }
        
        public void Active(bool isactive)
        {
            gameObject.SetActive(isactive);
            _isActive = isactive;
        }
        private void AddNumber(int number)
        {
            _rollerNumbers[rollerIndex] = number;
            rollerIndex++;
            CheckCode();
            if (rollerIndex > _rollerNumbers.Length - 1)
            {
                rollerIndex =0;
                _rollerNumbers = new int[] {0,0,0,0};
            }
        }
        
        private void CheckCode()
        {
            var inputcode = string.Join("", _rollerNumbers);
            _combinationText.text = inputcode;
            if (inputcode == _code.ToString())
            {
                _padlock.UnBlock();
            }
        }

        
        //This is cursed, I strapped for time :(
        private int? GetInput()
        {
            if(Input.GetKeyDown("0"))
            {
                return 0;
            }
            if(Input.GetKeyDown("1"))
            {
                return 1;
            }
            if(Input.GetKeyDown("2"))
            {
                return 2;
            }
            if(Input.GetKeyDown("3"))
            {
                return 3;
            }
            if(Input.GetKeyDown("4"))
            {
                return 4;
            }
            if(Input.GetKeyDown("5"))
            {
                return 5;
            }
            if(Input.GetKeyDown("6"))
            {
                return 6;
            }
            if(Input.GetKeyDown("7"))
            {
                return 7;
            }
            if(Input.GetKeyDown("8"))
            {
                return 8;
            }
            if(Input.GetKeyDown("9"))
            {
                return 9;
            }

            return null;
        }
    }
}

