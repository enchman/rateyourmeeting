   M S B A M L   `   `   `  ����J  FRateYourMeeting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:RateYourMeetingRateYourMeeting  5           RateYourMeeting.SignupPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:RateYourMeeting     5      .��5	      � .+�5
   
   �� -   5          ��Name$  
labelTitle��5
      $��Sign up��6$   $��Center=�66   $��0,30,0,0q�6S   $	��Top=�6e   $��21(�6}   �� 5   
   $	��400��6   $��50��6   $	��Top=�6)   $��
43,75,57,0q�6A   .+�5      �� -   5        $  labelUsername��5      $��Username��6+   $
��Left=�6>   $	��Top=�6Y   $��10(�6q    �� Left$ 51��6   
 �� Top$ 13��6�   �� 5      -   5         ��Name$ boxUsername��5      $��23��6+   $	 110��67    �� TextWrapping$
 Wrap=�6I   $ 13��6]   $	��280��6m   5      �� 5   
   $	��400��6   $��50��6   $	��Top=�6)   $��43,125,57,0q�6A   .+�5      �� -   5        $  labelPassword��5      $��Password��6+   $
��Left=�6>   $	��Top=�6Y   $��10(�6q   $ 53��6   $ 13��6�   G� 5      -   5         G�Name$ boxPassword��5      $��23��6/   $	 110��6;   $ 13��6M   $	��280��6]   5      �� 5   
   $	��400��6   $��50��6   $	��Top=�6)   $��43,175,57,0q�6A   .+�5      �� -   5        $  labelRepassword��5      $��Re-password��6-   $
��Left=�6C   $	��Top=�6^   $��10(�6v   $ 38��6�   $ 13��6�    J� RenderTransformOrigin$ -0.149,0.739&�6�   G� 5      -   5        $ boxRepassword��5      $��23��61   $	 110��6=   $ 13��6O   $	��280��6_   5      �� 5   
   $	��400��6   $��50��6   $	��Top=�6)   $��43,225,57,0q�6A   .+�5      �� -   5        $  labelFirstname��5      $��	Firstname��6,   $
��Left=�6@   $	��Top=�6\   $��10(�6t   $ 52��6�   $ 13��6�   $��53��6�   �� 5      -	   5        $ boxFirstname��5      $��23��6,   $	 110��68   $
 Wrap=�6J   $ 13��6^   $	��280��6n   5      �� 5   
   $	��400��6   $��50��6   $	��Top=�6)   $��43,275,57,0q�6A   .+�5      �� -
   5        $  labelLastname��5      $��Lastname��6+   $
��Left=�6>   $	��Top=�6Y   $��10(�6q   $ 53��6   $ 13��6�   $��52��6�   �� 5      -   5        $ boxLastname��5      $��23��6+   $	 110��67   $
 Wrap=�6I   $ 13��6]   $	��280��6m   5      �� 5   
   $	��400��6   $��50��6   $	��Top=�6)   $��43,325,57,0q�6A   .+�5       �� -   5        $  labelStatus��5       $��Status��6)   $
��Left=�6:   $	��Top=�6U   $��10(�6m   $ 68��6{   $ 10��6�   $��38��6�   �� 5!      $	��192��6   $	 134��6%   $ 16��67    �� Orientation$ 
Horizontal=�6G   .+�5"      	� -   5         	�Name$ radioCustomer��5"      	 	� 	GroupName$	 UserType��65   $��Customer��6J   $��0,0,20,0q�6]   
 d� 	IsChecked$

 TrueY�6o   	� 5#      -   5        $ radioWorker��5#      $	 UserType��63   $��Employee��6H   5$      5%      �� 5&   
   $	��400��6   $��50��6   $	��Top=�6)   $��43,375,57,0q�6A   .+�5'      �� -   5         ��Name$ buttonSignup��5'      $��Sign up��6+   $	 110��6=   $ 14��6O   $��74��6_   6j   �� 5(      -   5        $ buttonCancel��5(      $��Cancel��6+   $	 206��6<   $ 14��6N   $��74��6^   6i   5)      5+      5,      