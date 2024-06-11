using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VolumeRendering
{
    internal class Asset
    {
        private string assetFilename = "assets/head-181x217x181.dat";  // assets/vertebra-47x512x512.dat / assets/head-181x217x181.dat
        public Vector origin = new Vector(0, 0, 0);
        public static int lengthOnXAxis = 181; //47 / 181
        public static int lengthOnYAxis = 217; //512 / 217
        public static int lengthOnZAxis = 181; //512 / 181

        public int[ , , ] AssetMatrix { get; } = new int[lengthOnXAxis, lengthOnYAxis, lengthOnZAxis];

        public Asset()
        {
            readAsset();
        }

        public int getValueFromPosition(Vector position)
        {
            if(!isPointInsideAsset(position))
            {
                return 0;
            }

            int distanceFromAssetOriginOnX = Convert.ToInt32(Math.Abs(position.X - origin.X));
            int distanceFromAssetOriginOnY = Convert.ToInt32(Math.Abs(position.Y - origin.Y));
            int distanceFromAssetOriginOnZ = Convert.ToInt32(Math.Abs(position.Z - origin.Z));

            /*Console.WriteLine($"X: {distanceFromAssetOriginOnX}, Y: {distanceFromAssetOriginOnY}, Z: {distanceFromAssetOriginOnZ}");*/

            return AssetMatrix[distanceFromAssetOriginOnX, distanceFromAssetOriginOnY, distanceFromAssetOriginOnZ];

        }

        public bool isPointInsideAsset(Vector positionOfPoint)
        {
            return positionOfPoint.X > origin.X && positionOfPoint.X < origin.X + lengthOnXAxis - 1 &&
                positionOfPoint.Y > origin.Y && positionOfPoint.Y < origin.Y + lengthOnYAxis - 1 &&
                positionOfPoint.Z > origin.Z && positionOfPoint.Z < origin.Z + lengthOnZAxis - 1;
                
        }

        private void readAsset()
        {
            using (FileStream fileStream = new FileStream(assetFilename, FileMode.Open))
            {
                for (int i = 0; i < lengthOnXAxis; i++)
                {
                    for (int j = 0; j < lengthOnYAxis; j++)
                    {
                        for (int z = 0; z < lengthOnZAxis; z++)
                        {
                            int data = fileStream.ReadByte();
                            if (data == -1)
                            {
                                Console.WriteLine("Cannot Read file!");
                                return;
                            }
                            AssetMatrix[i, j, z] = data;
                        }
                    }
                }
            }
        }

        public double getCenterX()
        {
            return (origin.X + lengthOnXAxis) / 2;
        }

        public double getCenterY()
        {
            return (origin.Y + lengthOnYAxis) / 2;
        }

        public double getCenterZ()
        {
            return (origin.Z + lengthOnZAxis) / 2;
        }
    }
}
