   M S B A M L   `   `   `  ����A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel  5   	       PacMan.View.MainView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:PacMan.ViewModel     5   	     � Title$  MainView��5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6_    � DataContext 5	      !   PacMan.ViewModel.MainViewVm  5
   
    J� InputBindings 5      �� 5   
   
 �� Key$
 Down��6    �� Command 6!   �� *	PressDown+�� 5   
   $ S��6    6   �� *	PressDown+�� 5   
   $ Up��6    6   �� *	PressUp+�� 5   
   $ W��6    6   �� *	PressUp+�� 5   
   $
 Left��6    6!   �� *	PressLeft+�� 5   
   $ A��6    6   �� *	PressLeft+�� 5   
   $ Right��6    6"   �� *
PressRight+�� 5   
   $ D��6    6   �� *
PressRight+�� 5   
   $ Enter��6    6"   �� *Start+.��5      � 
������� � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��1* �6   	 � ColumnDefinitions	 5   
   �� 5      $��1* �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   .+�5$   
   o� $��0��6   $��0��6   .@�5%      m� $��28��6   $��14(�6#   $
��Game��61   .@�5&      m� $��28��6   $��14(�6'   $��Start��65   
 m� InputGestureText$
 Enter��6D    m� Command 6]   �� *Start+��5'      �� 5(       �� 
TargetType" m�6     �� Triggers 5)      �� 5*   "    �� Binding 6.   �� *
IsGameOver+ �� Value	 False6M   .a�5+   &   ��  �� Property � y� �� Value$ Visible=�6C   5,   #   �� 5-   "    6.   �� *
IsGameOver+ True6M   .a�5.   &   ��  � y�$ 	Collapsed=�6C   5/   #   51      53      m� 54      $��28��6   $��14(�6'   $��Recreate��65    6G   �� *
Recreate+��55      �� 56      " m�6     57      �� 58   "    6.   �� *
IsGameOver+ True6M   .a�59   &   ��  � y�$ Visible=�6C   5:   #   �� 5;   "    6.   �� *
IsGameOver+	 False6M   .a�5<   &   ��  � y�$ 	Collapsed=�6C   5=   #   5?      5A      m� 5B      $��28��6   $��14(�6'   $��Enter Player Name��65    6P   �� *	EnterName+5C      5D      m� 5F      $��28��6<   $��14(�6H   $��Ghost behavior��6V   {�6   �� *IsGameNotBegun+��6n   �� *GhostsMovingAlgorithms+��5G      �� 5H      " m�6   .�5I      ��  � �� 63   ��  �� Path$ Header��� 5J       �@m�Command 64   �� $ ChangeAlgorithm��� 5K       �@m�	IsChecked 66   �� $ 	IsChecked�5L      5N      m� 5P      $��28��6   $��14(�6#   $��Show records��61    6G   �� *ShowRecords+5Q      �� 5S   
   $��1q�6   
���   �$��1q�6/   $��14(�6C   $��0��6k   $��1��6x   ��6Q   �� *Lives+6x   �� 5T   
   $��1q�6   
���   �$��1q�6/   $��14(�6C   $��80��6Q   $��0��6y   $��2��6�   ��6_   �� *Score+6�   �� 5U   
   $��1q�6   
���   �$��1q�6/   $��14(�6C   $��0��6j   $��3��6w   ��6Q   �� *Time+6w   �� 5W   
   $��1��66   $��0��6C   $��4��6S   ��6   �� *
Location+6S   5X      5Y      