   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5          WPF.SchoolsControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:WPF     5      .��5
      � -   5          �Name$  
SchoolGrid��5
      $��1q�6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��80 �6   �� 5      $��80 �6   �� 5      $	��150 �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��0��5      $��0��6   $��1q�6.   $��Přidat školu��69   6d   �� 5   
   -   5        $��0��5      $��1��6   $��1q�6.   $��Smazat školu��69   6g   �� 5   
   -   5        $��0��5      $��2��6   $��1q�6.   $!��Zobrazit studijní programy��69   6u   �� 5   
   -   5        $��0��5      $��3��6   $��1q�6.   $��Zobrazit přihlášky��69   6q   �� 5   
   $��1��6   $��0��6$   $��5��64   $��Visible=�6H   .��5       �� -   5         ��Name$ ItemsControl��5       ��61   �� *	Schools+��5!      �� 5"      .R�5#      ��  ��Name$ 
ItemBorder��65   
���   �$��1q�6I   $��2q�6]    �� Padding$ 10q�6h   ._�5$      ��  �� Orientation$ Vertical=�6)   .+�5%   "   �� $
��Bold"�6,   Název školy6K   �� 5&   "   ��6,   �� *Name+6D   �� 5'   "   $
��Bold"�6,   Adresa školy6L   �� 5(   "   ��6,   �� *	Address+6G   5)      5*      5+      5-      5.      51      52      