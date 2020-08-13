using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using static MsnhnetSharp.MsnhnetDef;

namespace MsnhnetSharp
{
    public class Msnhnet
    {
        const string MsnhnetLib = "msnhnet.dll";

        private const int MaxBBoxNum = 1024;

        [DllImport(MsnhnetLib, EntryPoint = "initMsnhnet")]
        static extern int _initMsnhnet();

        [DllImport(MsnhnetLib, EntryPoint = "dispose")]
        static extern int _dispose();

        [DllImport(MsnhnetLib, EntryPoint = "withGPU")]
        static extern int _withGPU(ref int GPU);

        [DllImport(MsnhnetLib, EntryPoint = "withCUDNN")]
        static extern int _withCUDNN(ref int CUDNN);

        [DllImport(MsnhnetLib, EntryPoint = "getInputDim")]
        static extern int _getInputDim(ref int width, ref int heigth, ref int channel);

        [DllImport(MsnhnetLib, EntryPoint = "getCpuForwardTime")]
        static extern int _getCpuForwardTime(ref float cpuForwardTime);

        [DllImport(MsnhnetLib, EntryPoint = "getGpuForwardTime")]
        static extern int _getGpuForwardTime(ref float getGpuForwardTime);

        [DllImport(MsnhnetLib, EntryPoint = "buildMsnhnet")]
        static extern int _buildMsnhnet(ref IntPtr msg, string msnhnet, string msnhbin, int useFp16, int useCudaOnly);

        [DllImport(MsnhnetLib, EntryPoint = "runClassifyFile")]
        static unsafe extern int _runClassifyFile(ref IntPtr msg, string imagePath, ref int bestIndex, PredDataType predDataType,
                                                 int runGPU, float* mean, float* std);

        [DllImport(MsnhnetLib, EntryPoint = "runClassifyList")]
        static unsafe extern int _runClassifyList(ref IntPtr msg, byte* data, int width, int height, int channel, ref int bestIndex, PredDataType predDataType,
                                                  int swapRGB, int runGPU, float* mean, float* std);

        [DllImport(MsnhnetLib, EntryPoint = "runClassifyNoPred")]
        static unsafe extern int _runClassifyNoPred(ref IntPtr msg, float* data, int len, ref int bestIndex, int runGPU);

