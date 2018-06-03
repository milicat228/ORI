using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    public class SumNode
    {
        //ovo je cvor sabirac, a x je lista ulaza
        private List<double> x;

        public SumNode()
        {
            x = new List<double>();
        }
        /// <summary>
        /// Suma svih elemenata
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double forward(List<double> x)
        {
            //forward - propagacija unapred, ako je na ulazu x, sta je na izlazu
            this.x = x;
            //TODO 1: implementirati forward funckiju za sum node
            double sum_retVal = 0.0;
            foreach(double value in x)
            {
                sum_retVal += value ;
            }
            return sum_retVal;
        }
        /// <summary>
        /// Izvod funkcije po svakom elementu
        /// z = x + y 
        /// dz/dx = 1 => dx = dz
        /// dz/dy = 1 => dy = dz
        /// </summary>
        /// <param name="dz"></param>
        /// <returns></returns>
        public List<double> backward(double dz)
        {
            //TODO 2: implementirati backward funkciju za sum node
            //ako je na izlazu promena dz, kolika je bila promena svakog od ulaza
            List<double> retVal = new List<double>();
            for(int i = 0; i < x.Count; ++i)
            {
                retVal.Add(dz);
            }

            return retVal;
        }
    }
}
