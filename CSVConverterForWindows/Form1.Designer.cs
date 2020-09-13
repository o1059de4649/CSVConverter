namespace CreateModelClass
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.AddFileButton = new System.Windows.Forms.Button();
            this.FileNameBox = new System.Windows.Forms.TextBox();
            this.ExcuteButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ExportPathBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.KeyWordBox = new System.Windows.Forms.TextBox();
            this.KeyWordLabel = new System.Windows.Forms.Label();
            this.SearchWordBox = new System.Windows.Forms.TextBox();
            this.SearchWordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "インポートするフォルダを選択してください。";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // AddFileButton
            // 
            this.AddFileButton.Location = new System.Drawing.Point(287, 38);
            this.AddFileButton.Name = "AddFileButton";
            this.AddFileButton.Size = new System.Drawing.Size(89, 30);
            this.AddFileButton.TabIndex = 1;
            this.AddFileButton.Text = "フォルダを参照";
            this.AddFileButton.UseVisualStyleBackColor = true;
            this.AddFileButton.Click += new System.EventHandler(this.AddFileButton_Click);
            // 
            // FileNameBox
            // 
            this.FileNameBox.Location = new System.Drawing.Point(22, 44);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(245, 19);
            this.FileNameBox.TabIndex = 2;
            // 
            // ExcuteButton
            // 
            this.ExcuteButton.Location = new System.Drawing.Point(447, 432);
            this.ExcuteButton.Name = "ExcuteButton";
            this.ExcuteButton.Size = new System.Drawing.Size(71, 31);
            this.ExcuteButton.TabIndex = 3;
            this.ExcuteButton.Text = "実行";
            this.ExcuteButton.UseVisualStyleBackColor = true;
            this.ExcuteButton.Click += new System.EventHandler(this.ExcuteButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(287, 97);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(89, 30);
            this.ExportButton.TabIndex = 4;
            this.ExportButton.Text = "フォルダを参照";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ExportPathBox
            // 
            this.ExportPathBox.Location = new System.Drawing.Point(22, 103);
            this.ExportPathBox.Name = "ExportPathBox";
            this.ExportPathBox.Size = new System.Drawing.Size(245, 19);
            this.ExportPathBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "エクスポートするフォルダを選択してください。";
            // 
            // KeyWordBox
            // 
            this.KeyWordBox.Location = new System.Drawing.Point(22, 170);
            this.KeyWordBox.Name = "KeyWordBox";
            this.KeyWordBox.Size = new System.Drawing.Size(245, 19);
            this.KeyWordBox.TabIndex = 7;
            this.KeyWordBox.Text = "ワンピース レディース";
            // 
            // KeyWordLabel
            // 
            this.KeyWordLabel.AutoSize = true;
            this.KeyWordLabel.Location = new System.Drawing.Point(20, 155);
            this.KeyWordLabel.Name = "KeyWordLabel";
            this.KeyWordLabel.Size = new System.Drawing.Size(201, 12);
            this.KeyWordLabel.TabIndex = 8;
            this.KeyWordLabel.Text = "先頭に追加する文字列を入力してください";
            // 
            // SearchWordBox
            // 
            this.SearchWordBox.Location = new System.Drawing.Point(22, 223);
            this.SearchWordBox.Name = "SearchWordBox";
            this.SearchWordBox.Size = new System.Drawing.Size(245, 19);
            this.SearchWordBox.TabIndex = 9;
            this.SearchWordBox.Text = "バックシャン ワンピース 夏";
            // 
            // SearchWordLabel
            // 
            this.SearchWordLabel.AutoSize = true;
            this.SearchWordLabel.Location = new System.Drawing.Point(20, 208);
            this.SearchWordLabel.Name = "SearchWordLabel";
            this.SearchWordLabel.Size = new System.Drawing.Size(168, 12);
            this.SearchWordLabel.TabIndex = 10;
            this.SearchWordLabel.Text = "検索する文字列を入力してください";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 473);
            this.Controls.Add(this.SearchWordLabel);
            this.Controls.Add(this.SearchWordBox);
            this.Controls.Add(this.KeyWordLabel);
            this.Controls.Add(this.KeyWordBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExportPathBox);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.ExcuteButton);
            this.Controls.Add(this.FileNameBox);
            this.Controls.Add(this.AddFileButton);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddFileButton;
        private System.Windows.Forms.TextBox FileNameBox;
        private System.Windows.Forms.Button ExcuteButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.TextBox ExportPathBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KeyWordBox;
        private System.Windows.Forms.Label KeyWordLabel;
        private System.Windows.Forms.TextBox SearchWordBox;
        private System.Windows.Forms.Label SearchWordLabel;
    }
}

