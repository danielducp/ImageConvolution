using System;
using System.IO;

namespace ImageConvolution
{
    class Images
    {

        public void CalculatingImage()
        {

           
            int[,] initialimage = new int[4,4] { { 128, 255, 196, 64 }, { 20, 128, 255, 96 }, { 210, 20, 128, 255 }, { 210, 210, 20, 128 } };
            int[,] kernel = new int[3,3] { { 1, 2, 1 }, { 0, 0, 0},  { -1, -2, -1} };
            string[,] result = new string [4,4];


        for (int col = 0; col  < initialimage.GetLength(0); col++)
        {
                for (int row = 0; col  < initialimage.GetLength(1); row++)
                {
                        if(col - 1 <0 || col +1 == initialimage.GetLength(0)||row-1<0||row+1==initialimage.GetLength(1))
                        {
                                result[col, row] = "0";
                        }
                        else{
                                result[col,row] = ((initialimage[col-1, row - 1] * kernel[0,0])+
                                (initialimage[col,row - 1] * kernel [1, 0])+
                                (initialimage [col+1, row-1]*kernel[2,0])+
                                (initialimage [col-1, row]*kernel[0,1])+
                                (initialimage [col, row]*kernel[1,1])+
                                (initialimage [col+1, row]*kernel[2,1])+
                                (initialimage [col-1, row+1]*kernel[0,2])+
                                (initialimage [col, row+1]*kernel[1,2])+
                                (initialimage [col+1, row+1]*kernel[2,2])).ToString();
                            }
                    }

        }
        }
}
}
           