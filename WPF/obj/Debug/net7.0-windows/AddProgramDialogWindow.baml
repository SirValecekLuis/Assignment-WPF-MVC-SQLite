   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	        WPF.AddProgramDialogWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   #localclr-namespace:WPF     5   	     � Title$  Přidat studijní program��5   	   $	��450��6)   $	��800��66   .��5
      � -   5         �Name$ AddProgramGrid��5
       � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��20 �6   �� 5      $��40 �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Název Oboru��6-   6E   �� 5   
   -   5         ��Name$ TextBoxName��5   /   $��1��6   $��0��6   6F   �� 5   
   $��2��6   $��0��6   $��Popis oboru��6-   6E   �� 5   
   -   5        $ TextBoxDescription��5   /   $��3��6   $��0��6   6M   �� 5   
   $��4��6   $��0��6   $��Volné pozice��6-   6F   �� 5    
   -   5        $ TextBoxFreePositions��5    /   $��5��6   $��0��6   6O   �� 5"   
   -   5        $��0��5"      $��6��6!   $	��150��6.   $��5q�6:   $��Přidat studijní program��6E   6�   �� 5#   
   -   5         ��Name$ 	InfoLabel��5#      $��0��6#   $��7��63   $�� ��6@   6M   5$      5%      