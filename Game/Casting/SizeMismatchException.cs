using System;
class SizeMismatchException : Exception
{
    public SizeMismatchException(int size1, int size2) : base ($"Function requires equal size parameters, {size1} : {size2} are not equal.")
    {

    }
}