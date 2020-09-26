/*
 *  (c) Copyright 2020 Warwick Molloy
 *  Provided under the MIT License
 */

using UnityEngine;

[System.Serializable]
public struct SizeParams
{
    /// <summary>
    /// Number of rows for the scene
    /// </summary>
    [Tooltip("How tall is the grid?")]
    public int NumberOfRows;

    /// <summary>
    /// Number of coluns that make up a row.
    /// </summary>
    [Tooltip("How wide is the grid?")]
    public int NumberPerRow;

    [Tooltip("How tall is each row in Unity display units?")]
    public float RowHeight;

    [Tooltip("How wide is each column in Unity display units?")]
    public float ColumnWidth;

    public static string ToString(SizeParams sizes)
    {
        return $"SizeParams: Num / row: {sizes.NumberPerRow}  Num of Rows: {sizes.NumberOfRows}";
    }
}
