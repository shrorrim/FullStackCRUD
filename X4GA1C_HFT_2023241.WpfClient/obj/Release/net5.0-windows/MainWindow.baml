   M S B A M L   `   `   `  ���� W  SX4GA1C_HFT_2023241.WpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:X4GA1C_HFT_2023241.WpfClientX4GA1C_HFT_2023241.WpfClient  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem T PX4GA1C_HFT_2023241.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:X4GA1C_HFT_2023241.Models;assembly=X4GA1C_HFT_2023241.ModelsX4GA1C_HFT_2023241.Models -    'X4GA1C_HFT_2023241.WpfClient.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:X4GA1C_HFT_2023241.WpfClient     6sys&clr-namespace:System;assembly=mscorlib     ]modelsJclr-namespace:X4GA1C_HFT_2023241.Models;assembly=X4GA1C_HFT_2023241.Models       � Title$
  Main��$	��800��$
��1750�� � DataContext 6   0X4GA1C_HFT_2023241.WpfClient.MainWindowViewModel  .���  � ColumnDefinitions �� $��5* ��� $��7* ��� $	��10* �.+��� -    ��Name$ listBox2��$��2��$��20(�$��Consolas)����� *SelectedCollection+ �� UpdateSourceTrigger$ Explicit=� �� SelectedItem �� *SelectedElement+$ PropertyChanged=� � 	Resources  �� DataType&   X4GA1C_HFT_2023241.Models.Laptop%  X4GA1C_HFT_2023241.Models.Brand'  !X4GA1C_HFT_2023241.Models.Orderer%  X4GA1C_HFT_2023241.Models.Order+   %X4GA1C_HFT_2023241.WpfClient.YearInfo �� Orientation	 �� Padding��%  (��       *, +)(�� )     *, +)(�� M     *, +)(�� q     *, +)(�� �     *, +)�� "  .R��� ���� *	ModelName+�� "  .R��� ���� *Name+�� "  .R��� ���� *Name+�� "  .R��� ���� *Id+�� "  .R��� 
���   �$��1q�._��� $ 
Horizontal=�.+��� $��Year:��$��10q�$	 10q��� $��10q�$	 10q����� *Year+�� $��Month:��$��10q�$	 10q��� $��10q�$	 10q����� *Month+�� $��Income:��$��10q�$	 10q��� $��10q�$	 10q����� *IncomeByMonth+�� -   
 ��Name$
 inputStackPanel��$��1��.+��� $��20(�$��Consolas)����� *SelectedElement+ �� Mode$ TwoWay=�$ Explicit=� ��%�  (��       *, +)(�� Z    *, +)(�� �    *, +)(�� �    *, +)�� "  .R��� .+��� $��Brand:���� ���� *
Brand.Name+�� $��Model:���� ���� *	ModelName+�� $
��CPU:���� ���� *	Processor+�� $
��RAM:���� ���� *RAM+�� $��Storage:���� ���� *	Storage+�� $��RAM_Upgradeable:���� ���� *RAM_Upgradeable+�� $��Price:���� ���� *Price+�� "  .R��� .+��� $��Name:���� ���� *Name+�� $��YearOfAppearance:���� ���� *YearOfAppearance+�� "  .R��� .+��� $��OrderId:���� ���� *Id+�� $��Date:���� ���� *Date+�� $��Ordered Laptop's Id:���� ���� *
LaptopId+�� $��Orderer's Id:���� ���� *	OrdererId+�� $��Ordered Model:���� ���� *Laptop.ModelName+�� "  .R��� .+��� $��
OrdererId:���� ���� *Id+�� $��Name:���� ���� *Name+�� $��PhoneNumber:���� ���� *PhoneNumber+�� $��0��.+��� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Laptop������ *WorkWithLaptops+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Brand������ *WorkWithBrands+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Order������ *WorkWithOrders+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Orderer������ *WorkWithOrderers+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Create������ *Create+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Update������ *Update+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��Delete������ *Delete+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��AvgPriceByBrands������ *StatAvgPriceByBrands+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��MostPayingOrderers������ *StatMostPayingOrderers+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��MostPopularBrands������ *StatMostPopularBrands+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��MostPopularLaptopModels������ *StatMostPopularLaptopModels+�� 
���� ��
�������$��10q�$��5q�$��18(�$��Consolas)�$��GetStatByYear������ *StatGetStatByYear+