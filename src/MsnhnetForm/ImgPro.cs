using MsnhnetSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace MsnhnetForm
{
    public class ImgPro
    {
        static public Bitmap ConvertTo24bpp(Image img)
        {
            if(img==null)
            {
                throw new Exception("Image empty");
            }
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        public static bool ReSize(Bitmap srcBmp, int outWidth, int outHeight, out Bitmap dstBmp, bool useBilinear)
        {
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
  
            if ((outWidth == srcBmp.Width) && outHeight == srcBmp.Height)
            {
                dstBmp = new Bitmap(srcBmp);
                return true;
            }

            double ratioH = 1.0 * outHeight / srcBmp.Height;
            double ratioW = 1.0 * outWidth/ srcBmp.Width;

            dstBmp = new Bitmap(outWidth, outHeight);

            BitmapData srcBmpData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData dstBmpData = dstBmp.LockBits(new Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* srcPtr = null;
                byte* dstPtr = null;
                int srcI = 0;
                int srcJ = 0;
                double srcdI = 0;
                double srcdJ = 0;
                double a = 0;
                double b = 0;
                double F1 = 0;
                double F2 = 0;
                if (!useBilinear)
                {
                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcI = (int)(i / ratioH);
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            dstPtr[j * 3] = srcPtr[(int)(j / ratioW) * 3];
                            dstPtr[j * 3 + 1] = srcPtr[(int)(j / ratioW) * 3 + 1];
                            dstPtr[j * 3 + 2] = srcPtr[(int)(j / ratioW) * 3 + 2];
                        }
                    }
                }
                else 
                {
                    byte* srcPtrNext = null;
                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcdI = i / ratioH;
                        srcI = (int)srcdI;
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;
                        srcPtrNext = (byte*)srcBmpData.Scan0 + (srcI + 1) * srcBmpData.Stride;
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            srcdJ = j / ratioW;
                            srcJ = (int)srcdJ;
                            if (srcdJ < 1 || srcdJ > srcBmp.Width - 1 || srcdI < 1 || srcdI > srcBmp.Height - 1)
                            {
                                dstPtr[j * 3] = 255;
                                dstPtr[j * 3 + 1] = 255;
                                dstPtr[j * 3 + 2] = 255;
                                continue;
                            }
                            a = srcdI - srcI;
                            b = srcdJ - srcJ;
                            for (int k = 0; k < 3; k++)
                            {
                                //f(i+p,j+q)=(1-p)(1-q)f(i,j)+(1-p)qf(i,j+1)+p(1-q)f(i+1,j)+pqf(i+1, j + 1)
                                F1 = (1 - b) * srcPtr[srcJ * 3 + k] + b * srcPtr[(srcJ + 1) * 3 + k];
                                F2 = (1 - b) * srcPtrNext[srcJ * 3 + k] + b * srcPtrNext[(srcJ + 1) * 3 + k];
                                dstPtr[j * 3 + k] = (byte)((1 - a) * F1 + a * F2);
                            }
                        }
                    }
                }
            }
            srcBmp.UnlockBits(srcBmpData);
            dstBmp.UnlockBits(dstBmpData);
            return true;
        }

        public static Msnhnet.BBox bboxResize2Org(Msnhnet.BBox box, int curW, int curH, int orgW, int orgH)
        {

            if (orgW > orgH)
            {
                float scaledRatio = 1.0f * curW / orgW;
                int padUp = (int)((curH - orgH * scaledRatio) / 2);
                box.x = box.x / scaledRatio;
                box.w = box.w / scaledRatio;
                box.y = (box.y - padUp) / scaledRatio;
                box.h = box.h / scaledRatio;
            }
            else
            {
                float scaledRatio = 1.0f * curH / orgH;
                int padLeft = (int)((curW - orgW * scaledRatio) / 2);
                box.x = (box.x - padLeft) / scaledRatio;
                box.w = box.w / scaledRatio;
                box.y = box.y / scaledRatio;
                box.h = box.h / scaledRatio;

            }
            return box;
        }
    }
}
