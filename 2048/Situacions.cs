using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    /// <summary>
    /// Aquesta classe conté diferents situacions de joc que poden ser útils per debugar.
    /// </summary>
    class Situacions
    {
        private static int NUM_SITUACIONS = 6;
        private static int[][,] ArraySituacions = new int[NUM_SITUACIONS][,];

        static Situacions()
        {
            ArraySituacions[0] = new int[,] {
                { 0,0,0,0 },
                { 0,0,0,0 },
                { 0,0,0,0 },
                { 0,0,0,0 } };
            ArraySituacions[1] = new int[,] {
				{4,0 ,0 ,0 },
			    {4,0 ,0 ,0 },
			    {8,2 ,2 ,0 },
			    {4,16,32,0 } };
            ArraySituacions[2] = new int[,] {
                {256,1024,512,256 },
                {512,1024,256,128 },
                {128,512 ,64 ,32  },
                {64 ,16  ,32 ,128 } };
            ArraySituacions[3] = new int[,] {
                {256,1024,512,256 },
                {512,64  ,256,128 },
                {128,512 ,64 ,32  },
                {64 ,16  ,32 ,128 } };
            ArraySituacions[4] = new int[,] {
                {4,0,2 ,0  },
                {0,0,0 ,0  },
                {2,0,2 ,0  },
                {0,4,16,16 } };
            ArraySituacions[5] = new int[,] {
                {0,2,0,2  },
                {0,0,0,2  },
                {0,0,8,2  },
                {0,2,4,32 } };
        }

        public static void EstabelixSituacio(Bloc[,] bl, int sit)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    bl[y, x].Valor = ArraySituacions[sit][y, x];
                }
            }
        }
    }
}
