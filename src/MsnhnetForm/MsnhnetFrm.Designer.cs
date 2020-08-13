namespace MsnhnetForm
{
    partial class MsnhnetFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.readImgBtn = new System.Windows.Forms.Button();
            this.operationGbx = new System.Windows.Forms.GroupBox();
            this.yoloGPUBtn = new System.Windows.Forms.Button();
            this.yoloDetectBtn = new System.Windows.Forms.Button();
            this.classifyPreBtn = new System.Windows.Forms.Button();
            this.classfiyGPUBtn = new System.Windows.Forms.Button();
            this.classfiyBtn = new System.Windows.Forms.Button();
            this.initBtn = new System.Windows.Forms.Button();
            this.resetImgBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.msnhnetPathTxt = new System.Windows.Forms.TextBox();
            this.msnhbinPathTxt = new System.Windows.Forms.TextBox();
            this.labelPathTxt = new System.Windows.Forms.TextBox();
            this.msnhnetPathBtn = new System.Windows.Forms.Button();
            this.labelPathBtn = new System.Windows.Forms.Button();
            this.msnhBinBtn = new System.Windows.Forms.Button();
            this.netFileGbx = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.operationGbx.SuspendLayout();
            this.netFileGbx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // readImgBtn
            // 
            this.readImgBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.readImgBtn.Location = new System.Drawing.Point(3, 19);
            this.readImgBtn.Name = "readImgBtn";
            this.readImgBtn.Size = new System.Drawing.Size(183, 23);
            this.readImgBtn.TabIndex = 1;
            this.readImgBtn.Text = "读取图片(ReadImg)";
            this.readImgBtn.UseVisualStyleBackColor = true;
            this.readImgBtn.Click += new System.EventHandler(this.readImgBtn_Click);
            // 
            // operationGbx
            // 
            this.operationGbx.Controls.Add(this.yoloGPUBtn);
            this.operationGbx.Controls.Add(this.yoloDetectBtn);
            this.operationGbx.Controls.Add(this.classifyPreBtn);
            this.operationGbx.Controls.Add(this.classfiyGPUBtn);
            this.operationGbx.Controls.Add(this.classfiyBtn);
            this.operationGbx.Controls.Add(this.initBtn);
            this.operationGbx.Controls.Add(this.resetImgBtn);
            this.operationGbx.Controls.Add(this.readImgBtn);
            this.operationGbx.Dock = System.Windows.Forms.DockStyle.Left;
            this.operationGbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.operationGbx.Location = new System.Drawing.Point(0, 0);
            this.operationGbx.Name = "operationGbx";
            this.operationGbx.Size = new System.Drawing.Size(189, 702);
            this.operationGbx.TabIndex = 2;
            this.operationGbx.TabStop = false;
            this.operationGbx.Text = "操作(operation)";
            // 
            // yoloGPUBtn
            // 
            this.yoloGPUBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.yoloGPUBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.yoloGPUBtn.Location = new System.Drawing.Point(3, 180);
            this.yoloGPUBtn.Name = "yoloGPUBtn";
            this.yoloGPUBtn.Size = new System.Drawing.Size(183, 23);
            this.yoloGPUBtn.TabIndex = 15;
            this.yoloGPUBtn.Text = "yolo GPU(Yolo Detect GPU)";
            this.yoloGPUBtn.UseVisualStyleBackColor = false;
            this.yoloGPUBtn.Click += new System.EventHandler(this.yoloGPUBtn_Click);
            // 
            // yoloDetectBtn
            // 
            this.yoloDetectBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.yoloDetectBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.yoloDetectBtn.Location = new System.Drawing.Point(3, 157);
            this.yoloDetectBtn.Name = "yoloDetectBtn";
            this.yoloDetectBtn.Size = new System.Drawing.Size(183, 23);
            this.yoloDetectBtn.TabIndex = 14;
            this.yoloDetectBtn.Text = "yolo检测(Yolo Detect)";
            this.yoloDetectBtn.UseVisualStyleBackColor = false;
            this.yoloDetectBtn.Click += new System.EventHandler(this.yoloDetectBtn_Click);
            // 
            // classifyPreBtn
            // 
            this.classifyPreBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.classifyPreBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.classifyPreBtn.Location = new System.Drawing.Point(3, 134);
            this.classifyPreBtn.Name = "classifyPreBtn";
            this.classifyPreBtn.Size = new System.Drawing.Size(183, 23);
            this.classifyPreBtn.TabIndex = 13;
            this.classifyPreBtn.Text = "分类+预处理(Classify+Pre)";
            this.classifyPreBtn.UseVisualStyleBackColor = false;
            this.classifyPreBtn.Click += new System.EventHandler(this.classifyPreBtn_Click);
            // 
            // classfiyGPUBtn
            // 
            this.classfiyGPUBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.classfiyGPUBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.classfiyGPUBtn.Location = new System.Drawing.Point(3, 111);
            this.classfiyGPUBtn.Name = "classfiyGPUBtn";
            this.classfiyGPUBtn.Size = new System.Drawing.Size(183, 23);
            this.classfiyGPUBtn.TabIndex = 12;
            this.classfiyGPUBtn.Text = "分类GPU(Classify)";
            this.classfiyGPUBtn.UseVisualStyleBackColor = false;
            this.classfiyGPUBtn.Click += new System.EventHandler(this.classfiyGPUBtn_Click);
            // 
            // classfiyBtn
            // 
            this.classfiyBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.classfiyBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.classfiyBtn.Location = new System.Drawing.Point(3, 88);
            this.classfiyBtn.Name = "classfiyBtn";
            this.classfiyBtn.Size = new System.Drawing.Size(183, 23);
            this.classfiyBtn.TabIndex = 11;
            this.classfiyBtn.Text = "分类(Classify)";
            this.classfiyBtn.UseVisualStyleBackColor = false;
            this.classfiyBtn.Click += new System.EventHandler(this.classfiyBtn_Click);
            // 
            // initBtn
            // 
            this.initBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.initBtn.Location = new System.Drawing.Point(3, 65);
            this.initBtn.Name = "initBtn";
            this.initBtn.Size = new System.Drawing.Size(183, 23);
            this.initBtn.TabIndex = 10;
            this.initBtn.Text = "初始化网络(init net)";
            this.initBtn.UseVisualStyleBackColor = true;
            this.initBtn.Click += new System.EventHandler(this.initBtn_Click);
            // 
            // resetImgBtn
            // 
            this.resetImgBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.resetImgBtn.Location = new System.Drawing.Point(3, 42);
            this.resetImgBtn.Name = "resetImgBtn";
            this.resetImgBtn.Size = new System.Drawing.Size(183, 23);
            this.resetImgBtn.TabIndex = 9;
            this.resetImgBtn.Text = "重置图片(ResetImg)";
            this.resetImgBtn.UseVisualStyleBackColor = true;
            this.resetImgBtn.Click += new System.EventHandler(this.resetImgBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox1.Location = new System.Drawing.Point(1131, 55);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(311, 647);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(189, 680);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(942, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // msnhnetPathTxt
            // 
            this.msnhnetPathTxt.Location = new System.Drawing.Point(113, 24);
            this.msnhnetPathTxt.Name = "msnhnetPathTxt";
            this.msnhnetPathTxt.Size = new System.Drawing.Size(303, 23);
            this.msnhnetPathTxt.TabIndex = 1;
            this.msnhnetPathTxt.Text = "darknet53.msnhnet";
            // 
            // msnhbinPathTxt
            // 
            this.msnhbinPathTxt.Location = new System.Drawing.Point(528, 24);
            this.msnhbinPathTxt.Name = "msnhbinPathTxt";
            this.msnhbinPathTxt.Size = new System.Drawing.Size(303, 23);
            this.msnhbinPathTxt.TabIndex = 1;
            this.msnhbinPathTxt.Text = "darknet53.msnhbin";
            // 
            // labelPathTxt
            // 
            this.labelPathTxt.Location = new System.Drawing.Point(942, 24);
            this.labelPathTxt.Name = "labelPathTxt";
            this.labelPathTxt.Size = new System.Drawing.Size(303, 23);
            this.labelPathTxt.TabIndex = 3;
            this.labelPathTxt.Text = "imagenet_cn.names";
            // 
            // msnhnetPathBtn
            // 
            this.msnhnetPathBtn.Location = new System.Drawing.Point(12, 24);
            this.msnhnetPathBtn.Name = "msnhnetPathBtn";
            this.msnhnetPathBtn.Size = new System.Drawing.Size(95, 23);
            this.msnhnetPathBtn.TabIndex = 4;
            this.msnhnetPathBtn.Text = "msnhnetPath";
            this.msnhnetPathBtn.UseVisualStyleBackColor = true;
            this.msnhnetPathBtn.Click += new System.EventHandler(this.msnhnetPathBtn_Click);
            // 
            // labelPathBtn
            // 
            this.labelPathBtn.Location = new System.Drawing.Point(841, 24);
            this.labelPathBtn.Name = "labelPathBtn";
            this.labelPathBtn.Size = new System.Drawing.Size(95, 23);
            this.labelPathBtn.TabIndex = 4;
            this.labelPathBtn.Text = "labelPath";
            this.labelPathBtn.UseVisualStyleBackColor = true;
            this.labelPathBtn.Click += new System.EventHandler(this.labelPathBtn_Click);
            // 
            // msnhBinBtn
            // 
            this.msnhBinBtn.Location = new System.Drawing.Point(427, 24);
            this.msnhBinBtn.Name = "msnhBinBtn";
            this.msnhBinBtn.Size = new System.Drawing.Size(95, 23);
            this.msnhBinBtn.TabIndex = 4;
            this.msnhBinBtn.Text = "msnhbinPath";
            this.msnhBinBtn.UseVisualStyleBackColor = true;
            this.msnhBinBtn.Click += new System.EventHandler(this.msnhnetBtn_Click);
            // 
            // netFileGbx
            // 
            this.netFileGbx.Controls.Add(this.msnhBinBtn);
            this.netFileGbx.Controls.Add(this.labelPathBtn);
            this.netFileGbx.Controls.Add(this.msnhnetPathBtn);
            this.netFileGbx.Controls.Add(this.labelPathTxt);
            this.netFileGbx.Controls.Add(this.msnhbinPathTxt);
            this.netFileGbx.Controls.Add(this.msnhnetPathTxt);
            this.netFileGbx.Dock = System.Windows.Forms.DockStyle.Top;
            this.netFileGbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.netFileGbx.Location = new System.Drawing.Point(189, 0);
            this.netFileGbx.Name = "netFileGbx";
            this.netFileGbx.Size = new System.Drawing.Size(1253, 55);
            this.netFileGbx.TabIndex = 3;
            this.netFileGbx.TabStop = false;
            this.netFileGbx.Text = "参数配置(Params Config)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(189, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(942, 625);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // MsnhnetFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 702);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.netFileGbx);
            this.Controls.Add(this.operationGbx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MsnhnetFrm";
            this.Text = "Msnhnet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MsnhnetFrm_FormClosing);
            this.operationGbx.ResumeLayout(false);
            this.netFileGbx.ResumeLayout(false);
            this.netFileGbx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button readImgBtn;
        private System.Windows.Forms.GroupBox operationGbx;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox msnhnetPathTxt;
        private System.Windows.Forms.TextBox msnhbinPathTxt;
        private System.Windows.Forms.TextBox labelPathTxt;
        private System.Windows.Forms.Button msnhnetPathBtn;
        private System.Windows.Forms.Button labelPathBtn;
        private System.Windows.Forms.Button msnhBinBtn;
        private System.Windows.Forms.GroupBox netFileGbx;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button yoloGPUBtn;
        private System.Windows.Forms.Button yoloDetectBtn;
        private System.Windows.Forms.Button classifyPreBtn;
        private System.Windows.Forms.Button classfiyGPUBtn;
        private System.Windows.Forms.Button classfiyBtn;
        private System.Windows.Forms.Button initBtn;
        private System.Windows.Forms.Button resetImgBtn;
    }
}

