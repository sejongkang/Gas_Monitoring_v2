   M S B A M L   `   `   `  ����J  FGas_Monitoring2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Gas_Monitoring2.ViewModelsGas_Monitoring2.ViewModels  5   P   &     Gas_Monitoring2.Views.SimpleView   5      c _DevExpress.Xpf.Gauges.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxga4http://schemas.devexpress.com/winfx/2008/xaml/gauges  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ] YDevExpress.Mvvm.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      ;
ViewModels(clr-namespace:Gas_Monitoring2.ViewModels   6P   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      $	��350��5	   R   $	��400��6^     � DataContext  6   1  +DevExpress.Mvvm.UI.ViewModelSourceExtension     Type$ ViewModels:TabViewModelL� � 	Resources 5
       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value��%�  '��       �� 5   
   " ��6   .�5      ��  � �� �	#FF8391896+   �� 5       � ��
 �����6+   �� 5       �@J�SnapsToDevicePixels . 64   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 8��6!   #��z��62   ._�5      � .+�5   "   �� $ MyContentPresenter��63   #��z��6O   $��Center=�6s   $��Center=�6�   $��0,0,0,0q�6�   5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   ��  � �� �	#FF18935B6?   �� 5   "    � ��
 �����6?   5      P� 5      
 � �� . . �5   "   ��  � �� �	#FF1574496?   �� 5   "    � ��
 �����6?   5      5!      5#      5$      5%      .��5&      � $��5q�6    � RowDefinitions 5'   
   �� 5(      $��1* �6   �� 5*      $��8* �6   .+�5,   
   �� $��3��6   ���	#FFFDFDFD$��2��66   ��6J   �� *
Conn_stk+�� 5-   
   $��0��6   $��2q�6�    � ToolTip$* $디바이스 탭을 생성합니다.��6�   $��16(�6�   $
��Bold"�6�   $��5q�6�   ��6   �� *Dev.Local_name+��6A   �� *ButtonCommand+��6c   �� *
Conn_stk+6�   � 57   
   $��1��6   $��5,5,5,5q�6    � ColumnDefinitions 58      �� 59      $��5* �6#   �� 5:      $��4* �6#   .+�5<      � $��0��6   .+�5=      �� $��2��6   $��2��6-   ���	#FFF0F0F06A   � 5>       5?      �� 5@      $��2* �6(   �� 5A      $��3* �6(   �� 5B      $��3* �6(   �� 5C      $��3* �6(   �� 5D      $��3* �6(   �� 5E      $��3* �6(   �� 5F      $��3* �6(    5H      �� 5I      $��1* �6+   �� 5J      $��1* �6+   �� 5K      $��1* �6+   .+�5M      �� $��가스��6   $��0��6)   $��0��66   $��Stretch=�6F   $��Stretch=�6d   $��Center=�6�   
�������$
��Bold"�6�   $��13(�6�   ���	#FF999999$��Center=�6�   $��1q�6  �� 5N      $�� 농도(ppm) ��6   $��0��60   $��1��6=   $��2��6M   $��Stretch=�6a   $��Stretch=�6   $��Center=�6�   
�������$
��Bold"�6�   $��13(�6�   ���	#FF9B9B9B$��Center=�6  $��1q�6+  �� 5O      $
��H2S ��6   $��1��6+   $��0��68   $��Center=�6H   ���	#FF858585$��Right=�6�   $��0q�6�   $��15(�6�   �� 5P      $
��NH3 ��6   $��2��6+   $��0��68   $��Center=�6H   ���	#FF858585$��Right=�6�   $��0q�6�   $��15(�6�   �� 5Q      $��  CH3SH ��6   $��3��6/   $��0��6<   $��Center=�6L   ���	#FF858585$��Right=�6�   $��0q�6�   $��15(�6�   �� 5R      $	��CO ��6   $��4��6*   $��0��67   $��Center=�6G   ���	#FF858585$��Right=�6�   $��0q�6�   $��15(�6�   �� 5S      $
��CO2 ��6   $��5��6+   $��0��68   $��Center=�6H   ���	#FF858585$��Right=�6�   $��0q�6�   $��15(�6�   �� 5T      $
��CH4 ��6   $��6��6+   $��0��68   $��Center=�6H   ���	#FF858585$��Right=�6�   $��0q�6�   $��15(�6�   �� 5U      $��1��6<   $��1��6I   $��Center=�6Y   ���	#FF040404$��Right=�6�   $
��Bold"�6�   $��0q�6�   $��15(�6�   ��6   �� *Ppm_list[0]+6�   �� 5V      $��2��6<   $��1��6I   $��Center=�6Y   ���	#FF040404$��Right=�6�   $
��Bold"�6�   $��0q�6�   $��15(�6�   ��6   �� *Ppm_list[1]+6�   �� 5W      $��3��6<   $��1��6I   $��Center=�6Y   ���	#FF040404$��Right=�6�   $
��Bold"�6�   $��0q�6�   $��15(�6�   ��6   �� *Ppm_list[2]+6�   �� 5X      $��4��6<   $��1��6I   $��Center=�6Y   ���	#FF040404$��Right=�6�   $
��Bold"�6�   $��0q�6�   $��15(�6�   ��6   �� *Ppm_list[3]+6�   �� 5Y      $��5��6<   $��1��6I   $��Center=�6Y   ���	#FF040404$��Right=�6�   $
��Bold"�6�   $��0q�6�   $��15(�6�   ��6   �� *Ppm_list[4]+6�   �� 5Z      $��6��6<   $��1��6I   $��Center=�6Y   ���	#FF040404$��Right=�6�   $
��Bold"�6�   $��0q�6�   $��15(�6�   ��6   �� *Ppm_list[5]+6�   �� 5[      $��1��6N   $��2��6[   $
��None=�6�   $��Center=�6�   $��Center=�6�   ��6   *  $DevExpress.Xpf.Core.DXImageExtension  *SvgImages/Arrows/MoveUp.svg+y�6k   �� *Setting[0].Max+6�   �� 5\      $��2��6N   $��2��6[   $
��None=�6�   $��Center=�6�   $��Center=�6�   ��6     *SvgImages/Arrows/MoveUp.svg+y�6k   �� *Setting[1].Max+6�   �� 5]      $��3��6N   $��2��6[   $
��None=�6�   $��Center=�6�   $��Center=�6�   ��6     *SvgImages/Arrows/MoveUp.svg+y�6k   �� *Setting[2].Max+6�   �� 5^      $��4��6N   $��2��6[   $
��None=�6�   $��Center=�6�   $��Center=�6�   ��6     *SvgImages/Arrows/MoveUp.svg+y�6k   �� *Setting[3].Max+6�   �� 5_      $��5��6N   $��2��6[   $
��None=�6�   $��Center=�6�   $��Center=�6�   ��6     *SvgImages/Arrows/MoveUp.svg+y�6k   �� *Setting[4].Max+6�   �� 5`      $��6��6N   $��2��6[   $
��None=�6�   $��Center=�6�   $��Center=�6�   ��6     *SvgImages/Arrows/MoveUp.svg+y�6k   �� *Setting[5].Max+6�   5a      5b      � 5c      $��1��6    5d      �� 5e      $��1* �6$   �� 5f      $��9* �6$   .+�5h      �� $��0��6:   $��Center=�6G   
�������$
��Bold"�6|   $��17(�6�   $��Center=�6�   $��1q�6�   ��6   �� *Alert_set.Txt+��6�   �� *Alert_set.Back+6�   0  *DevExpress.Xpf.Gauges.CircularGaugeControl  5i      $��1��6,   $��5q�69      Scales 5j      $  DevExpress.Xpf.Gauges.ArcScale  5k         ShowSpindleCap] YDevExpress.Data.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$
 True 6(   !  DevExpress.Xpf.Gauges.Scale   
ShowLabels$ False 6>      ShowLine$ False 6Q      ShowMajorTickmarks$ False 6b      
StartValue$ 0Q�6}      EndValue$ 6Q�6�      MajorIntervalCount$ 6��6�      MinorIntervalCount$ 1��6�   $��40(�6�   $	��0,5q�6�      Needles 5l      *  $DevExpress.Xpf.Gauges.ArcScaleNeedle  5m   "   .  (DevExpress.Xpf.Gauges.ValueIndicatorBase   IsInteractive .     Value 6L   �� *Alert_set.Level+   Presentation 5n   &   @	  :DevExpress.Xpf.Gauges.CleanWhiteArcScaleNeedlePresentation	  5o   *   5q   #      Ranges 5s      )
  #DevExpress.Xpf.Gauges.ArcScaleRange
  5t   "   %  DevExpress.Xpf.Gauges.RangeBase   
StartValue/  )DevExpress.Xpf.Gauges.RangeValueConverter$ 0 65      EndValue$ 2 6D      Options 5u   &   (  "DevExpress.Xpf.Gauges.RangeOptions  5v   *       	Thickness  6<   �� *Alert_set.G_thick+! 
  Presentation! 5x   &   <  6DevExpress.Xpf.Gauges.DefaultArcScaleRangePresentation  5y   *   ?  9DevExpress.Xpf.Gauges.PredefinedArcScaleRangePresentation"   Fill" 5z   .   �� 5{   2   # �� Color$# 	#FF43C743��6B   5}   +   5   #   
  5�   "   $ 2 65   $ 4 6D    5�   &     5�   *     6<   �� *Alert_set.Y_thick+! 5�   &     5�   *   " 5�   .   �� 5�   2   $# 	#FFDEC039��6B   5�   +   5�   #   
  5�   "   $ 4 65   $ 6 6D    5�   &     5�   *     6<   �� *Alert_set.R_thick+! 5�   &     5�   *   " 5�   .   �� 5�   2   $# 	#FFF30000��6B   5�   +   5�   #   $   Markers$ 5�      *  $DevExpress.Xpf.Gauges.ArcScaleMarker  5�   "    66   �� *Alert_set.Level+5�      5�      5�      5�      '  !DevExpress.Xpf.Core.WaitIndicator  5�   
   %   DeferedVisibility% . $��분류 중..��64   $��2��6d   $��8(�6q   y�6E   �� *	Loading+��5�      �� 5�      .R�5�      �� & �� Orientation$& Vertical=�6!   .+�5�      �� $��기다려 주십시오��6$   $��10(�64   �� 5�      ��6$   �� 5�      5�      5�      �� 5�   
   $��연결 끊김��6   $��2��6A   $
��Bold"�6N   ���	#FF959595$��16(�6w   $��Center=�6�   $��Center=�6�   ���	#FF898989$��2q�6�   
�������$��5,0,5,5q�6  y�6    �� *	Conn_text+6  5�      5�      