   M S B A M L   `   `   `  ����J  FGas_Monitoring2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Gas_Monitoring2.ViewModelsGas_Monitoring2.ViewModels  5   P   %    Gas_Monitoring2.Views.AlertView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      ] YDevExpress.Mvvm.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      ;
ViewModels(clr-namespace:Gas_Monitoring2.ViewModels   6P   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      e aDevExpress.Xpf.Controls.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxwui7http://schemas.devexpress.com/winfx/2008/xaml/windowsui  5	      .��5      �   � RowDefinitions  5   
   �� 5      $	��100 �6   �� 5      $��50 �6   �� 5      $	��120 �6   �� 5      $	��315 �6   �� 5      $��20 �6   .+�5   
   �� $��0��6   ���	#FF4B05056!   �� 5   
   ��6   *  $DevExpress.Xpf.Core.DXImageExtension  *53SvgImages/Icon Builder/Security_WarningCircled2.svg+�� 5   
   $��1��6   $��Danger��6   $��Center=�6.   $��Center=�6R   $
��Bold"�6t   $��Impact)�6�   $��30(�6�   
���  ��6�   �� 5   
   $��	10,0,10,0q�6   $��2��6'   
�������64   � 5   
   $��2��6     5      �� 5      $��1* �6    �� 5      $��1* �6    �� 5      $��1* �6     � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5       $��1* �6#   �� 5!      $��1* �6#   �� 5"      $��1* �6#   .+�5$      �� $��디바이스��6   $��0��6#   $��0��60   $��Center=�6@   $��Center=�6d   $
��Bold"�6�   $��16(�6�   �� 5%      $��0��69   $��1��6F   $	��0,5q�6V   $��Center=�6c   $��16(�6�   ��6   �� *Alert_log.Serial+6�   �� 5&      $��장소��6   $��0��6!   $��2��6.   $��Center=�6>   $��Center=�6b   $
��Bold"�6�   $��16(�6�   �� 5'      $��0��6;   $��3��6H   $	��0,5q�6X   $��Center=�6e   $��16(�6�   ��6   �� *Alert_log.Location+6�   �� 5(      $��가스��6   $��1��6!   $��0��6.   $��Center=�6>   $��Center=�6b   $
��Bold"�6�   
���  ��$��16(�6�   �� 5)      $��1��66   $��1��6C   $	��0,5q�6S   $��Center=�6`   
���  ��$��16(�6�   $
��Bold"�6�   ��6   �� *Alert_log.Gas+6�   �� 5*      $��농도��6   $��1��6!   $��2��6.   $��Center=�6>   $��Center=�6b   $
��Bold"�6�   
���  ��$��16(�6�   � 5+      $��1��6   $��3��6     5,      �� 5-      $��1* �6'   �� 5.      $��2* �6'   .+�50      �� $��0��6:   $	��0,5q�6J   $��Center=�6W   
���  ��$��16(�6�   $
��Left=�6�   $
��Bold"�6�   ��6   �� *Alert_log.Ppm+6�   �� 51      $	��ppm��6   $��1��6&   $	��0,5q�66   $��Center=�6C   $
��Left=�6e   
���  ��$��16(�6�   $
��Bold"�6�   52      �� 53      $��날짜��6   $��2��6!   $��0��6.   $��Center=�6>   $��Center=�6b   $
��Bold"�6�   $��16(�6�   �� 54      $��2��67   $��1��6D   $	��0,5q�6T   $��Center=�6a   $��16(�6�   ��6   �� *Alert_log.Date+6�   �� 55      $��시간��6   $��2��6!   $��2��6.   $��Center=�6>   $��Center=�6b   $
��Bold"�6�   $��16(�6�   �� 56      $��2��67   $��3��6D   $	��0,5q�6T   $��Center=�6a   $��16(�6�   ��6   �� *Alert_log.Time+6�   57      �� 58   
   $��3��6   $��10q�6$   $
��Auto=�60   $��Hidden=�6`   $��Stretch=�6�   $��Stretch=�6�   .��59      �� .+�5:      9�  9� 
IsExpanded . $	��0,5q�6-   $��물질 정보��6:   $��16(�6I   $
��Bold"�6W   .��5;      �� $��Uniform=�6;   $��Center=�6M   ��6   �� *Content_src+6M   5<      9� 5=      $	��0,5q�6   $��누출사고 대처방법��6(   $��16(�6;   $
��Bold"�6I   .��5>      �� $��Uniform=�6<   $��Center=�6N   ��6   �� *Exposure_src+6N   5?      9� 5@      $	��0,5q�6   $��응급조치 요령��6(   $��16(�69   $
��Bold"�6G   .��5A      �� $��Uniform=�6=   $��Center=�6O   ��6   �� *Emergency_src+6O   5B      5C      5D      �� 5E   
   $��4��6   $9��3*출처 : 고용노동부 안전보건공단 CHEM-i��6   $��Right=�6A   $��0,0,20,0q�6d   5F      5G      