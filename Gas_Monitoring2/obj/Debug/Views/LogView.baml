   M S B A M L   `   `   `  ����J  FGas_Monitoring2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Gas_Monitoring2.ViewsGas_Monitoring2.Views  5      G(clr-namespace:Gas_Monitoring2.ViewModelsGas_Monitoring2.ViewModels  5	   P   #    Gas_Monitoring2.Views.LogView   5      j fDevExpress.Xpf.LayoutControl.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  a ]DevExpress.Xpf.Core.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  6X   c _DevExpress.Xpf.Charts.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aPmyGhttp://schemas.devexpress.com/winfx/2008/xaml/charts/rangecontrolclient  6�   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      /Views#clr-namespace:Gas_Monitoring2.Views  5      e aDevExpress.Xpf.Controls.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxwui7http://schemas.devexpress.com/winfx/2008/xaml/windowsui  5      ]	 YDevExpress.Mvvm.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm  	 5	      ;
ViewModels(clr-namespace:Gas_Monitoring2.ViewModels  	 6P     � DataContext  5      1  +DevExpress.Mvvm.UI.ViewModelSourceExtension     Type$ ViewModels:LogViewModelL�.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $	��10* �6   .+�5   
   9� $4��.로그를 조회할 옵션을 설정합니다.��6   .��5      �� .+�5      �  5      �� 5      $��1* �6(   �� 5      $��1* �6(    � ColumnDefinitions 5      �� 5      $��1* �6+   �� 5      $��2* �6+   �� 5      $��1* �6+   �� 5      $��2* �6+   .+�5       �� $��로그 수 : ��6   $��0��6.   $��Center=�6>   ���	#FF706E6E$��11(�6w   $��Right=�6�    � ToolTip$1 +조회할 로그 개수를 선택합니다.��6�   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5!      $��1��6'   $��Center=�6~   $
��15,5q�6�   $��Stretch=�6�   $
��15,2q�6�   $��11(�6�   '  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable$ FalseX�6�   +  %DevExpress.Xpf.Editors.LookUpEditBase   ItemsSource 67   �� *
Num_list+   SelectedItem 6X   �� *Selected_num+6�   �� 5"      $��디바이스 : ��6   $��2��6.   $��Center=�6>   ���	#FF706E6E$��11(�6w   $��Right=�6�   $0 *조회할 디바이스를 선택합니다.��6�     5#      $��3��6'   $��Center=�6�   $
��15,5q�6�   $��Stretch=�6�   $
��15,2q�6�   $��11(�6�   $ FalseX�6�    67   �� *Serial_list+ 6[   �� *Selected_serial+6�   �� 5$      $��가스 종류 : ��6   $��1��6/   $��0��6<   $��Center=�6L   ���	#FF706E6E$��11(�6�   $��Right=�6�   $1 +조회할 가스 종류를 선택합니다.��6�     5%      $��1��6'   $��1��64   $��Center=�6�   $
��15,5q�6�   $��Stretch=�6�   $
��15,2q�6�   $��11(�6�   $ FalseX�6�    6D   �� *
Gas_list+ 6e   �� *Selected_gas+6�   �� 5&      $��	레벨 : ��6   $��1��6,   $��2��69   $��Center=�6I   ���	#FF706E6E$��11(�6�   $��Right=�6�   $* $조회할 레벨을 선택합니다.��6�     5'      $��1��6'   $��3��64   $��Center=�6�   $
��15,5q�6�   $��Stretch=�6�   $
��15,2q�6�   $��11(�6�   $ FalseX�6�    6D   �� *
Lvl_list+ 6e   �� *Selected_lvl+6�   5(      � 5)       5*      �� 5+      $��4* �6+   �� 5,      $��1* �6+   �� 5-      $��4* �6+   �� 5.      $��3* �6+   .+�50      %  DevExpress.Xpf.Editors.DateEdit  $��0��6#   $
��20,0q�63      ShowClearButton .  $��Center=�6w   $��Center=�51   '   $��Center=�6`   $4 .조회를 시작할 날짜를 선택합니다.��52   x   	   MaxValue	 50   A   �� *
Max_date+��51   I   Z� 
   DateTime
 52   '   �� *
Start_date+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 53      '  !DevExpress.Mvvm.UI.EventToCommand  54      �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ EditValueChanged��64   +	  %DevExpress.Mvvm.UI.EventToCommandBase 	  Command 6Q   �� *PickCommand+56      �� 57      $��1��6   $��~��6,   $��Center=�68   $��Center=�6\     58      $��2��6#   $
��20,0q�63    .  $��Center=�59   '   $��Center=�6K   $��Center=�6�   $1 +조회를 끝낼 날짜를 선택합니다.��5:   v      MinValue 58   A   �� *
Min_date+	 6_   �� *
Max_date+��59   m   Z� 
 5:   '   �� *
End_date+$ PropertyChanged=�$ TwoWay=�6v   �� 5;      $��3��6   $��10,15q�6-   $��조회��6<   $
��Bold"�6I   $��2q�6{   $7 1선택한 설정으로 로그를 조회합니다.��6�   ��6[   �� *LoadCommand+6�   5<      5=      5>      � 5?   
   $��1��6   
������� 5@      �� 5A      $��3* �6   �� 5B      $��2* �6   �� 5C      $��2* �6   �� 5D      $��3* �6   �� 5E      $��2* �6   �� 5F      $��2* �6   �� 5G      $��5* �6   .+�5I      �� $��6��6   ���	#FF5F5F5F$��0��6?   �� 5J      $��0��6   $��날짜��6$   $��Center=�61   $��Center=�6U   $
��Bold"�6w   �� 5K      $��1��6   $��시간��6$   $��Center=�61   $��Center=�6U   $
��Bold"�6w   �� 5L      $��2��6   $��가스��6$   $��Center=�61   $��Center=�6U   $
��Bold"�6w   �� 5M      $��3��6   $��농도(ppm)��6$   $��Center=�66   $��Center=�6Z   $
��Bold"�6|   �� 5N      $��4��6   $��레벨��6$   $��Center=�61   $��Center=�6U   $
��Bold"�6w   �� 5O      $��5��6   $��장소��6$   $��Center=�61   $��Center=�6U   $
��Bold"�6w   �� 5P      $��6��6   $��디바이스��6$   $��Center=�63   $��Center=�6W   $
��Bold"�6y   5Q      (
  "DevExpress.Xpf.WindowsUI.SlideView
  5R   
   $��1q�6   $��2��6%   $
��Auto=�6P    
  Orientation$ Vertical=�6�   
�������$��0q�6�   $��Stretch=�6�   ��62   �� *Items+6�   5S      5T      