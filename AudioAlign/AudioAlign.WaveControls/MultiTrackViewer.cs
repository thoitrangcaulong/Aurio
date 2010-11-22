﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace AudioAlign.WaveControls {
    [TemplatePart(Name = "PART_TimeScale", Type = typeof(TimeScale))]
    [TemplatePart(Name = "PART_TrackListBox", Type = typeof(MultiTrackListBox))]
    public class MultiTrackViewer : VirtualViewBase {

        public static readonly DependencyProperty VirtualCaretOffsetProperty;

        static MultiTrackViewer() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiTrackViewer), 
                new FrameworkPropertyMetadata(typeof(MultiTrackViewer)));

            VirtualCaretOffsetProperty = TrackPositionSelectorOverlay.VirtualCaretOffsetProperty
                .AddOwner(typeof(MultiTrackViewer), new FrameworkPropertyMetadata() { Inherits = true });
        }

        public MultiTrackViewer() {
            this.Loaded += new RoutedEventHandler(MultiTrackViewer_Loaded);
        }

        private void MultiTrackViewer_Loaded(object sender, RoutedEventArgs e) {
            TrackPositionSelectorOverlay caretOverlay = GetTemplateChild("PART_Caret") as TrackPositionSelectorOverlay;
            if (caretOverlay != null) {
                Binding virtualCaretOffsetBinding = new Binding() {
                    Source = caretOverlay,
                    Path = new PropertyPath("VirtualCaretOffset")
                };
                SetBinding(VirtualCaretOffsetProperty, virtualCaretOffsetBinding);
            }
        }

        public long VirtualCaretOffset {
            get { return (long)GetValue(VirtualCaretOffsetProperty); }
        }

        public ItemCollection Items {
            get {
                ItemsControl itemsControl = GetTemplateChild("PART_TrackListBox") as ItemsControl;
                return itemsControl.Items;
            }
        }
    }
}
