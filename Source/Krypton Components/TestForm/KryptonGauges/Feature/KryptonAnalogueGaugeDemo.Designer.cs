#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class KryptonAnalogueGaugeDemo
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.kpnlRoot = new Krypton.Toolkit.KryptonPanel();
        this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
        this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
        this.flpGauges = new System.Windows.Forms.FlowLayoutPanel();
        this.kcgMain = new Krypton.Gauges.KryptonCircularGauge();
        this.klgMain = new Krypton.Gauges.KryptonLinearGauge();
        this.krgMain = new Krypton.Gauges.KryptonRectangularGauge();
        this.krgSegments = new Krypton.Gauges.KryptonRectangularGauge();
        this.kcgInteractive = new Krypton.Gauges.KryptonCircularGauge();
        this.kpnlControls = new Krypton.Toolkit.KryptonPanel();
        this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
        this.klblValue = new Krypton.Toolkit.KryptonLabel();
        this.ktrkValue = new Krypton.Toolkit.KryptonTrackBar();
        this.kchkAnimate = new Krypton.Toolkit.KryptonCheckBox();
        this.kchkInteractive = new Krypton.Toolkit.KryptonCheckBox();
        this.kbtnSelectCircular = new Krypton.Toolkit.KryptonButton();
        this.kbtnSelectLinear = new Krypton.Toolkit.KryptonButton();
        this.kbtnSelectRectangular = new Krypton.Toolkit.KryptonButton();
        this.kbtnSelectSegments = new Krypton.Toolkit.KryptonButton();
        this.kpgGauge = new Krypton.Toolkit.KryptonPropertyGrid();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlRoot)).BeginInit();
        this.kpnlRoot.SuspendLayout();
        this.tlpMain.SuspendLayout();
        this.flpGauges.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.kpnlControls)).BeginInit();
        this.kpnlControls.SuspendLayout();
        this.tlpControls.SuspendLayout();
        this.SuspendLayout();
        //
        // kpnlRoot
        //
        this.kpnlRoot.Controls.Add(this.tlpMain);
        this.kpnlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlRoot.Location = new System.Drawing.Point(0, 0);
        this.kpnlRoot.Name = "kpnlRoot";
        this.kpnlRoot.Size = new System.Drawing.Size(980, 620);
        this.kpnlRoot.TabIndex = 0;
        //
        // tlpMain
        //
        this.tlpMain.ColumnCount = 2;
        this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
        this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
        this.tlpMain.Controls.Add(this.klblInstructions, 0, 0);
        this.tlpMain.Controls.Add(this.flpGauges, 0, 1);
        this.tlpMain.Controls.Add(this.kpnlControls, 0, 2);
        this.tlpMain.Controls.Add(this.kpgGauge, 1, 0);
        this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpMain.Location = new System.Drawing.Point(0, 0);
        this.tlpMain.Name = "tlpMain";
        this.tlpMain.Padding = new System.Windows.Forms.Padding(8);
        this.tlpMain.RowCount = 3;
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        this.tlpMain.Size = new System.Drawing.Size(980, 620);
        this.tlpMain.SetColumnSpan(this.klblInstructions, 1);
        this.tlpMain.SetRowSpan(this.kpgGauge, 3);
        this.tlpMain.TabIndex = 0;
        //
        // klblInstructions
        //
        this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblInstructions.Location = new System.Drawing.Point(11, 11);
        this.klblInstructions.Name = "klblInstructions";
        this.klblInstructions.Size = new System.Drawing.Size(662, 42);
        this.klblInstructions.TabIndex = 0;
        this.klblInstructions.Values.Text = "Issue #4656: circular, linear, and rectangular gauges share ranges and palette colours. Drag the trackbar, toggle animation / interactive input, or drag the dedicated interactive circular gauge.";
        //
        // flpGauges
        //
        this.flpGauges.AutoScroll = true;
        this.flpGauges.Controls.Add(this.kcgMain);
        this.flpGauges.Controls.Add(this.klgMain);
        this.flpGauges.Controls.Add(this.krgMain);
        this.flpGauges.Controls.Add(this.krgSegments);
        this.flpGauges.Controls.Add(this.kcgInteractive);
        this.flpGauges.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flpGauges.Location = new System.Drawing.Point(11, 59);
        this.flpGauges.Name = "flpGauges";
        this.flpGauges.Padding = new System.Windows.Forms.Padding(4);
        this.flpGauges.Size = new System.Drawing.Size(662, 430);
        this.flpGauges.TabIndex = 1;
        //
        // kcgMain
        //
        this.kcgMain.Location = new System.Drawing.Point(7, 7);
        this.kcgMain.Name = "kcgMain";
        this.kcgMain.Size = new System.Drawing.Size(180, 180);
        this.kcgMain.TabIndex = 0;
        //
        // klgMain
        //
        this.klgMain.Location = new System.Drawing.Point(193, 7);
        this.klgMain.Name = "klgMain";
        this.klgMain.Size = new System.Drawing.Size(240, 64);
        this.klgMain.TabIndex = 1;
        //
        // krgMain
        //
        this.krgMain.Location = new System.Drawing.Point(439, 7);
        this.krgMain.Name = "krgMain";
        this.krgMain.Size = new System.Drawing.Size(160, 56);
        this.krgMain.TabIndex = 2;
        //
        // krgSegments
        //
        this.krgSegments.Location = new System.Drawing.Point(7, 193);
        this.krgSegments.Name = "krgSegments";
        this.krgSegments.Size = new System.Drawing.Size(200, 48);
        this.krgSegments.TabIndex = 3;
        //
        // kcgInteractive
        //
        this.kcgInteractive.Location = new System.Drawing.Point(213, 193);
        this.kcgInteractive.Name = "kcgInteractive";
        this.kcgInteractive.Size = new System.Drawing.Size(160, 160);
        this.kcgInteractive.TabIndex = 4;
        this.kcgInteractive.ValueChanged += new System.EventHandler<Krypton.Gauges.GaugeValueChangedEventArgs>(this.OnGaugeValueChanged);
        //
        // kpnlControls
        //
        this.kpnlControls.Controls.Add(this.tlpControls);
        this.kpnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpnlControls.Location = new System.Drawing.Point(11, 495);
        this.kpnlControls.Name = "kpnlControls";
        this.kpnlControls.Size = new System.Drawing.Size(662, 114);
        this.kpnlControls.TabIndex = 2;
        //
        // tlpControls
        //
        this.tlpControls.ColumnCount = 4;
        this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
        this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        this.tlpControls.Controls.Add(this.klblValue, 0, 0);
        this.tlpControls.Controls.Add(this.ktrkValue, 1, 0);
        this.tlpControls.Controls.Add(this.kchkAnimate, 2, 0);
        this.tlpControls.Controls.Add(this.kchkInteractive, 3, 0);
        this.tlpControls.Controls.Add(this.kbtnSelectCircular, 0, 1);
        this.tlpControls.Controls.Add(this.kbtnSelectLinear, 1, 1);
        this.tlpControls.Controls.Add(this.kbtnSelectRectangular, 2, 1);
        this.tlpControls.Controls.Add(this.kbtnSelectSegments, 3, 1);
        this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpControls.Location = new System.Drawing.Point(0, 0);
        this.tlpControls.Name = "tlpControls";
        this.tlpControls.Padding = new System.Windows.Forms.Padding(4);
        this.tlpControls.RowCount = 2;
        this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tlpControls.Size = new System.Drawing.Size(662, 114);
        this.tlpControls.TabIndex = 0;
        //
        // klblValue
        //
        this.klblValue.Dock = System.Windows.Forms.DockStyle.Fill;
        this.klblValue.Location = new System.Drawing.Point(7, 7);
        this.klblValue.Name = "klblValue";
        this.klblValue.Size = new System.Drawing.Size(74, 48);
        this.klblValue.TabIndex = 0;
        this.klblValue.Values.Text = "Value: 0";
        //
        // ktrkValue
        //
        this.ktrkValue.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ktrkValue.Location = new System.Drawing.Point(87, 7);
        this.ktrkValue.Maximum = 100;
        this.ktrkValue.Name = "ktrkValue";
        this.ktrkValue.Size = new System.Drawing.Size(328, 48);
        this.ktrkValue.TabIndex = 1;
        this.ktrkValue.ValueChanged += new System.EventHandler(this.ktrkValue_ValueChanged);
        //
        // kchkAnimate
        //
        this.kchkAnimate.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kchkAnimate.Location = new System.Drawing.Point(421, 7);
        this.kchkAnimate.Name = "kchkAnimate";
        this.kchkAnimate.Size = new System.Drawing.Size(114, 48);
        this.kchkAnimate.TabIndex = 2;
        this.kchkAnimate.Values.Text = "Animate";
        this.kchkAnimate.CheckedChanged += new System.EventHandler(this.kchkAnimate_CheckedChanged);
        //
        // kchkInteractive
        //
        this.kchkInteractive.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kchkInteractive.Location = new System.Drawing.Point(541, 7);
        this.kchkInteractive.Name = "kchkInteractive";
        this.kchkInteractive.Size = new System.Drawing.Size(114, 48);
        this.kchkInteractive.TabIndex = 3;
        this.kchkInteractive.Values.Text = "Interactive";
        this.kchkInteractive.CheckedChanged += new System.EventHandler(this.kchkInteractive_CheckedChanged);
        //
        // kbtnSelectCircular
        //
        this.kbtnSelectCircular.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnSelectCircular.Location = new System.Drawing.Point(7, 61);
        this.kbtnSelectCircular.Name = "kbtnSelectCircular";
        this.kbtnSelectCircular.Size = new System.Drawing.Size(74, 46);
        this.kbtnSelectCircular.TabIndex = 4;
        this.kbtnSelectCircular.Values.Text = "Circular";
        this.kbtnSelectCircular.Click += new System.EventHandler(this.kbtnSelectCircular_Click);
        //
        // kbtnSelectLinear
        //
        this.kbtnSelectLinear.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnSelectLinear.Location = new System.Drawing.Point(87, 61);
        this.kbtnSelectLinear.Name = "kbtnSelectLinear";
        this.kbtnSelectLinear.Size = new System.Drawing.Size(328, 46);
        this.kbtnSelectLinear.TabIndex = 5;
        this.kbtnSelectLinear.Values.Text = "Linear";
        this.kbtnSelectLinear.Click += new System.EventHandler(this.kbtnSelectLinear_Click);
        //
        // kbtnSelectRectangular
        //
        this.kbtnSelectRectangular.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnSelectRectangular.Location = new System.Drawing.Point(421, 61);
        this.kbtnSelectRectangular.Name = "kbtnSelectRectangular";
        this.kbtnSelectRectangular.Size = new System.Drawing.Size(114, 46);
        this.kbtnSelectRectangular.TabIndex = 6;
        this.kbtnSelectRectangular.Values.Text = "Rectangular";
        this.kbtnSelectRectangular.Click += new System.EventHandler(this.kbtnSelectRectangular_Click);
        //
        // kbtnSelectSegments
        //
        this.kbtnSelectSegments.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kbtnSelectSegments.Location = new System.Drawing.Point(541, 61);
        this.kbtnSelectSegments.Name = "kbtnSelectSegments";
        this.kbtnSelectSegments.Size = new System.Drawing.Size(114, 46);
        this.kbtnSelectSegments.TabIndex = 7;
        this.kbtnSelectSegments.Values.Text = "Segments";
        this.kbtnSelectSegments.Click += new System.EventHandler(this.kbtnSelectSegments_Click);
        //
        // kpgGauge
        //
        this.kpgGauge.Dock = System.Windows.Forms.DockStyle.Fill;
        this.kpgGauge.Location = new System.Drawing.Point(679, 11);
        this.kpgGauge.Name = "kpgGauge";
        this.kpgGauge.Size = new System.Drawing.Size(290, 598);
        this.kpgGauge.TabIndex = 3;
        //
        // KryptonAnalogueGaugeDemo
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(980, 620);
        this.Controls.Add(this.kpnlRoot);
        this.Name = "KryptonAnalogueGaugeDemo";
        this.Text = "Analogue Gauges (#4656)";
        this.Load += new System.EventHandler(this.KryptonAnalogueGaugeDemo_Load);
        ((System.ComponentModel.ISupportInitialize)(this.kpnlRoot)).EndInit();
        this.kpnlRoot.ResumeLayout(false);
        this.tlpMain.ResumeLayout(false);
        this.tlpMain.PerformLayout();
        this.flpGauges.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.kpnlControls)).EndInit();
        this.kpnlControls.ResumeLayout(false);
        this.tlpControls.ResumeLayout(false);
        this.tlpControls.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonPanel kpnlRoot;
    private System.Windows.Forms.TableLayoutPanel tlpMain;
    private Krypton.Toolkit.KryptonLabel klblInstructions;
    private System.Windows.Forms.FlowLayoutPanel flpGauges;
    private Krypton.Gauges.KryptonCircularGauge kcgMain;
    private Krypton.Gauges.KryptonLinearGauge klgMain;
    private Krypton.Gauges.KryptonRectangularGauge krgMain;
    private Krypton.Gauges.KryptonRectangularGauge krgSegments;
    private Krypton.Gauges.KryptonCircularGauge kcgInteractive;
    private Krypton.Toolkit.KryptonPanel kpnlControls;
    private System.Windows.Forms.TableLayoutPanel tlpControls;
    private Krypton.Toolkit.KryptonLabel klblValue;
    private Krypton.Toolkit.KryptonTrackBar ktrkValue;
    private Krypton.Toolkit.KryptonCheckBox kchkAnimate;
    private Krypton.Toolkit.KryptonCheckBox kchkInteractive;
    private Krypton.Toolkit.KryptonButton kbtnSelectCircular;
    private Krypton.Toolkit.KryptonButton kbtnSelectLinear;
    private Krypton.Toolkit.KryptonButton kbtnSelectRectangular;
    private Krypton.Toolkit.KryptonButton kbtnSelectSegments;
    private Krypton.Toolkit.KryptonPropertyGrid kpgGauge;
}
