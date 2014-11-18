#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// Enter the description of your new custom indicator here
    /// </summary>
    [Description("Enter the description of your new custom indicator here")]
    public class PriceDiff2 : Indicator
    {
        #region Variables
        // Wizard generated variables
            private int shift = 1; // Default setting for Shift
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.FromKnownColor(KnownColor.MediumBlue), PlotStyle.Line, "CloseDiff"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Green), PlotStyle.Line, "HighLow"));
            Add(new Plot(Color.FromKnownColor(KnownColor.DarkViolet), PlotStyle.Line, "LowHigh"));
            Overlay				= false;
			BarsRequired = Shift;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.
			if (CurrentBars[0] < BarsRequired)
				return;
            CloseDiff.Set(Close[0]-Close[Shift]);
            HighLow.Set(High[0]-Low[Shift]);
            LowHigh.Set(Low[0]-High[Shift]);
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries CloseDiff
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries HighLow
        {
            get { return Values[1]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries LowHigh
        {
            get { return Values[2]; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int Shift
        {
            get { return shift; }
            set { shift = Math.Max(1, value); }
        }
        #endregion
    }
}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private PriceDiff2[] cachePriceDiff2 = null;

        private static PriceDiff2 checkPriceDiff2 = new PriceDiff2();

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public PriceDiff2 PriceDiff2(int shift)
        {
            return PriceDiff2(Input, shift);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public PriceDiff2 PriceDiff2(Data.IDataSeries input, int shift)
        {
            if (cachePriceDiff2 != null)
                for (int idx = 0; idx < cachePriceDiff2.Length; idx++)
                    if (cachePriceDiff2[idx].Shift == shift && cachePriceDiff2[idx].EqualsInput(input))
                        return cachePriceDiff2[idx];

            lock (checkPriceDiff2)
            {
                checkPriceDiff2.Shift = shift;
                shift = checkPriceDiff2.Shift;

                if (cachePriceDiff2 != null)
                    for (int idx = 0; idx < cachePriceDiff2.Length; idx++)
                        if (cachePriceDiff2[idx].Shift == shift && cachePriceDiff2[idx].EqualsInput(input))
                            return cachePriceDiff2[idx];

                PriceDiff2 indicator = new PriceDiff2();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Shift = shift;
                Indicators.Add(indicator);
                indicator.SetUp();

                PriceDiff2[] tmp = new PriceDiff2[cachePriceDiff2 == null ? 1 : cachePriceDiff2.Length + 1];
                if (cachePriceDiff2 != null)
                    cachePriceDiff2.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachePriceDiff2 = tmp;
                return indicator;
            }
        }
    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.PriceDiff2 PriceDiff2(int shift)
        {
            return _indicator.PriceDiff2(Input, shift);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.PriceDiff2 PriceDiff2(Data.IDataSeries input, int shift)
        {
            return _indicator.PriceDiff2(input, shift);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.PriceDiff2 PriceDiff2(int shift)
        {
            return _indicator.PriceDiff2(Input, shift);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.PriceDiff2 PriceDiff2(Data.IDataSeries input, int shift)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.PriceDiff2(input, shift);
        }
    }
}
#endregion
