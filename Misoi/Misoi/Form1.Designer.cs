﻿namespace Misoi
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
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilteredImage)).BeginInit();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(732, 28);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(732, 104);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(75, 23);
            this.OpenFileBtn.TabIndex = 1;
            this.OpenFileBtn.Text = "Choose file";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // FilePathTB
            // 
            this.FilePathTB.Location = new System.Drawing.Point(732, 133);
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
            this.mainImage.Size = new System.Drawing.Size(309, 251);
            this.mainImage.TabIndex = 3;
            this.mainImage.TabStop = false;
            // 
            // FilteredImage
            // 
            this.FilteredImage.Location = new System.Drawing.Point(363, 28);
            this.FilteredImage.Name = "FilteredImage";
            this.FilteredImage.Size = new System.Drawing.Size(311, 251);
            this.FilteredImage.TabIndex = 4;
            this.FilteredImage.TabStop = false;
            // 
            // medianCB
            // 
            this.medianCB.AutoSize = true;
            this.medianCB.Location = new System.Drawing.Point(732, 57);
            this.medianCB.Name = "medianCB";
            this.medianCB.Size = new System.Drawing.Size(86, 17);
            this.medianCB.TabIndex = 5;
            this.medianCB.Text = "Median Filter";
            this.medianCB.UseVisualStyleBackColor = true;
            // 
            // monochromeCB
            // 
            this.monochromeCB.AutoSize = true;
            this.monochromeCB.Location = new System.Drawing.Point(732, 81);
            this.monochromeCB.Name = "monochromeCB";
            this.monochromeCB.Size = new System.Drawing.Size(113, 17);
            this.monochromeCB.TabIndex = 6;
            this.monochromeCB.Text = "Monochrome Filter";
            this.monochromeCB.UseVisualStyleBackColor = true;
            // 
            // levelTB
            // 
            this.levelTB.Location = new System.Drawing.Point(710, 183);
            this.levelTB.Name = "levelTB";
            this.levelTB.Size = new System.Drawing.Size(100, 20);
            this.levelTB.TabIndex = 7;
            this.levelTB.Text = "70";
            // 
            // windowsSizeTB
            // 
            this.windowsSizeTB.Location = new System.Drawing.Point(710, 220);
            this.windowsSizeTB.Name = "windowsSizeTB";
            this.windowsSizeTB.Size = new System.Drawing.Size(100, 20);
            this.windowsSizeTB.TabIndex = 8;
            this.windowsSizeTB.Text = "5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 309);
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
    }
}

