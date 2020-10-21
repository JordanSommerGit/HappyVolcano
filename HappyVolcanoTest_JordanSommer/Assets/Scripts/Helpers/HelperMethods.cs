using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMethods
{
    public static int Get1DIndex(int index2D, int nrRows, int nrCols)
    {
        return nrRows * index2D + nrCols;
    }
}
