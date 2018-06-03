using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRegression
{
    //https://msdn.microsoft.com/en-us/magazine/dn913188.aspx
    public class MultiRegression
    {        
        //coef ce ici redom kao polinom na papiru
        private double[] coef;        
        private Random random = new Random();

        public void Train(double[][] X, int maxEpochs, double alpha)
        {
            //random init coefs
            coef = new double[X[0].Length];
            for(int i = 0; i < coef.Length; ++i)
            {
                coef[i] = random.NextDouble();                
            }

            int current = 0;
            while(current < maxEpochs)
            {                
                ++current;                
                for (int i = 0; i < X.Length; ++i)  // Accumulate
                {
                    double[] accumulatedGradients = new double[coef.Length];
                    double computed = ComputeOutput(X[i]);
                    int targetIndex = X[i].Length - 1;
                    double target = X[i][targetIndex];
                    accumulatedGradients[0] += (target - computed) * 1; // For b0
                    for (int j = 1; j < coef.Length; ++j)
                        accumulatedGradients[j] += (target - computed) * X[i][j - 1];

                    for (int j = 0; j < coef.Length; ++j)
                        coef[j] += alpha * accumulatedGradients[j];
                }
            }
            
        }

        public double ComputeOutput(double[] dataItem)
        {
            double z = 0.0;
            z += coef[0]; // Add b0 constant
            for (int i = 0; i < coef.Length - 1; ++i)
                z += (coef[i + 1] * dataItem[i]); // Skip b0
            return z;
            //return 1.0 / (1.0 + Math.Exp(-z));
        }
    }
}
