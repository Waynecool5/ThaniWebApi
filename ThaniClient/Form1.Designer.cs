namespace ThaniClient
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
            this.btnRedeem = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCus = new System.Windows.Forms.TextBox();
            this.txtFname = new System.Windows.Forms.TextBox();
            this.txtLname = new System.Windows.Forms.TextBox();
            this.txtSales = new System.Windows.Forms.TextBox();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.txtMDiscount = new System.Windows.Forms.TextBox();
            this.txtTPoints = new System.Windows.Forms.TextBox();
            this.txtLoca = new System.Windows.Forms.TextBox();
            this.txtCashier = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBalance = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnVoid = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMPoints = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMValues = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLoca = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTrans = new System.Windows.Forms.Label();
            this.lblInvoice = new System.Windows.Forms.Label();
            this.panDisplay = new System.Windows.Forms.Panel();
            this.lblMode = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.txtPts = new System.Windows.Forms.TextBox();
            this.txtExpired = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.btnHide = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblEdit = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRedeem
            // 
            this.btnRedeem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRedeem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedeem.ForeColor = System.Drawing.Color.White;
            this.btnRedeem.Location = new System.Drawing.Point(15, 13);
            this.btnRedeem.Margin = new System.Windows.Forms.Padding(4);
            this.btnRedeem.Name = "btnRedeem";
            this.btnRedeem.Size = new System.Drawing.Size(124, 38);
            this.btnRedeem.TabIndex = 0;
            this.btnRedeem.Text = "Send/Redeem";
            this.btnRedeem.UseVisualStyleBackColor = false;
            this.btnRedeem.Click += new System.EventHandler(this.btnRedeem_ClickAsync);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(582, 13);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 38);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "CloseOut";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Card #:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 108);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "FirstName:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "LastName:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 352);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Point Valued:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(360, 352);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Store Discount:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 303);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Total Points:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 247);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Total Sales:";
            // 
            // txtCus
            // 
            this.txtCus.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCus.ForeColor = System.Drawing.Color.Black;
            this.txtCus.Location = new System.Drawing.Point(79, 55);
            this.txtCus.Margin = new System.Windows.Forms.Padding(4);
            this.txtCus.MinimumSize = new System.Drawing.Size(283, 30);
            this.txtCus.Name = "txtCus";
            this.txtCus.Size = new System.Drawing.Size(283, 37);
            this.txtCus.TabIndex = 11;
            this.txtCus.Text = "000000000000";
            // 
            // txtFname
            // 
            this.txtFname.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFname.ForeColor = System.Drawing.Color.Black;
            this.txtFname.Location = new System.Drawing.Point(79, 99);
            this.txtFname.Margin = new System.Windows.Forms.Padding(4);
            this.txtFname.MinimumSize = new System.Drawing.Size(283, 30);
            this.txtFname.Name = "txtFname";
            this.txtFname.Size = new System.Drawing.Size(283, 33);
            this.txtFname.TabIndex = 12;
            this.txtFname.Text = "Tester";
            // 
            // txtLname
            // 
            this.txtLname.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLname.ForeColor = System.Drawing.Color.Black;
            this.txtLname.Location = new System.Drawing.Point(79, 143);
            this.txtLname.Margin = new System.Windows.Forms.Padding(4);
            this.txtLname.MinimumSize = new System.Drawing.Size(283, 30);
            this.txtLname.Name = "txtLname";
            this.txtLname.Size = new System.Drawing.Size(283, 33);
            this.txtLname.TabIndex = 13;
            this.txtLname.Text = "Tesrter";
            // 
            // txtSales
            // 
            this.txtSales.BackColor = System.Drawing.Color.Green;
            this.txtSales.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSales.ForeColor = System.Drawing.Color.Lime;
            this.txtSales.Location = new System.Drawing.Point(112, 236);
            this.txtSales.Margin = new System.Windows.Forms.Padding(4);
            this.txtSales.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtSales.Name = "txtSales";
            this.txtSales.Size = new System.Drawing.Size(175, 40);
            this.txtSales.TabIndex = 3;
            this.txtSales.Text = "20000.00";
            // 
            // txtPoints
            // 
            this.txtPoints.BackColor = System.Drawing.Color.Green;
            this.txtPoints.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPoints.ForeColor = System.Drawing.Color.Lime;
            this.txtPoints.Location = new System.Drawing.Point(112, 342);
            this.txtPoints.Margin = new System.Windows.Forms.Padding(4);
            this.txtPoints.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(175, 40);
            this.txtPoints.TabIndex = 5;
            this.txtPoints.Text = "20.00";
            // 
            // txtMDiscount
            // 
            this.txtMDiscount.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtMDiscount.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMDiscount.ForeColor = System.Drawing.Color.White;
            this.txtMDiscount.Location = new System.Drawing.Point(471, 342);
            this.txtMDiscount.Margin = new System.Windows.Forms.Padding(4);
            this.txtMDiscount.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtMDiscount.Name = "txtMDiscount";
            this.txtMDiscount.Size = new System.Drawing.Size(175, 40);
            this.txtMDiscount.TabIndex = 16;
            this.txtMDiscount.Text = "30.00";
            // 
            // txtTPoints
            // 
            this.txtTPoints.BackColor = System.Drawing.Color.Green;
            this.txtTPoints.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTPoints.ForeColor = System.Drawing.Color.Lime;
            this.txtTPoints.Location = new System.Drawing.Point(112, 289);
            this.txtTPoints.Margin = new System.Windows.Forms.Padding(4);
            this.txtTPoints.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtTPoints.Name = "txtTPoints";
            this.txtTPoints.Size = new System.Drawing.Size(175, 40);
            this.txtTPoints.TabIndex = 4;
            this.txtTPoints.Text = "2000";
            // 
            // txtLoca
            // 
            this.txtLoca.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoca.Location = new System.Drawing.Point(433, 99);
            this.txtLoca.Margin = new System.Windows.Forms.Padding(4);
            this.txtLoca.MinimumSize = new System.Drawing.Size(250, 30);
            this.txtLoca.Name = "txtLoca";
            this.txtLoca.Size = new System.Drawing.Size(250, 33);
            this.txtLoca.TabIndex = 18;
            this.txtLoca.Text = "Swan Street";
            // 
            // txtCashier
            // 
            this.txtCashier.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashier.Location = new System.Drawing.Point(433, 143);
            this.txtCashier.Margin = new System.Windows.Forms.Padding(4);
            this.txtCashier.MinimumSize = new System.Drawing.Size(250, 30);
            this.txtCashier.Name = "txtCashier";
            this.txtCashier.Size = new System.Drawing.Size(250, 33);
            this.txtCashier.TabIndex = 19;
            this.txtCashier.Text = "Cashier No: 1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btnBalance);
            this.panel1.Controls.Add(this.btnHistory);
            this.panel1.Controls.Add(this.btnRefund);
            this.panel1.Controls.Add(this.btnVerify);
            this.panel1.Controls.Add(this.btnVoid);
            this.panel1.Controls.Add(this.btnRedeem);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 412);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 65);
            this.panel1.TabIndex = 20;
            // 
            // btnBalance
            // 
            this.btnBalance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnBalance.Location = new System.Drawing.Point(152, 13);
            this.btnBalance.Margin = new System.Windows.Forms.Padding(4);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(73, 38);
            this.btnBalance.TabIndex = 1;
            this.btnBalance.Text = "Balance";
            this.btnBalance.UseVisualStyleBackColor = true;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnHistory.Location = new System.Drawing.Point(410, 13);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(73, 38);
            this.btnHistory.TabIndex = 4;
            this.btnHistory.Text = "History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnRefund.Location = new System.Drawing.Point(324, 13);
            this.btnRefund.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(73, 38);
            this.btnRefund.TabIndex = 3;
            this.btnRefund.Text = "Refund";
            this.btnRefund.UseVisualStyleBackColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnVerify.Location = new System.Drawing.Point(238, 13);
            this.btnVerify.Margin = new System.Windows.Forms.Padding(4);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(73, 38);
            this.btnVerify.TabIndex = 2;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnVoid
            // 
            this.btnVoid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnVoid.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnVoid.Location = new System.Drawing.Point(496, 13);
            this.btnVoid.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(73, 38);
            this.btnVoid.TabIndex = 5;
            this.btnVoid.Text = "Void";
            this.btnVoid.UseVisualStyleBackColor = false;
            this.btnVoid.Click += new System.EventHandler(this.btnVoid_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 19);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 16);
            this.label10.TabIndex = 21;
            this.label10.Text = "Customer Type:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.White;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(132, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 20);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Customer";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(245, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 20);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "BARP";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.White;
            this.radioButton3.Location = new System.Drawing.Point(331, 17);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(68, 20);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "STAFF";
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 200);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(159, 25);
            this.label11.TabIndex = 25;
            this.label11.Text = "Sales Details:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(359, 202);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(202, 23);
            this.label12.TabIndex = 26;
            this.label12.Text = "Massy Sys. Invoice:";
            // 
            // txtMPoints
            // 
            this.txtMPoints.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtMPoints.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMPoints.ForeColor = System.Drawing.Color.White;
            this.txtMPoints.Location = new System.Drawing.Point(471, 236);
            this.txtMPoints.Margin = new System.Windows.Forms.Padding(4);
            this.txtMPoints.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtMPoints.Name = "txtMPoints";
            this.txtMPoints.Size = new System.Drawing.Size(175, 40);
            this.txtMPoints.TabIndex = 28;
            this.txtMPoints.Text = "1500";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(362, 253);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 16);
            this.label13.TabIndex = 27;
            this.label13.Text = "System Point:";
            // 
            // txtMValues
            // 
            this.txtMValues.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtMValues.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMValues.ForeColor = System.Drawing.Color.White;
            this.txtMValues.Location = new System.Drawing.Point(471, 289);
            this.txtMValues.Margin = new System.Windows.Forms.Padding(4);
            this.txtMValues.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtMValues.Name = "txtMValues";
            this.txtMValues.Size = new System.Drawing.Size(175, 40);
            this.txtMValues.TabIndex = 30;
            this.txtMValues.Text = "150";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(362, 303);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 16);
            this.label14.TabIndex = 29;
            this.label14.Text = "System Value:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.lblLoca);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.txtLoca);
            this.panel2.Controls.Add(this.txtCashier);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(696, 194);
            this.panel2.TabIndex = 35;
            // 
            // lblLoca
            // 
            this.lblLoca.AutoSize = true;
            this.lblLoca.BackColor = System.Drawing.Color.Magenta;
            this.lblLoca.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoca.ForeColor = System.Drawing.Color.Black;
            this.lblLoca.Location = new System.Drawing.Point(430, 63);
            this.lblLoca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoca.MinimumSize = new System.Drawing.Size(105, 18);
            this.lblLoca.Name = "lblLoca";
            this.lblLoca.Size = new System.Drawing.Size(105, 18);
            this.lblLoca.TabIndex = 38;
            this.lblLoca.Text = "LocID: SS";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(551, 15);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(126, 25);
            this.label22.TabIndex = 37;
            this.label22.Text = "Sales Date";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(444, 18);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.MinimumSize = new System.Drawing.Size(105, 18);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 18);
            this.label20.TabIndex = 36;
            this.label20.Text = "Sales Date:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(368, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Location:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(376, 154);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Cashier:";
            // 
            // lblTrans
            // 
            this.lblTrans.AutoSize = true;
            this.lblTrans.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrans.ForeColor = System.Drawing.Color.Brown;
            this.lblTrans.Location = new System.Drawing.Point(169, 202);
            this.lblTrans.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrans.Name = "lblTrans";
            this.lblTrans.Size = new System.Drawing.Size(160, 23);
            this.lblTrans.TabIndex = 35;
            this.lblTrans.Text = "Transaction #";
            // 
            // lblInvoice
            // 
            this.lblInvoice.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoice.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblInvoice.Location = new System.Drawing.Point(556, 201);
            this.lblInvoice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInvoice.MinimumSize = new System.Drawing.Size(115, 25);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(132, 25);
            this.lblInvoice.TabIndex = 37;
            this.lblInvoice.Text = "Invoice#";
            // 
            // panDisplay
            // 
            this.panDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panDisplay.Controls.Add(this.lblMode);
            this.panDisplay.Controls.Add(this.btnSubmit);
            this.panDisplay.Controls.Add(this.txtBalance);
            this.panDisplay.Controls.Add(this.txtCardNo);
            this.panDisplay.Controls.Add(this.txtInvoice);
            this.panDisplay.Controls.Add(this.txtPts);
            this.panDisplay.Controls.Add(this.txtExpired);
            this.panDisplay.Controls.Add(this.txtType);
            this.panDisplay.Controls.Add(this.btnHide);
            this.panDisplay.Controls.Add(this.label25);
            this.panDisplay.Controls.Add(this.label24);
            this.panDisplay.Controls.Add(this.label23);
            this.panDisplay.Controls.Add(this.label18);
            this.panDisplay.Controls.Add(this.label17);
            this.panDisplay.Controls.Add(this.label16);
            this.panDisplay.Controls.Add(this.lblEdit);
            this.panDisplay.Controls.Add(this.label15);
            this.panDisplay.Location = new System.Drawing.Point(100, 20);
            this.panDisplay.Name = "panDisplay";
            this.panDisplay.Size = new System.Drawing.Size(498, 386);
            this.panDisplay.TabIndex = 40;
            this.panDisplay.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.BackColor = System.Drawing.Color.Magenta;
            this.lblMode.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Black;
            this.lblMode.Location = new System.Drawing.Point(401, 273);
            this.lblMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMode.MinimumSize = new System.Drawing.Size(50, 20);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(50, 34);
            this.lblMode.TabIndex = 48;
            this.lblMode.Text = "invoice Date:";
            this.lblMode.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.Lime;
            this.btnSubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(30, 333);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(157, 38);
            this.btnSubmit.TabIndex = 47;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_ClickAsync);
            // 
            // txtBalance
            // 
            this.txtBalance.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtBalance.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalance.ForeColor = System.Drawing.Color.White;
            this.txtBalance.Location = new System.Drawing.Point(195, 117);
            this.txtBalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtBalance.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(175, 40);
            this.txtBalance.TabIndex = 46;
            this.txtBalance.Text = "0";
            // 
            // txtCardNo
            // 
            this.txtCardNo.BackColor = System.Drawing.Color.White;
            this.txtCardNo.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardNo.Location = new System.Drawing.Point(195, 64);
            this.txtCardNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCardNo.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(287, 40);
            this.txtCardNo.TabIndex = 45;
            this.txtCardNo.Text = "####-####-####-####";
            // 
            // txtInvoice
            // 
            this.txtInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInvoice.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoice.ForeColor = System.Drawing.Color.Black;
            this.txtInvoice.Location = new System.Drawing.Point(195, 21);
            this.txtInvoice.Margin = new System.Windows.Forms.Padding(4);
            this.txtInvoice.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.ReadOnly = true;
            this.txtInvoice.Size = new System.Drawing.Size(266, 26);
            this.txtInvoice.TabIndex = 44;
            this.txtInvoice.Text = "MassyRec#";
            // 
            // txtPts
            // 
            this.txtPts.BackColor = System.Drawing.Color.White;
            this.txtPts.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPts.ForeColor = System.Drawing.Color.Red;
            this.txtPts.Location = new System.Drawing.Point(195, 273);
            this.txtPts.Margin = new System.Windows.Forms.Padding(4);
            this.txtPts.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtPts.Name = "txtPts";
            this.txtPts.Size = new System.Drawing.Size(175, 40);
            this.txtPts.TabIndex = 43;
            this.txtPts.Text = "0";
            // 
            // txtExpired
            // 
            this.txtExpired.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtExpired.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpired.ForeColor = System.Drawing.Color.White;
            this.txtExpired.Location = new System.Drawing.Point(195, 221);
            this.txtExpired.Margin = new System.Windows.Forms.Padding(4);
            this.txtExpired.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtExpired.Name = "txtExpired";
            this.txtExpired.ReadOnly = true;
            this.txtExpired.Size = new System.Drawing.Size(175, 40);
            this.txtExpired.TabIndex = 42;
            this.txtExpired.Text = "0";
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtType.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.ForeColor = System.Drawing.Color.White;
            this.txtType.Location = new System.Drawing.Point(195, 169);
            this.txtType.Margin = new System.Windows.Forms.Padding(4);
            this.txtType.MinimumSize = new System.Drawing.Size(175, 40);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(175, 40);
            this.txtType.TabIndex = 41;
            this.txtType.Text = "$0.00";
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.Red;
            this.btnHide.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.ForeColor = System.Drawing.Color.Black;
            this.btnHide.Location = new System.Drawing.Point(332, 333);
            this.btnHide.Margin = new System.Windows.Forms.Padding(4);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(140, 38);
            this.btnHide.TabIndex = 40;
            this.btnHide.Text = "Cancel Redeem";
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(12, 230);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(171, 25);
            this.label25.TabIndex = 39;
            this.label25.Text = "Expired Points:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(12, 177);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(161, 25);
            this.label24.TabIndex = 38;
            this.label24.Text = "Balance Type:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(12, 126);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(175, 25);
            this.label23.TabIndex = 37;
            this.label23.Text = "Balance Points:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(378, 204);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(151, 25);
            this.label18.TabIndex = 34;
            this.label18.Text = "TRXNPOINTS";
            this.label18.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(12, 72);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(151, 25);
            this.label17.TabIndex = 33;
            this.label17.Text = "Customer_ID";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 25);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(122, 25);
            this.label16.TabIndex = 32;
            this.label16.Text = "Invoice #:";
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEdit.Location = new System.Drawing.Point(12, 282);
            this.lblEdit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(166, 25);
            this.lblEdit.TabIndex = 36;
            this.lblEdit.Text = "Total Redeem:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(387, 177);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(153, 25);
            this.label15.TabIndex = 31;
            this.label15.Text = "invoice Date:";
            this.label15.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(696, 477);
            this.Controls.Add(this.panDisplay);
            this.Controls.Add(this.lblInvoice);
            this.Controls.Add(this.lblTrans);
            this.Controls.Add(this.txtMValues);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtMPoints);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtTPoints);
            this.Controls.Add(this.txtMDiscount);
            this.Controls.Add(this.txtPoints);
            this.Controls.Add(this.txtSales);
            this.Controls.Add(this.txtLname);
            this.Controls.Add(this.txtFname);
            this.Controls.Add(this.txtCus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Massy Loyality Manager";
            this.Activated += new System.EventHandler(this.Form1_ActivatedAsync);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panDisplay.ResumeLayout(false);
            this.panDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRedeem;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCus;
        private System.Windows.Forms.TextBox txtFname;
        private System.Windows.Forms.TextBox txtLname;
        private System.Windows.Forms.TextBox txtSales;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.TextBox txtMDiscount;
        private System.Windows.Forms.TextBox txtTPoints;
        private System.Windows.Forms.TextBox txtLoca;
        private System.Windows.Forms.TextBox txtCashier;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMPoints;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMValues;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTrans;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnVoid;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.Button btnBalance;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblLoca;
        private System.Windows.Forms.Panel panDisplay;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.TextBox txtPts;
        private System.Windows.Forms.TextBox txtExpired;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblMode;
    }
}

