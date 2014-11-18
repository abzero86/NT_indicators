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
    public class EMADiff : Indicator
    {
        #region Variables
        // Wizard generated variables
            private int fast = 12; // Default setting for Fast
            private int slow = 26; // Default setting for Slow
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.FromKnownColor(KnownColor.Orange), PlotStyle.Line, "FastEMA"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Green), PlotStyle.Line, "SlowEMA"));
            Overlay				= true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.
            FastEMA.Set(EMA(Close, Fast)[0]);
            SlowEMA.Set(EMA(Close, Slow)[0]);
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries FastEMA
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries SlowEMA
        {
            get { return Values[1]; }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int Fast
        {
            get { return fast; }
            set { fast = Math.Max(1, value); }
        }

        [Description("")]
        [GridCategory("Parameters")]
        public int Slow
        {
            get { return slow; }
            set { slow = Math.Max(1, value); }
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
        private EMADiff[] cacheEMADiff = null;

        private static EMADiff checkEMADiff = new EMADiff();

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public EMADiff EMADiff(int fast, int slow)
        {
            return EMADiff(Input, fast, slow);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public EMADiff EMADiff(Data.IDataSeries input, int fast, int slow)
        {
            if (cacheEMADiff != null)
                for (int idx = 0; idx < cacheEMADiff.Length; idx++)
                    if (cacheEMADiff[idx].Fast == fast && cacheEMADiff[idx].Slow == slow && cacheEMADiff[idx].EqualsInput(input))
                        return cacheEMADiff[idx];

            lock (checkEMADiff)
            {
                checkEMADiff.Fast = fast;
                fast = checkEMADiff.Fast;
                checkEMADiff.Slow = slow;
                slow = checkEMADiff.Slow;

                if (cacheEMADiff != null)
                    for (int idx = 0; idx < cacheEMADiff.Length; idx++)
                        if (cacheEMADiff[idx].Fast == fast && cacheEMADiff[idx].Slow == slow && cacheEMADiff[idx].EqualsInput(input))
                            return cacheEMADiff[idx];

                EMADiff indicator = new EMADiff();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                indicator.Fast = fast;
                indicator.Slow = slow;
                Indicators.Add(indicator);
                indicator.SetUp();

                EMADiff[] tmp = new EMADiff[cacheEMADiff == null ? 1 : cacheEMADiff.Length + 1];
                if (cacheEMADiff != null)
                    cacheEMADiff.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cacheEMADiff = tmp;
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
        public Indicator.EMADiff EMADiff(int fast, int slow)
        {
            return _indicator.EMADiff(Input, fast, slow);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.EMADiff EMADiff(Data.IDataSeries input, int fast, int slow)
        {
            return _indicator.EMADiff(input, fast, slow);
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
        public Indicator.EMADiff EMADiff(int fast, int slow)
        {
            return _indicator.EMADiff(Input, fast, slow);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.EMADiff EMADiff(Data.IDataSeries input, int fast, int slow)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.EMADiff(input, fast, slow);
        }
    }
}
#endregion
