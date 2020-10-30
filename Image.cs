   using System;
using System.Drawing;
//Followed tutorial from  https://medium.com/@jagathprasad0/creating-convolutional-neural-network-from-scratch-using-c-fa72b497226b
namespace ImageConvolution
{
    class Image
    {
        
         public void Train()
        {
            Bitmap img = new Bitmap(@"image.jpeg.jpg");
            double [,,] pixelvalues = new double [3, img.Width, img.Height];

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    pixelvalues [0, i, j] = pixel.R;
                    pixelvalues [1, i, j] = pixel.G;
                    pixelvalues [2, i, j] = pixel.B;
                   
                }
            }

            dynamic output = this.Filter(1,2,3);

            output = this.ConvolutionLayer(pixelvalues, output);
        
            output = this.ActivationLayer(output);
            
            output = this.MaxPoolingLayer(output, 2);

            output = this.FlatternLayer(output);

            double[] weights = this.RandomWeights(output.Length);
            output = this.FullyConnectedLayer(output, weights);
        }

        public double [,,] ConvolutionLayer(double[,,] input, double [,,,] filter)
        {
           double[,,] output = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];
           try{
               for (int i = 0; filter.GetLength(0); i++)
               {
                   for (int j = 0; j < input.GetLength(0); j++)
                   {
                       for (int k = 0; k<input.GetLength(1); k++)
                       {
                           for (int l = 0; l<input.GetLength(2); l++)
                           {
                               output[j,k,l]= input[j,k,l]*filter[i,j,k,l];
                           }
                       }
                   }
               }
           }
           catch (Exception ex)
           {

           }
        }

          public double [,,] ActivationLayer(double[,,] input)
        {
           double[,,] output = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];
           try{
            
                   for (int j = 0; j < input.GetLength(0); j++)
                   {
                       for (int k = 0; k<input.GetLength(1); k++)
                       {
                           for (int l = 0; l<input.GetLength(2); l++)
                           {
                               output[j,k,l]= input[j,k,l]*input[j,k,l];
                           }
                       }
                   }
               
           }
           catch (Exception ex)
           {
               
           }
           return output;
    }
     public double [,,] MaxPoolingLayer(double[,,] input, int filtersize)
        {

            double[,,] output = null;

            try{
                var newHeight = ((input.GetLength(1) - filtersize) / 2) + 1;
                var newWidth = ((input.GetLength(2) - filtersize) / 2) + 1;

                output = new double[input.GetLength(0), newHeight, newWidth];
                 for (int j = 0; j < input.GetLength(0); j++)
                   {

                       var current_y = 0; var out_y = 0;
                       for (int k = current_y*filtersize; k < input.GetLength(1); k++)
                       {
                           var current_x = 0; var out_x=0;
                           var rowValue = input[j,k, 0] * newWidth + input[j,k, 0];
                           for (int l = current_x+filtersize; l < input.GetLength(2); l++)
                           {
                               var columnValue = input[j,k,l] * newHeight + input[j,k,l];
                               double maxValue = MaxValue(input, j, k, l, filtersize);
                               output[j, out_y, out_x] = input[j,k,l] > maxValue ? input[j, k, l]:maxValue;
                               current_x = current_x+2;
                               out_x = out_x + 1;  
                           }

                           current_y = current_y + 2;
                           out_y = out_y + 1;

                       }
            }}
           
           catch (Exception ex)
           {
               
           }
           return output;
        }

           public double[] FlatternLayer(double[,,] input)
           {
               int rgbChannel = input.GetLength(0);
               int rowPixel = input.GetLength(1);
               int columnPixel = input.GetLength(2);
               int length = rgbChannel * rowPixel * columnPixel;
               double[] output = new double[length];

               try{
                   int count = 0;
                   for (int i = 0; i < rgbChannel; i++)
                   {
                     for (int j = 0; j < rowPixel; j++)
                    {
                        for (int k=0; k<columnPixel; k++)
                        {
                            output[count] = input[i,j,k];
                            count = count+1;
                        }
                    }
                   }}

                     catch (Exception ex)
           {
               
           }
           return output;
               }
           }