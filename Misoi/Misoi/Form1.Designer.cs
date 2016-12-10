namespace Misoi
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartBtn = new System.Windows.Forms.Button();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.FilePathTB = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainImage = new System.Windows.Forms.PictureBox();
            this.FilteredImage = new System.Windows.Forms.PictureBox();
            this.medianCB = new System.Windows.Forms.CheckBox();
            this.monochromeCB = new System.Windows.Forms.CheckBox();
            this.levelTB = new System.Windows.Forms.TextBox();
            this.windowsSizeTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilteredImage)).BeginInit();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(31, 640);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(31, 601);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(75, 23);
            this.OpenFileBtn.TabIndex = 1;
            this.OpenFileBtn.Text = "Choose file";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // FilePathTB
            // 
            this.FilePathTB.Location = new System.Drawing.Point(112, 601);
            this.FilePathTB.Name = "FilePathTB";
            this.FilePathTB.Size = new System.Drawing.Size(100, 20);
            this.FilePathTB.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // mainImage
            // 
            this.mainImage.Location = new System.Drawing.Point(31, 28);
            this.mainImage.Name = "mainImage";
            this.mainImage.Size = new System.Drawing.Size(600, 567);
            this.mainImage.TabIndex = 3;
            this.mainImage.TabStop = false;
            // 
            // FilteredImage
            // 
            this.FilteredImage.Location = new System.Drawing.Point(637, 28);
            this.FilteredImage.Name = "FilteredImage";
            this.FilteredImage.Size = new System.Drawing.Size(600, 567);
            this.FilteredImage.TabIndex = 4;
            this.FilteredImage.TabStop = false;
            // 
            // medianCB
            // 
            this.medianCB.AutoSize = true;
            this.medianCB.Location = new System.Drawing.Point(321, 601);
            this.medianCB.Name = "medianCB";
            this.medianCB.Size = new System.Drawing.Size(86, 17);
            this.medianCB.TabIndex = 5;
            this.medianCB.Text = "Median Filter";
            this.medianCB.UseVisualStyleBackColor = true;
            this.medianCB.Visible = false;
            // 
            // monochromeCB
            // 
            this.monochromeCB.AutoSize = true;
            this.monochromeCB.Location = new System.Drawing.Point(321, 624);
            this.monochromeCB.Name = "monochromeCB";
            this.monochromeCB.Size = new System.Drawing.Size(113, 17);
            this.monochromeCB.TabIndex = 6;
            this.monochromeCB.Text = "Monochrome Filter";
            this.monochromeCB.UseVisualStyleBackColor = true;
            this.monochromeCB.Visible = false;
            // 
            // levelTB
            // 
            this.levelTB.Location = new System.Drawing.Point(531, 604);
            this.levelTB.Name = "levelTB";
            this.levelTB.Size = new System.Drawing.Size(100, 20);
            this.levelTB.TabIndex = 7;
            this.levelTB.Text = "70";
            // 
            // windowsSizeTB
            // 
            this.windowsSizeTB.Location = new System.Drawing.Point(531, 631);
            this.windowsSizeTB.Name = "windowsSizeTB";
            this.windowsSizeTB.Size = new System.Drawing.Size(100, 20);
            this.windowsSizeTB.TabIndex = 8;
            this.windowsSizeTB.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(458, 607);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(458, 634);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Window size";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 679);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(736, 607);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(736, 657);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 13;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(637, 631);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 14;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(835, 631);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 704);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.windowsSizeTB);
            this.Controls.Add(this.levelTB);
            this.Controls.Add(this.monochromeCB);
            this.Controls.Add(this.medianCB);
            this.Controls.Add(this.FilteredImage);
            this.Controls.Add(this.mainImage);
            this.Controls.Add(this.FilePathTB);
            this.Controls.Add(this.OpenFileBtn);
            this.Controls.Add(this.StartBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilteredImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button OpenFileBtn;
        private System.Windows.Forms.TextBox FilePathTB;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox mainImage;
        private System.Windows.Forms.PictureBox FilteredImage;
        private System.Windows.Forms.CheckBox medianCB;
        private System.Windows.Forms.CheckBox monochromeCB;
        private System.Windows.Forms.TextBox levelTB;
        private System.Windows.Forms.TextBox windowsSizeTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
    }
}

