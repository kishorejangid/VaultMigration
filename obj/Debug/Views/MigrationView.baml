   M S B A M L   `   `   `  ����I  EVaultMigration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:VaultMigration.ConvertersVaultMigration.Converters  5      (    "VaultMigration.Views.MigrationView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      6conv'clr-namespace:VaultMigration.Converters    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � 	Resources  5      7   1VaultMigration.Converters.InverseBooleanConverter   InverseBooleanConverter��%   &	          5   
   6I   5      .��5      ]� $��5q�6   .+�5	      �� $	��Top=�6    �� Orientation$ Vertical=�6&   .+�5
   
   ��  �� Padding$ 5q�6   $��1q�6     
AssetBrush#��[ 61   ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   64   �� 5      $
��Auto �6$   64   �� 5      $
��Auto �6$   64   �� 5      $
��Auto �6$   64   �� 5      $
��Auto �6$   64   �� 5      $
��Auto �6$   64   �� 5      $
��Auto �6$   64    � ColumnDefinitions 5      �� 5      $
��Auto �6'   66   �� 5      $��* �6'   63   .+�5      �� $��0��6   $��0��6)   $��0,0,5,0q�69   $��Center=�6J   $��Right=�6e   Select Application:6�   �� 5      $��27��6   $��0��6(   $��1��65   $��0,5,0,5q�6E   $	��150��6V   $
��Left=�6�    �� DisplayMemberPath$
 Name��6�   ��6b   �� *Applications+ �� SelectedItem 6�   �� *SelectedApplication+��5      �� 5       �� 
TargetType" ��6    .�5   "   ��  �� Property � {�	 �� Value	 6>   �� *
IsMigrated+
 �� 	Converter#
 [  6�   5      5       �� 5"      $��1��6   $��0��6)   $��0,0,5,0q�69   $��Center=�6J   $��Right=�6e   Temp Download Path:6�   �� 5#      $��27��6   $��Center=�6&   #��[ 6H   $��1��6r   $��1��6   $��0,5,0,5q�6�   $	��150��6�   $
��Left=�6�   ��6�   �� *TempDownloadPath+�� 5%      
�������#��[ 6,   $��0q�6U   $��30��6i   $��2��6u   $��1��6�   ��6�   �� *MigrateCommand+.��6�   	Migrate6�   5&      5'      �� 5(   
   $��1q�6   #��[ 6%   $ 5q�6O   $��0,5,0,0q�6[   ._�5)      �� $ Vertical=�6   .+�5*      �� Downloading �� 6-   �61   �� *DownloadProgress+ �� TargetNullValue$ 0��6g    of �� 6p   �6t   �� *
TotalCount+$ 0��6�   6�   �� 5+      $��0,5,0,0q�6    Downloading �� 6>   �6B   �� *CurrentDownload+6e   6k   � 5,      #��[ 6"   $��5��6K   $��0q�6V    � Minimum$ 0Q�6�   $��0,3,0,0q�6�    � Value 6j   �� *DownloadProgress+$ 0�� � Maximum 6�   �� *
TotalCount+$	 100��6�   5-      5.      �� 5/   
   $��1q�6   #��[ 6%   $��0,5,0,0q�6O   $ 5q�6a   ._�50      �� $ Vertical=�6   .+�51      �� #!No of Vault Objects in Queue → �� 6@   �6D   �� *QueueLength+ �� Mode$ OneWay=�6o   6u   �� 52      
Uploading �� 6+   �6/   �� *CurrentUpload+6P   6V   53      54      55      � 56      $��Bottom=�6    � IsIndeterminate . #��[ 6A   $��Bottom=�6j   $��10��6�   $��0q�6�   $ 67Q�6�   $��0,3,0,0q�6�   6�   57      58      