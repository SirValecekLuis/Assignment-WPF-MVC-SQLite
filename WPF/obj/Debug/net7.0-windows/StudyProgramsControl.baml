   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5          WPF.StudyProgramsControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:WPF     5      .��5
      � -   5          �Name$  
SchoolGrid��5
      $��1q�6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $	��150 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��1q�5      $��0��6   $��0��6)   $��Přidat studijní program��69   6p   �� 5   
   -   5        $��1q�5      $��0��6   $��1��6)   $��Smazat studijní program��69   6s   �� 5   
   $��1��6   $��0��6   .��6-   Studijní programy6@   �� 5   
   $��2��6   $��0��6$   $��3��64   $
��Auto=�6H   .��5      �� -   5         ��Name$ ItemsControl��5      ��61   �� *
Programs+��5      �� 5      .R�5       ��  ��Name$ 
ItemBorder��6!   
���   �$��1q�6I   $��2q�6]    �� Padding$ 10q�6h   ._�5!      ��  �� Orientation$ Vertical=�6)   .+�5"   "   �� $
��Bold"�6,   Název oboru6K   �� 5#   "   ��6,   �� *Name+6D   �� 5$   "   $
��Bold"�6,   Popis oboru6K   �� 5%   "   ��6,   �� *Description+6K   �� 5&   "   $
��Bold"�6,   Volná místa6K   �� 5'   "   ��6,   �� *FreePositions+6M   �� 5(   "   $
��Bold"�6,   Zabraná místa6M   �� 5)   "   ��6,   �� *OccupiedPositions+6Q   5*      5+      5,      5.      5/      51      52      