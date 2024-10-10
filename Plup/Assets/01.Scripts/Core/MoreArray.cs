using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Serializable2DArray<T>
{
    private List<List<T>> _data = new List<List<T>>();
    public int rows => _data.Count;
    public int cols => _data[0].Count;

    public Serializable2DArray(int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            List<T> row = new List<T>(new T[columns]);
            _data.Add(row);
        }
    }

    public Serializable2DArray(T[,] originalArray)
    {
        _data = Enumerable.Range(0, originalArray.GetLength(0))
                         .Select(i => Enumerable.Range(0, originalArray.GetLength(1))
                                                .Select(j => originalArray[i, j])
                                                .ToList())
                         .ToList();

        Debug.Log(_data.Count);
    }

    public T this[int row, int col]
    {
        get => _data[row][col];
        set => _data[row][col] = value;
    }
}
