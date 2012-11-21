namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LeftHandLabel = new System.Windows.Forms.Label();
            this.RightHandLabel = new System.Windows.Forms.Label();
            this.StraightLabel = new System.Windows.Forms.Label();
            this.TurnLabel = new System.Windows.Forms.Label();
            this.BrakeLabel = new System.Windows.Forms.Label();
            this.RightOffsetLabel = new System.Windows.Forms.Label();
            this.LeftOffsetLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.NXTLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LeftHandLabel
            // 
            this.LeftHandLabel.AutoSize = true;
            this.LeftHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftHandLabel.Location = new System.Drawing.Point(35, 30);
            this.LeftHandLabel.Name = "LeftHandLabel";
            this.LeftHandLabel.Size = new System.Drawing.Size(133, 31);
            this.LeftHandLabel.TabIndex = 0;
            this.LeftHandLabel.Text = "LeftHand:";
            // 
            // RightHandLabel
            // 
            this.RightHandLabel.AutoSize = true;
            this.RightHandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightHandLabel.Location = new System.Drawing.Point(35, 90);
            this.RightHandLabel.Name = "RightHandLabel";
            this.RightHandLabel.Size = new System.Drawing.Size(158, 31);
            this.RightHandLabel.TabIndex = 1;
            this.RightHandLabel.Text = "RightHand: ";
            this.RightHandLabel.UseWaitCursor = true;
            // 
            // StraightLabel
            // 
            this.StraightLabel.AutoSize = true;
            this.StraightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StraightLabel.Location = new System.Drawing.Point(35, 273);
            this.StraightLabel.Name = "StraightLabel";
            this.StraightLabel.Size = new System.Drawing.Size(88, 31);
            this.StraightLabel.TabIndex = 2;
            this.StraightLabel.Text = "Go to:";
            // 
            // TurnLabel
            // 
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TurnLabel.Location = new System.Drawing.Point(35, 230);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(108, 31);
            this.TurnLabel.TabIndex = 3;
            this.TurnLabel.Text = "Turn to:";
            // 
            // BrakeLabel
            // 
            this.BrakeLabel.AutoSize = true;
            this.BrakeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrakeLabel.Location = new System.Drawing.Point(35, 313);
            this.BrakeLabel.Name = "BrakeLabel";
            this.BrakeLabel.Size = new System.Drawing.Size(100, 31);
            this.BrakeLabel.TabIndex = 4;
            this.BrakeLabel.Text = "Brake: ";
            // 
            // RightOffsetLabel
            // 
            this.RightOffsetLabel.AutoSize = true;
            this.RightOffsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightOffsetLabel.Location = new System.Drawing.Point(35, 179);
            this.RightOffsetLabel.Name = "RightOffsetLabel";
            this.RightOffsetLabel.Size = new System.Drawing.Size(225, 31);
            this.RightOffsetLabel.TabIndex = 5;
            this.RightOffsetLabel.Text = "RightHandOffset:";
            this.RightOffsetLabel.UseWaitCursor = true;
            // 
            // LeftOffsetLabel
            // 
            this.LeftOffsetLabel.AutoSize = true;
            this.LeftOffsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftOffsetLabel.Location = new System.Drawing.Point(35, 139);
            this.LeftOffsetLabel.Name = "LeftOffsetLabel";
            this.LeftOffsetLabel.Size = new System.Drawing.Size(207, 31);
            this.LeftOffsetLabel.TabIndex = 6;
            this.LeftOffsetLabel.Text = "LeftHandOffset:";
            this.LeftOffsetLabel.UseWaitCursor = true;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(35, 413);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(222, 31);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Text = "Kinect is initialing";
            // 
            // NXTLabel
            // 
            this.NXTLabel.AutoSize = true;
            this.NXTLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NXTLabel.Location = new System.Drawing.Point(35, 382);
            this.NXTLabel.Name = "NXTLabel";
            this.NXTLabel.Size = new System.Drawing.Size(201, 31);
            this.NXTLabel.TabIndex = 8;
            this.NXTLabel.Text = "NXT is initialing";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(882, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(235, 418);
            this.textBox1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(430, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "stop kinect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(537, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Brake";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(648, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Set Idle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 508);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.NXTLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.LeftOffsetLabel);
            this.Controls.Add(this.RightOffsetLabel);
            this.Controls.Add(this.BrakeLabel);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.StraightLabel);
            this.Controls.Add(this.RightHandLabel);
            this.Controls.Add(this.LeftHandLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LeftHandLabel;
        private System.Windows.Forms.Label RightHandLabel;
        private System.Windows.Forms.Label StraightLabel;
        private System.Windows.Forms.Label TurnLabel;
        private System.Windows.Forms.Label BrakeLabel;
        private System.Windows.Forms.Label RightOffsetLabel;
        private System.Windows.Forms.Label LeftOffsetLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label NXTLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

