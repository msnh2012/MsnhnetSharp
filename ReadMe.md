# MsnhnetSharp
---
**C# wrapper for msnhnet.**
### Msnhnet
https://github.com/msnh2012/Msnhnet
### Prebuild Msnhnet
- **msnhnet_cpu_avx**(CPU Avx needed)</br>
- https://github.com/msnh2012/MsnhnetSharp/blob/master/prebuild/msnhnet_cpu_avx.7z
- **msnhnet_gpu_cuda10_1_cudnn7_6**(CPU Avx, GPU Cuda10.1 and Cudnn7.6 needed)</br>
- https://github.com/msnh2012/MsnhnetSharp/blob/master/prebuild/msnhnet_gpu_cuda10_1_cudnn7_6.7z

### How to use
- 1 ```git clone https://github.com/msnh2012/Msnhnet```
- 2 ```git clone https://github.com/msnh2012/MsnhnetSharp.git```
- 3 Download pretrained net 链接：https://pan.baidu.com/s/1mBaJvGx7tp2ZsLKzT5ifOg 提取码：x53z
- 4 Build msnhnet with cmake (Release mode) or use prebuild lib.
- 5 Copy msnhnet.dll and opencv dlls to MsnhnetSharp dir.(Debug or Relase)
- 6 Open this project with Visual Studio. 
- 7 Build MsnhnetSharp and MsnhnetForm.
- 8 Set msnhnetPath and msnhbinPath ("models" dir) and labelPath("Msnhnet/labels")
- 9 Read image > init net > run.
### Demo
![](readme_images/ui.png)</br>

Enjoy it.