        [StructLayout(LayoutKind.Sequential)]
        public struct BBox
        {
            public float x;
            public float y;
            public float w;
            public float h;
            public float conf;
            public float bestClsConf;
            public UInt32 bestClsIdx;
            public float angle;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct BBoxContainer
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxBBoxNum)]
            public BBox[] boxes;
        }

        [DllImport(MsnhnetLib, EntryPoint = "runYoloFile")]
        static extern int _runYoloFile(ref IntPtr msg, string imagePath, ref BBoxContainer bboxContainer, ref int detectedNum, int runGPU);

        [DllImport(MsnhnetLib, EntryPoint = "runYoloList")]
        static unsafe extern int _runYoloList(ref IntPtr msg, byte* data, int width, int height, int channel, ref BBoxContainer bboxContainer, ref int detectedNum, int swapRGB, int runGPU);

        private bool netBuilt = false;
        private bool netInited = false;

        /// <summary>
        /// check GPU 
        /// </summary>
        /// <returns></returns>
        static public bool WithGPU()
        {
            int GPU = 0;
            _withGPU(ref GPU);
            return (GPU == 1) ? true : false;
        }

        /// <summary>
        /// check CUDNN
        /// </summary>
        /// <returns></returns>
        static public bool WithCudnn()
        {
            int CUDNN = 0;
            _withCUDNN(ref CUDNN);
            return (CUDNN == 1) ? true : false;
        }

        /// <summary>
        /// Get input dim
        /// </summary>
        /// <returns></returns>
        public Dim GetInputDim()
        {
            if (netBuilt)
            {
                int width = 0;
                int height = 0;
                int channel = 0;
                _getInputDim(ref width, ref height, ref channel);

                Dim dim;
                dim.width = width;
                dim.height = height;
                dim.channel = channel;

                return dim;
            }
            else
            {
                throw new Exception("Net wasn't built yet");
            }

        }

        /// <summary>
        /// Get cpu forward time
        /// </summary>
        /// <returns></returns>
        public float GetCpuForwardTime()
        {
            float time = 0;
            _getCpuForwardTime(ref time);
            return time;
        }

        /// <summary>
        /// Get gpu forward time
        /// </summary>
        /// <returns></returns>
        public float GetGpuForwardTime()
        {
            float time = 0;
            if (_getGpuForwardTime(ref time) != 1)
            {
                throw new Exception("Msnhnet is not compiled with GPU mode!");
            }
            return time;
        }

        /// <summary>
        /// init net
        /// </summary>
        public void InitNet()
        {
            if (_initMsnhnet() != 1)
            {
                throw new Exception("Init net Failed");
            }
            netInited = true;
        }

        /// <summary>
        /// build net
        /// </summary>
        /// <param name="msnhnet">msnhnet file path</param>
        /// <param name="msnhbin">msnhbin file path</param>
        /// <param name="useFp16">use fp16 or not</param>
        /// <param name="useCudaOnly">build with cudnn, but only use cuda</param>
        public void BuildNet(string msnhnet, string msnhbin, bool useFp16, bool useCudaOnly)
        {
            if (!netInited)
            {
                throw new Exception("Net wasn't inited yet");
            }

            IntPtr msg = new IntPtr();
            if (_buildMsnhnet(ref msg, msnhnet, msnhbin, useFp16 ? 1 : 0, useCudaOnly ? 1 : 0) != 1)
            {
                string mstr = Marshal.PtrToStringAnsi(msg);
                throw new Exception(mstr);
            }
            netBuilt = true;
        }

        /// <summary>
        /// dispose net
        /// </summary>
        public void Dispose()
        {
            _dispose();
        }

        /// <summary>
        /// forward net with image file, with preprocess
        /// </summary>
        /// <param name="imagePath">image file path</param>
        /// <param name="predDataType">process function</param>
        /// <param name="runGPU">run wit GPU</param>
        /// <param name="mean"> if normalize mean val </param>
        /// <param name="std">if normalize std val</param>
        /// <returns></returns>
        public int RunClassifyFile(string imagePath, PredDataType predDataType, bool runGPU, float[] mean = null, float[] std = null)
        {
            if (!netBuilt)
            {
                throw new Exception("Net wasn't built yet");
            }

            IntPtr msg = new IntPtr();
            int bestIndex = 0;
            unsafe
            {
                if (predDataType == PredDataType.PRE_DATA_TRANSFORMED_FC3)
                {
                    fixed (float* meanPtr = mean)
                    {
                        fixed (float* stdPtr = std)
                        {
                            if (_runClassifyFile(ref msg, imagePath, ref bestIndex, predDataType, runGPU ? 1 : 0, meanPtr, stdPtr) != 1)
                            {
                                string mstr = Marshal.PtrToStringAnsi(msg);
                                throw new Exception(mstr);
                            }
                        }

                    }
                }
                else
                {
                    if (_runClassifyFile(ref msg, imagePath, ref bestIndex, predDataType, runGPU ? 1 : 0, null, null) != 1)
                    {
                        string mstr = Marshal.PtrToStringAnsi(msg);
                        throw new Exception(mstr);
                    }
                }
            }

            return bestIndex;
        }

        /// <summary>
        ///  forward net with image BitmapData, with preprocess
        /// </summary>
        /// <param name="bitmap">data</param>
        /// <param name="predDataType">process function</param>
        /// <param name="swapRGB">net swap RGB or not</param>
        /// <param name="runGPU">run wit GPU</param>
        /// <param name="mean"> if normalize mean val </param>
        /// <param name="std">if normalize std val</param>
        /// <returns></returns>
        public int RunClassifyList(BitmapData bitmap, PredDataType predDataType, bool swapRGB, bool runGPU, float[] mean = null, float[] std = null)
        {
            if (!netBuilt)
            {
                throw new Exception("Net wasn't built yet");
            }
            IntPtr msg = new IntPtr();
            int bestIndex = 0;
            unsafe
            {
                if (predDataType == PredDataType.PRE_DATA_TRANSFORMED_FC3)
                {
                    fixed (float* meanPtr = mean)
                    {
                        fixed (float* stdPtr = std)
                        {
                            if (_runClassifyList(ref msg, (byte*)bitmap.Scan0, bitmap.Width, bitmap.Height, bitmap.Stride / bitmap.Width, ref bestIndex, predDataType, swapRGB ? 1 : 0, runGPU ? 1 : 0, meanPtr, stdPtr) != 1)
                            {
                                string mstr = Marshal.PtrToStringAnsi(msg);
                                throw new Exception(mstr);
                            }
                        }

                    }
                }
                else
                {
                    if (_runClassifyList(ref msg, (byte*)bitmap.Scan0, bitmap.Width, bitmap.Height, bitmap.Stride / bitmap.Width, ref bestIndex, predDataType, swapRGB ? 1 : 0, runGPU ? 1 : 0, null, null) != 1)
                    {
                        string mstr = Marshal.PtrToStringAnsi(msg);
                        throw new Exception(mstr);
                    }
                }
            }

            return bestIndex;
        }

        /// <summary>
        /// forward net with float data, without preprocess
        /// </summary>
        /// <param name="data">flaot data</param>
        /// <param name="runGPU">run wit GPU</param>
        /// <returns></returns>
        public int RunClassifyNoPred(float[] data, bool runGPU)
        {
            if (!netBuilt)
            {
                throw new Exception("Net wasn't built yet");
            }
            IntPtr msg = new IntPtr();
            int bestIndex = 0;
            unsafe
            {
                fixed (float* dataPtr = data)
                {
                    if (_runClassifyNoPred(ref msg, dataPtr, data.Length, ref bestIndex, runGPU ? 1 : 0) != 1)
                    {
                        string mstr = Marshal.PtrToStringAnsi(msg);
                        throw new Exception(mstr);
                    }
                }
            }

            return bestIndex;
        }

        /// <summary>
        /// run yolo
        /// </summary>
        /// <param name="imagePath">image path</param>
        /// <param name="runGPU">run with GPU</param>
        /// <returns></returns>
        public List<BBox> RunYoloFile(string imagePath, bool runGPU)
        {
            List<BBox> bboxVec = new List<BBox>();
            IntPtr msg = new IntPtr();
            BBoxContainer bboxContainer = new BBoxContainer();
            int detectNum = 0;

            if (!netBuilt)
            {
                throw new Exception("Net wasn't built yet");
            }

            if (_runYoloFile(ref msg, imagePath, ref bboxContainer, ref detectNum, runGPU?1:0)!=1)
            {
                string mstr = Marshal.PtrToStringAnsi(msg);
                throw new Exception(mstr);
            }

            for (int i = 0; i < detectNum; i++)
            {
                bboxVec.Add(bboxContainer.boxes[i]);
            }

            return bboxVec;
        }

        public List<BBox> RunYoloList(BitmapData bitmap, bool swapRGB, bool runGPU)
        {
            if (!netBuilt)
            {
                throw new Exception("Net wasn't built yet");
            }
            List<BBox> bboxVec = new List<BBox>();
            IntPtr msg = new IntPtr();
            BBoxContainer bboxContainer = new BBoxContainer();
            int detectNum = 0;

            unsafe
            {
                if (_runYoloList(ref msg, (byte*)bitmap.Scan0, bitmap.Width, bitmap.Height, bitmap.Stride / bitmap.Width, ref bboxContainer, ref detectNum, swapRGB?1:0, runGPU?1:0) != 1)
                {
                    string mstr = Marshal.PtrToStringAnsi(msg);
                    throw new Exception(mstr);
                }
            }

            for (int i = 0; i < detectNum; i++)
            {
                bboxVec.Add(bboxContainer.boxes[i]);
            }

            return bboxVec;
        }
    }
}
