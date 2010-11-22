﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using AudioAlign.Audio;

namespace AudioAlign.WaveControls {
    public class VirtualViewBase: Control {

        public static readonly DependencyProperty VirtualViewportOffsetProperty = DependencyProperty.Register(
            "VirtualViewportOffset", typeof(long), typeof(VirtualViewBase),
                new FrameworkPropertyMetadata { Inherits = true, AffectsRender = true });

        public static readonly DependencyProperty VirtualViewportWidthProperty = DependencyProperty.Register(
            "VirtualViewportWidth", typeof(long), typeof(VirtualViewBase),
                new FrameworkPropertyMetadata { Inherits = true, AffectsRender = true, 
                    PropertyChangedCallback = OnViewportWidthChanged, DefaultValue = 1000L });

        private static void OnViewportWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            VirtualViewBase ctrl = (VirtualViewBase)d;
            ctrl.OnViewportWidthChanged((long)e.OldValue, (long)e.NewValue);
        }

        public long VirtualViewportOffset {
            get { return (long)GetValue(VirtualViewportOffsetProperty); }
            set { SetValue(VirtualViewportOffsetProperty, value); }
        }

        public long VirtualViewportWidth {
            get { return (long)GetValue(VirtualViewportWidthProperty); }
            set { SetValue(VirtualViewportWidthProperty, value); }
        }

        public Interval VirtualViewportInterval {
            get { return new Interval(VirtualViewportOffset, VirtualViewportOffset + VirtualViewportWidth); }
        }

        public static long PhysicalToVirtualOffset(Interval virtualViewportInverval, double controlWidth, double physicalOffset) {
            long visibleIntervalOffset = (long)Math.Round(virtualViewportInverval.Length / controlWidth * physicalOffset);
            return virtualViewportInverval.From + visibleIntervalOffset;
        }

        public static double VirtualToPhysicalOffset(Interval virtualViewportInverval, double controlWidth, long virtualOffset) {
            virtualOffset -= virtualViewportInverval.From;
            double physicalOffset = controlWidth / virtualViewportInverval.Length * virtualOffset;
            return physicalOffset;
        }

        public long PhysicalToVirtualOffset(double physicalOffset) {
            return PhysicalToVirtualOffset(VirtualViewportInterval, ActualWidth, physicalOffset);
        }

        public double VirtualToPhysicalOffset(long virtualOffset) {
            return VirtualToPhysicalOffset(VirtualViewportInterval, ActualWidth, virtualOffset);
        }

        protected virtual void OnViewportWidthChanged(long oldValue, long newValue) {}
    }
}
