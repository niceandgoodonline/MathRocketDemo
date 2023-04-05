using System.Collections.Generic;
using UnityEngine;

public class Calculator
{
    public float Calculate(string s)
    {
        float        _result     = 0;
        Stack<float> _stack      = new Stack<float>();
        float        _currentNum = 0;
        char         _operator   = '+';
        
        for(int i = 0; i < s.Length; i++)
        {
            char _currentChar = s[i];
            if (char.IsDigit(_currentChar))
            {
                _currentNum = _currentNum * 10 + float.Parse(_currentChar.ToString());
            }
            if(i == s.Length - 1 || !char.IsDigit(_currentChar) && !char.IsWhiteSpace(_currentChar))
            {
                if(_operator == '+')
                {
                    _stack.Push(_currentNum);
                }
                else if (_operator == '-')
                {
                    _stack.Push(-_currentNum);
                }
                else if (_operator == '*')
                {
                    _stack.Push( _stack.Pop()*_currentNum);
                }
                else if (_operator == '/')
                {
                    _stack.Push(_stack.Pop() / _currentNum);
                }
                _operator = _currentChar;
                _currentNum = 0;
            }
        }
        int _count = _stack.Count;
        for (int i = 0; i < _count; i++)
            _result += _stack.Pop();
        return _result;
    }
}
