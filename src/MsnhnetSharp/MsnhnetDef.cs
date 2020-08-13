using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MsnhnetSharp
{
    public class MsnhnetDef
    {
        public struct Vec3
        {
           public int x;
           public int y;
           public int z;

            public Vec3(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        public struct Dim
        {
            public int width;
            public int height;
            public int channel;
        }

        public enum PredDataType
        {
            PRE_DATA_NONE = 0,
            PRE_DATA_FC32_C1,
            PRE_DATA_FC32_C3,
            PRE_DATA_GOOGLENET_FC3,
            PRE_DATA_PADDING_ZERO_FC3,
            PRE_DATA_TRANSFORMED_FC3,
            PRE_DATA_CAFFE_FC3
        };

        static public List<Vec3> colors = new List<Vec3>()
        {
        new Vec3(0  , 0   ,200), new Vec3(0  , 200,   0), new Vec3(200 , 0 ,   0),
        new Vec3(0  , 255 ,200), new Vec3(255, 200,   0), new Vec3(200 , 0 , 255),
        new Vec3(50 , 0   ,200), new Vec3(50 , 200,   0), new Vec3(200 , 50,  50),
        new Vec3(50 , 255 ,200), new Vec3(255, 200,  50), new Vec3(200 , 50, 255),
        new Vec3(100, 0   ,200), new Vec3(100, 200,   0), new Vec3(200 ,100,  50),
        new Vec3(100, 255 ,200), new Vec3(255, 200, 100), new Vec3(200 ,100, 255),
        new Vec3(150, 0   ,200), new Vec3(150, 200,   0), new Vec3(200 ,150,  50),
        new Vec3(150, 255 ,200), new Vec3(255, 200, 150), new Vec3(200 ,150, 255),
        new Vec3(200, 0   ,200), new Vec3(200, 200,   0), new Vec3(200 ,200,  50),
        new Vec3(200, 255 ,200), new Vec3(255, 200, 200), new Vec3(200 ,200, 255),
        new Vec3(0  , 0   ,150), new Vec3(0  , 150,   0), new Vec3(150 , 0 ,   0),
        new Vec3(0  , 255 ,150), new Vec3(255, 150,   0), new Vec3(150 , 0 , 255),
        new Vec3(50 , 0   ,150), new Vec3(50 , 150,   0), new Vec3(150 , 50,  50),
        new Vec3(50 , 255 ,150), new Vec3(255, 150,  50), new Vec3(150 , 50, 255),
        new Vec3(100, 0   ,150), new Vec3(100, 150,   0), new Vec3(150 ,100,  50),
        new Vec3(100, 255 ,150), new Vec3(255, 150, 100), new Vec3(150 ,100, 255),
        new Vec3(150, 0   ,150), new Vec3(150, 150,   0), new Vec3(150 ,150,  50),
        new Vec3(150, 255 ,150), new Vec3(255, 150, 150), new Vec3(150 ,150, 255),
        new Vec3(200, 0   ,150), new Vec3(200, 150,   0), new Vec3(150 ,200,  50),
        new Vec3(200, 255 ,150), new Vec3(255, 150, 200), new Vec3(150 ,200, 255),
        new Vec3(0  , 0   ,255), new Vec3(0  , 255,   0), new Vec3(255 , 0 ,   0),
        new Vec3(0  , 255 ,255), new Vec3(255, 255,   0), new Vec3(255 , 0 , 255),
        new Vec3(50 , 0   ,255), new Vec3(50 , 255,   0), new Vec3(255 , 50,  50),
        new Vec3(50 , 255 ,255), new Vec3(255, 255,  50), new Vec3(255 , 50, 255),
        new Vec3(100, 0   ,255), new Vec3(100, 255,   0), new Vec3(255 ,100,  50),
        new Vec3(100, 255 ,255), new Vec3(255, 255, 100), new Vec3(255 ,100, 255),
        new Vec3(150, 0   ,255), new Vec3(150, 255,   0), new Vec3(255 ,150,  50),
        new Vec3(150, 255 ,255), new Vec3(255, 255, 150), new Vec3(255 ,150, 255),
        new Vec3(200, 0   ,255), new Vec3(200, 255,   0), new Vec3(255 ,200,  50),
        new Vec3(200, 255 ,255), new Vec3(255, 255, 200), new Vec3(255 ,200, 255),
        };
    }

}
